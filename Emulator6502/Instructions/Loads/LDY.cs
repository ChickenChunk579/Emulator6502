using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Loads
{
    public class LDY : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDY", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.LDY_ZP, 3),

                new Operation("LDY", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.LDY_ABSX, 4),

                new Operation("LDY", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.LDY_ABS, 4),
                new Operation("LDY", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.LDY_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            // Read in byte from memory and set accumulator value to it
            cpu.YRegister = cpu.ReadMemoryValue(address);

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.YRegister);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.YRegister);
        }
    }
}
