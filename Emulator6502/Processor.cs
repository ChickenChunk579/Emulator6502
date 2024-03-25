using Emulator6502.BusDevices;
using Emulator6502.Instructions;

using Serilog;
using Serilog.Core;

namespace Emulator6502
{
    public class Processor
    {
        public int ProgramCounter { get; set; } = 0x0;
        public int StackPointer { get; set; } = 0x0;
        public byte XRegister { get; set; } = 0x0;
        public byte YRegister { get; set; } = 0x0;
        public byte Accumulator { get; set; } = 0x0;

        public Operation CurrentOpCode { get; set; }
        public IInstruction CurrentInstruction { get; set; }

        public Logger Logger { get; set; }
        public Bus Bus { get; set; }

        #region flag properties

        public bool CarryFlag { get; set; }
        public bool ZeroFlag { get; set; }
        public bool DisableInterruptsFlag { get; set; }
        public bool BreakFlag {get; set;}
        public bool DecimalFlag { get; set; }
        public bool UnusedFlag { get; set; }
        public bool OverflowFlag { get; set; }
        public bool NegativeFlag { get; set; }

        #endregion

        public int CyclesLeft { get; set; }

        private List<IInstruction> instructions = [];

        private Dictionary<Operation, IInstruction> opcodeLookup = [];

        public Processor(bool addRAM = true)
        {
            // create logger
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("cpu.log")
                .MinimumLevel.Debug()
                .CreateLogger();

            ConfigureInstructions();

            Bus = new Bus();

            if (addRAM)
            {
                Bus.Devices.Add(new RAM());
            }
        }

        // resets the cpu to a known state
        public void Reset()
        {
            Logger.Debug("6502 reset");

            // set stack pointer correctly
            StackPointer = 0xFD;

            // load the address from the reset vector
            // set pc to reset vector address
            ProgramCounter = 0xFFFC;

            // reset pc to addres in the reset vector
            ProgramCounter = ReadMemoryValue(ProgramCounter) | (ReadMemoryValue(ProgramCounter + 1) << 8);

            // reset current opcode and instruction
            CurrentOpCode = null;
            CurrentInstruction = null;

            // TODO: add interrupts

            Logger.Debug("Reset complete");
        }

        public void LoadProgram(int offset, byte[] program, int initialProgramCounter, bool reset = true)
        {
            // load code into memory
            LoadProgram(offset, program);

            // exit if no reset
            if (!reset) return;

            // get parts of initial pc
            byte[] initialPcBytes = BitConverter.GetBytes(initialProgramCounter);

            // write bytes to reset vector
            WriteMemoryValue(0xFFFC, initialPcBytes[0]);
            WriteMemoryValue(0xFFFD, initialPcBytes[1]);
            
            // reset
            Reset();
        }

        public void LoadProgram(int offset, byte[] program)
        {
            // sanity checks to confirm code will fit
            if (offset > 0xFFFF)
            {
                throw new InvalidOperationException("Invalid offset - too large");
            }

            if (program.Length > 0xFFFF + offset)
            {
                throw new InvalidOperationException("Program size is larger than addressable space");
            }

            // loop over bytes and write them to memory
            for (int i = 0; i < program.Length; i++) WriteMemoryValue(i + offset, program[i]);
        }

        public void NextStep()
        {
            byte opcodeByte = ReadMemoryValue(ProgramCounter);

            var opcodeLookup = this.opcodeLookup.Where(op => op.Key.OpcodeByte == opcodeByte);

            if (opcodeLookup.Count() == 0)
            {
                throw new NotSupportedException($"Unsupported or illegal opcode {opcodeByte:X2}");
            }

            var opcode = opcodeLookup.Single();

            CurrentOpCode = opcode.Key;
            CurrentInstruction = opcode.Value;

            Logger.Information($"Executing instruction {CurrentOpCode.Name}");

            ProgramCounter++;

            CurrentInstruction.Execute(CurrentOpCode.OpcodeByte, CurrentOpCode.AddressingMode, this);

            // TODO: add interrupts
        }

        public ushort ReadIRQVector()
        {
            var highByte = ReadMemoryValue(0xFFFE);
            var lowByte = ReadMemoryValue(0xFFFF);

            return BitConverter.ToUInt16([highByte, lowByte]);
        }

