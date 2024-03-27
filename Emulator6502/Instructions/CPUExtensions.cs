namespace Emulator6502.Instructions
{
    public static class AddressModeExtensions
    {
        public static int GetAbsoluteAddress(Processor cpu, Operation operation)
        {
            return cpu.ReadMemoryValue(cpu.ProgramCounter++) | (cpu.ReadMemoryValue(cpu.ProgramCounter++) << 8);
        }

        public static int GetImpliedAddress(Processor cpu, Operation operation)
        {
            throw new NotImplementedException();
        }

        public static int GetAbsoluteXAddress(Processor cpu, Operation operation)
        {

            int lowByte;
            int highByte;

            //Get the low byte
            lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);

            //Get the high byte
            highByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);

            //We crossed a page boundary, so an extra read has occurred.
            //However, if this is an ASL, LSR, DEC, INC, ROR, ROL or STA operation, we do not decrease it by 1.
            if (lowByte + cpu.XRegister > 0xFF)
                switch (cpu.CurrentOperation.OpcodeEnum)
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
                            return (((highByte << 8) | lowByte) + cpu.XRegister) & 0xFFFF;
                        }
                    default:
                        {
                            cpu.ReadMemoryValue((((highByte << 8) | lowByte) + cpu.XRegister - 0xFF) & 0xFFFF);
                            break;
                        }
                }

            return (((highByte << 8) | lowByte) + cpu.XRegister) & 0xFFFF;
        }

        public static int GetAbsoluteYAddress(Processor cpu, Operation operation)
        {
            int lowByte;
            int highByte;

            //Get the low half of the address
            lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);

            //Get the high byte
            highByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);

            //We crossed a page boundary, so decrease the number of cycles by 1 if the operation is not STA
            if (lowByte + cpu.YRegister > 0xFF && cpu.CurrentOperation.OpcodeEnum != OpcodeEnum.STA_ABSY)
                cpu.ReadMemoryValue((((highByte << 8) | lowByte) + cpu.YRegister - 0xFF) & 0xFFFF);

            //Bitshift the high byte into place, AND with $FFFF to handle wrapping.
            return (((highByte << 8) | lowByte) + cpu.YRegister) & 0xFFFF;
        }

        public static int GetImmediateAddress(Processor cpu, Operation operation)
        {
            return cpu.ProgramCounter++;
        }

        public static int GetIndirectXAddress(Processor cpu, Operation operation)
        {
            int lowByte;
            int highByte;

            //Get the location of the address to retrieve
            lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);
            cpu.ReadMemoryValue(lowByte);

            lowByte += cpu.XRegister;

            //Now get the final Address. The is not a zero page address either.
            var finalAddress = cpu.ReadMemoryValue(lowByte & 0xFF) | (cpu.ReadMemoryValue((lowByte + 1) & 0xFF) << 8);
            
            return finalAddress;
        }

        public static int GetIndirectYAddress(Processor cpu, Operation operation)
        {
            int lowByte;
            int highByte;

            lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);

            var finalAddress = cpu.ReadMemoryValue(lowByte) + (cpu.ReadMemoryValue((lowByte + 1) & 0xFF) << 8);

            if ((finalAddress & 0xFF) + cpu.YRegister > 0xFF && cpu.CurrentOperation.OpcodeByte != 0x91)
                cpu.ReadMemoryValue((finalAddress + cpu.YRegister - 0xFF) & 0xFFFF);

            return (finalAddress + cpu.YRegister) & 0xFFFF;
        }

        public static int GetRelativeAddress(Processor cpu, Operation operation)
        {
            return cpu.ProgramCounter;
        }

        public static int GetZeroPageAddress(Processor cpu, Operation operation)
        {
            int lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);
            
            return lowByte;
        }

        public static int GetZeroPageXAddress(Processor cpu, Operation operation)
        {
            int lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);
            cpu.ReadMemoryValue(lowByte);

            lowByte += cpu.XRegister;
            lowByte &= 0xFF;

            //This address wraps if its greater than 0xFF
            if (lowByte > 0xFF)
            {
                lowByte -= 0x100;
                return lowByte;
            }

            return lowByte;
        }

        public static int GetZeroPageYAddress(Processor cpu, Operation operation)
        {
            int lowByte = cpu.ReadMemoryValue(cpu.ProgramCounter++);
            cpu.ReadMemoryValue(lowByte);

            lowByte += cpu.YRegister;
            lowByte &= 0xFF;

            return lowByte;
        }
    }
}
