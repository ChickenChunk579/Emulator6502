using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Loads
{
    public class LDA : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDA", AddressModeExtensions.GetIndirectXAddress, OpcodeEnum.LDA_INDX, 6),
                new Operation("LDA", AddressModeExtensions.GetIndirectYAddress, OpcodeEnum.LDA_INDY, 5),

                new Operation("LDA", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.LDA_ZP, 3),
                new Operation("LDA", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.LDA_ZPX, 4),

                new Operation("LDA", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.LDA_ABSX, 4),
                new Operation("LDA", AddressModeExtensions.GetAbsoluteYAddress, OpcodeEnum.LDA_ABSY, 4),

                new Operation("LDA", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.LDA_ABS, 4),
                new Operation("LDA", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.LDA_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            // Read in byte from memory and set accumulator value to it
            cpu.Accumulator = cpu.ReadMemoryValue(address);

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
