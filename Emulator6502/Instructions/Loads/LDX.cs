using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Loads
{
    public class LDX : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDX", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.LDX_ZP, 3),

                new Operation("LDX", AddressModeExtensions.GetAbsoluteYAddress, OpcodeEnum.LDX_ABSY, 4),

                new Operation("LDX", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.LDX_ABS, 4),
                new Operation("LDX", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.LDX_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            // Read in byte from memory and set accumulator value to it
            cpu.XRegister = cpu.ReadMemoryValue(address);

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