        // Reads an address from memory via the bus
        public byte ReadMemoryValue(int address)
        {
            Log.Debug($"Read address {address:X4}");
            return Bus.Read(Math.Clamp(address, 0x0000, 0xFFFF));
        }

        // Writes some data to an address in memory via the bus
        public void WriteMemoryValue(int address, byte data)
        {
            Log.Debug($"Write value {data:X2} to address {address:X4}");
            
            Bus.Write(Math.Clamp(address, 0x0000, 0xFFFF), data);
        }

        // takes an addressing mode and reads an address for that instruction
        public int GetAddressByAddressingMode(AddressingMode addressingMode)
        {
            int address;
            int highByte;
            switch (addressingMode)
            {
                case AddressingMode.Absolute:
                    {
                        return ReadMemoryValue(ProgramCounter++) | (ReadMemoryValue(ProgramCounter++) << 8);
                    }
                case AddressingMode.AbsoluteX:
                    {
                        //Get the low half of the address
                        address = ReadMemoryValue(ProgramCounter++);

                        //Get the high byte
                        highByte = ReadMemoryValue(ProgramCounter++);

                        //We crossed a page boundary, so an extra read has occurred.
                        //However, if this is an ASL, LSR, DEC, INC, ROR, ROL or STA operation, we do not decrease it by 1.
                        if (address + XRegister > 0xFF)
                            switch (CurrentOpCode.OpcodeByte)
                            {
                                case 0x1E:
                                case 0xDE:
                                case 0xFE:
                                case 0x5E:
                                case 0x3E:
                                case 0x7E:
                                case 0x9D:
                                    {
                                        //This is a Read Fetch Write Operation, so we don't make the extra read.
                                        return (((highByte << 8) | address) + XRegister) & 0xFFFF;
                                    }
                                default:
                                    {
                                        ReadMemoryValue((((highByte << 8) | address) + XRegister - 0xFF) & 0xFFFF);
                                        break;
                                    }
                            }

                        return (((highByte << 8) | address) + XRegister) & 0xFFFF;
                    }
                case AddressingMode.AbsoluteY:
                    {
                        //Get the low half of the address
                        address = ReadMemoryValue(ProgramCounter++);

                        //Get the high byte
                        highByte = ReadMemoryValue(ProgramCounter++);

                        //We crossed a page boundary, so decrease the number of cycles by 1 if the operation is not STA
                        if (address + YRegister > 0xFF && CurrentOpCode.OpcodeByte != 0x99)
                            ReadMemoryValue((((highByte << 8) | address) + YRegister - 0xFF) & 0xFFFF);

                        //Bitshift the high byte into place, AND with $FFFF to handle wrapping.
                        return (((highByte << 8) | address) + YRegister) & 0xFFFF;
                    }
                case AddressingMode.Immediate:
                    {
                        return ProgramCounter++;
                    }
                case AddressingMode.IndirectX:
                    {
                        //Get the location of the address to retrieve
                        address = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(address);

                        address += XRegister;

                        //Now get the final Address. The is not a zero page address either.
                        var finalAddress = ReadMemoryValue(address & 0xFF) | (ReadMemoryValue((address + 1) & 0xFF) << 8);
                        return finalAddress;
                    }
                case AddressingMode.IndirectY:
                    {
                        address = ReadMemoryValue(ProgramCounter++);

                        var finalAddress = ReadMemoryValue(address) + (ReadMemoryValue((address + 1) & 0xFF) << 8);

                        if ((finalAddress & 0xFF) + YRegister > 0xFF && CurrentOpCode.OpcodeByte != 0x91)
                            ReadMemoryValue((finalAddress + YRegister - 0xFF) & 0xFFFF);

                        return (finalAddress + YRegister) & 0xFFFF;
                    }
                case AddressingMode.Relative:
                    {
                        return ProgramCounter;
                    }
                case AddressingMode.ZeroPage:
                    {
                        address = ReadMemoryValue(ProgramCounter++);
                        return address;
                    }
                case AddressingMode.ZeroPageX:
                    {
                        address = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(address);

                        address += XRegister;
                        address &= 0xFF;

                        //This address wraps if its greater than 0xFF
                        if (address > 0xFF)
                        {
                            address -= 0x100;
                            return address;
                        }

                        return address;
                    }
                case AddressingMode.ZeroPageY:
                    {
                        address = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(address);

                        address += YRegister;
                        address &= 0xFF;

                        return address;
                    }
                default:
                    throw new InvalidOperationException(
                        $"The Address Mode '{addressingMode}' does not require an address");
            }
        }

