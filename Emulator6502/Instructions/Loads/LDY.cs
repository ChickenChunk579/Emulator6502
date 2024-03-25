using System.Reflection.Emit;

namespace Emulator6502.Instructions.Loads
{
    public class LDY : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDY", AddressingMode.ZeroPage, OpcodeEnum.LDY_ZP, 3),

                new Operation("LDY", AddressingMode.AbsoluteX, OpcodeEnum.LDY_ABSX, 4),

                new Operation("LDY", AddressingMode.Absolute, OpcodeEnum.LDY_ABS, 4),
                new Operation("LDY", AddressingMode.Immediate, OpcodeEnum.LDY_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {

            // Read in byte from memory and set accumulator value to it
            cpu.YRegister = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(operation.AddressingMode));

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
