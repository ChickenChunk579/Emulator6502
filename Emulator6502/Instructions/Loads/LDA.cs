using System.Reflection.Emit;

namespace Emulator6502.Instructions.Loads
{
    public class LDA : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDA", AddressingMode.IndirectX, OpcodeEnum.LDA_INDX, 6),
                new Operation("LDA", AddressingMode.IndirectY, OpcodeEnum.LDA_INDY, 5),

                new Operation("LDA", AddressingMode.ZeroPage, OpcodeEnum.LDA_ZP, 3),
                new Operation("LDA", AddressingMode.ZeroPageX, OpcodeEnum.LDA_ZPX, 4),

                new Operation("LDA", AddressingMode.AbsoluteX, OpcodeEnum.LDA_ABSX, 4),
                new Operation("LDA", AddressingMode.AbsoluteY, OpcodeEnum.LDA_ABSY, 4),

                new Operation("LDA", AddressingMode.Absolute, OpcodeEnum.LDA_ABS, 4),
                new Operation("LDA", AddressingMode.Immediate, OpcodeEnum.LDA_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {

            // Read in byte from memory and set accumulator value to it
            cpu.Accumulator = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(operation.AddressingMode));

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
