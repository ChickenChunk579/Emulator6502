using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Comparison
{
    // compare with accumulator
    public class CMP : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CMP", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.CMP_IMM, 2),
            new Operation("CMP", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.CMP_ZP, 3),
            new Operation("CMP", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.CMP_ZPX, 4),

            new Operation("CMP", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.CMP_ABS, 4),
            new Operation("CMP", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.CMP_ABSX, 4),
            new Operation("CMP", AddressModeExtensions.GetAbsoluteYAddress, OpcodeEnum.CMP_ABSY, 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            Common.Compare(operation, cpu, cpu.Accumulator);
        }
    }
}
