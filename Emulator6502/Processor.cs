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

        public StatusRegister SR {get; set;} = new();

        public Operation CurrentOperation { get; set; }
        public IInstruction CurrentInstruction { get; set; }

        public Logger Logger { get; set; }
        public Bus Bus { get; set; }

        public int Cycles { get; set; } = 0;

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
            CurrentOperation = null;
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
            Cycle();

            do { Cycle(); } while (Cycles != 0);
        }

        // Performs a cycle
        public void Cycle()
        {
            if (Cycles == 0)
            {
                byte opcodeByte = ReadMemoryValue(ProgramCounter);

                var opcodeLookup = this.opcodeLookup.Where(op => op.Key.OpcodeByte == opcodeByte);

                if (opcodeLookup.Count() == 0)
                {
                    throw new NotSupportedException($"Unsupported or illegal opcode {opcodeByte:X2}");
                }

                var opcode = opcodeLookup.Single();

                CurrentOperation = opcode.Key;
                CurrentInstruction = opcode.Value;
                Cycles = CurrentOperation.Cycles;

                Logger.Information($"Executing instruction {CurrentOperation.Name}");

                ProgramCounter++;

                CurrentInstruction.Execute(CurrentOperation, this);

                // TODO: add interrupts
            } else
            {
                Cycles--;
            }
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
        public int GetAddressForOperation(Operation operation)
        {
            int lowByte;
            int highByte;

            switch (operation.AddressingMode)
            {
                case AddressingMode.Absolute:
                    {
                        return ReadMemoryValue(ProgramCounter++) | (ReadMemoryValue(ProgramCounter++) << 8);
                    }
                case AddressingMode.AbsoluteX:
                    {
                        //Get the low byte
                        lowByte = ReadMemoryValue(ProgramCounter++);

                        //Get the high byte
                        highByte = ReadMemoryValue(ProgramCounter++);

                        //We crossed a page boundary, so an extra read has occurred.
                        //However, if this is an ASL, LSR, DEC, INC, ROR, ROL or STA operation, we do not decrease it by 1.
                        if (lowByte + XRegister > 0xFF)
                            switch (CurrentOperation.OpcodeEnum)
                            {
                                case OpcodeEnum.ASL_ABSX:
                                case OpcodeEnum.DEC_ABSX:
                                case OpcodeEnum.INC_ABSX:
                                case OpcodeEnum.LSR_ABSX:
                                case OpcodeEnum.ROR_ABSX:
                                case OpcodeEnum.ROL_ABSX:
                                case OpcodeEnum.STA_ABSX:
                                    {
                                        //This is a Read Fetch Write Operation, so we don't make the extra read.
                                        return (((highByte << 8) | lowByte) + XRegister) & 0xFFFF;
                                    }
                                default:
                                    {
                                        Console.WriteLine("ABSX Extra cycle");
                                        Cycles++;
                                        //ReadMemoryValue((((highByte << 8) | lowByte) + XRegister - 0xFF) & 0xFFFF);
                                        break;
                                    }
                            }

                        return (((highByte << 8) | lowByte) + XRegister) & 0xFFFF;
                    }
                case AddressingMode.AbsoluteY:
                    {
                        //Get the low half of the address
                        lowByte = ReadMemoryValue(ProgramCounter++);

                        //Get the high byte
                        highByte = ReadMemoryValue(ProgramCounter++);

                        //We crossed a page boundary, so decrease the number of cycles by 1 if the operation is not STA
                        if (lowByte + YRegister > 0xFF && CurrentOperation.OpcodeEnum != OpcodeEnum.STA_ABSY)
                        {
                            //ReadMemoryValue((((highByte << 8) | lowByte) + YRegister - 0xFF) & 0xFFFF);
                            Cycles++;
                            Console.WriteLine("ABSX Extra cycle");
                        }

                        //Bitshift the high byte into place, AND with $FFFF to handle wrapping.
                        return (((highByte << 8) | lowByte) + YRegister) & 0xFFFF;
                    }
                case AddressingMode.Immediate:
                    {
                        return ProgramCounter++;
                    }
                case AddressingMode.IndirectX:
                    {
                        //Get the location of the address to retrieve
                        lowByte = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(lowByte);

                        lowByte += XRegister;

                        //Now get the final Address. The is not a zero page address either.
                        var finalAddress = ReadMemoryValue(lowByte & 0xFF) | (ReadMemoryValue((lowByte + 1) & 0xFF) << 8);
                        return finalAddress;
                    }
                case AddressingMode.IndirectY:
                    {
                        lowByte = ReadMemoryValue(ProgramCounter++);

                        var finalAddress = ReadMemoryValue(lowByte) + (ReadMemoryValue((lowByte + 1) & 0xFF) << 8);

                        if ((finalAddress & 0xFF) + YRegister > 0xFF && CurrentOperation.OpcodeByte != 0x91)
                            ReadMemoryValue((finalAddress + YRegister - 0xFF) & 0xFFFF);

                        return (finalAddress + YRegister) & 0xFFFF;
                    }
                case AddressingMode.Relative:
                    {
                        return ProgramCounter;
                    }
                case AddressingMode.ZeroPage:
                    {
                        lowByte = ReadMemoryValue(ProgramCounter++);
                        return lowByte;
                    }
                case AddressingMode.ZeroPageX:
                    {
                        lowByte = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(lowByte);

                        lowByte += XRegister;
                        lowByte &= 0xFF;

                        //This address wraps if its greater than 0xFF
                        if (lowByte > 0xFF)
                        {
                            lowByte -= 0x100;
                            return lowByte;
                        }

                        return lowByte;
                    }
                case AddressingMode.ZeroPageY:
                    {
                        lowByte = ReadMemoryValue(ProgramCounter++);
                        ReadMemoryValue(lowByte);

                        lowByte += YRegister;
                        lowByte &= 0xFF;

                        return lowByte;
                    }
                default:
                    throw new InvalidOperationException(
                        $"The Address Mode '{operation.AddressingMode}' does not require an address");
            }
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

            // check for pass page boundry
            Console.WriteLine("Old page: " + (ProgramCounter & 0xFF00).ToString("X2"));
            Console.WriteLine("New page: " + (newProgramCounter & 0xFF00).ToString("X2"));
            Console.WriteLine("Old PC: " + ProgramCounter.ToString("X4"));
            Console.WriteLine("New PC: " + newProgramCounter.ToString("X4"));
            if ((ProgramCounter & 0xFF00) != (newProgramCounter & 0xFF00))
            {
                // The branch crosses a page boundary.
                Console.WriteLine("Extra cycle");
                Cycles++;
            }

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
            PushStack(SR.Value);
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