        // if value passed is zero set zero flag to true
        public void SetZeroFlagByResult(byte result)
        {
            Log.Debug($"Zero flag set to {result == 0}");
            ZeroFlag = result == 0;
        }

        // sets the negative flag if the intager is greater than 127 - thats how 6502 handles negatives
        // also means if 8th bit is set (same thing)
        public void SetNegativeFlagByResult(byte result)
        {
            Log.Debug($"Negative flag set to {result > 127}");
            NegativeFlag = result > 127;
        }

        // sets program counter to a relative address
        public void MoveProgramCounterByRelativeValue(int value)
        {
            // converts 6502-style negative number to c#-style negative
            int signedOffset = value > 127 ? value - 255 : value;

            Console.WriteLine("Offset pc by " + signedOffset);

            // calculates new program counter based on offset
            int newProgramCounter = ProgramCounter + signedOffset;

            if (newProgramCounter < 0)
            {
                newProgramCounter = 0xFFFF - newProgramCounter - 1;
            }

            // makes sure you always land on the correct spot for a positive number
            if (signedOffset >= 0)
            {
                newProgramCounter++;
            }

            // TODO: crossed page boundy then increment cycles

            ProgramCounter = newProgramCounter;
            ReadMemoryValue(ProgramCounter);
        }

        // writes a value onto the stack without modifying the sp
        public void PokeStack(byte value)
        {
            Bus.Write(StackPointer + 0x100, value);
        }     

        // read a value from the sack without changing the stack pointer
        public byte PeekStack()
        {
            return Bus.Read(StackPointer + 0x100);
        }
        
        public byte GetFlagByte()
        {
            byte sr = 0x0;

            sr.SetFlag(Flags.CarryBit, CarryFlag);
            sr.SetFlag(Flags.Zero, ZeroFlag);
            sr.SetFlag(Flags.DisableInterrupts, DisableInterruptsFlag);
            sr.SetFlag(Flags.Break, BreakFlag);
            sr.SetFlag(Flags.Decimal, DecimalFlag);
            sr.SetFlag(Flags.Unused, UnusedFlag);
            sr.SetFlag(Flags.Overflow, OverflowFlag);
            sr.SetFlag(Flags.Negative, NegativeFlag);

            return sr;
        }

        private void ConfigureInstructions()
        {
            // get all instructions from classes that extend IInstruction
            var targetInterface = typeof(IInstruction);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => targetInterface.IsAssignableFrom(p) && !p.IsInterface);

            foreach (Type type in types)
            {
                Logger.Debug($"Found instruction {type.Name}");

                // create instance of instruction
                var instance = Activator.CreateInstance(type) as IInstruction;

                if (instance is null)
                {
                    throw new Exception("Error creating instruction " + type.Name);
                }

                // add to list
                instructions.Add(instance);
            }

            foreach (var instruction in instructions)
            {
                foreach (var opcode in instruction.Opcodes)
                {
                    opcodeLookup.Add(opcode, instruction);
                }
            }
        }
        
        // stack helper methods
        // push single byte to stack
        public void PushStack(byte value)
        {
            WriteMemoryValue(0x100 + StackPointer--, value);
        }

        // push word to stack
        public void PushStack(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            WriteMemoryValue(0x100 + StackPointer--, bytes[1]);
            WriteMemoryValue(0x100 + StackPointer--, bytes[0]);
        }

        
        // push program counter to stack
        public void PushPCToStack()
        {
            PushStack((ushort)(ProgramCounter + 1));
        }

        // push status register to stack
        public void PushSRToStack()
        {
            PushStack(GetFlagByte());
        }

        // pop byte from stack
        public byte PopStackByte()
        {
            return ReadMemoryValue(0x100 + StackPointer++);
        }

        // pop word from stack
        public ushort PopStackWord()
        {
            byte[] bytes = [ReadMemoryValue(0x100 + StackPointer++), ReadMemoryValue(0x100 + StackPointer++)];
            return BitConverter.ToUInt16(bytes);
        }
    }
}
