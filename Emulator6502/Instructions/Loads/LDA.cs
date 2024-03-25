namespace Emulator6502.Instructions.Loads
{
    public class LDA : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Opcode> Opcodes
        {
            get => new List<Opcode>()
            {
                new Opcode("LDA", AddressingMode.IndirectX, 0xA1, 6),
                new Opcode("LDA", AddressingMode.IndirectY, 0xB1, 5),

                new Opcode("LDA", AddressingMode.ZeroPage, 0xA5, 3),
                new Opcode("LDA", AddressingMode.ZeroPageX, 0xB5, 4),

                new Opcode("LDA", AddressingMode.AbsoluteX, 0xBD, 4),
                new Opcode("LDA", AddressingMode.AbsoluteY, 0xB9, 4),

                new Opcode("LDA", AddressingMode.Absolute, 0xAD, 4),
                new Opcode("LDA", AddressingMode.Immediate, 0xA9, 2),

            };
        }

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {

            // Read in byte from memory and set accumulator value to it
            cpu.Accumulator = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));

            // Set zero flag based on value of accumulator
            cpu.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
