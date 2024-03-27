using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Arithmatic
{
    public class ADC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("ADC", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.ADC_ABS, 4),
            new Operation("ADC", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.ADC_ABSX, 4),
            new Operation("ADC", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.ADC_ABSY, 4),

            new Operation("ADC", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.ADC_IMM, 2),

            new Operation("ADC", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.ADC_ZP, 3),
            new Operation("ADC", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.ADC_ZPX, 4),

            new Operation("ADC", AddressModeExtensions.GetIndirectXAddress, OpcodeEnum.ADC_INDX, 6),
            new Operation("ADC", AddressModeExtensions.GetIndirectXAddress, OpcodeEnum.ADC_INDX, 5),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            // read operand
            var memoryValue = cpu.ReadMemoryValue(address);

            // perform calculation
            var newValue = memoryValue + cpu.Accumulator + (cpu.SR.CarryFlag ? 1 : 0);

            // set overflow flag
            cpu.SR.OverflowFlag = ((cpu.Accumulator ^ newValue) & 0x80) != 0 && ((cpu.Accumulator ^ memoryValue) & 0x80) == 0;
        
            if (cpu.SR.DecimalFlag)
            {
                // TODO: Add decimal mode
            } else
            {
                // set carry flag
                if (newValue > 255)
                {
                    cpu.SR.CarryFlag = true;
                    newValue -= 256;
                } else
                {
                    cpu.SR.CarryFlag = false;
                }
            }

            // set zero and negative flags
            cpu.SR.SetNegativeFlagByResult((byte)newValue);
            cpu.SR.SetZeroFlagByResult((byte)newValue);

            // set accumulator to result
            cpu.Accumulator = (byte)newValue;
        }
    }
}
