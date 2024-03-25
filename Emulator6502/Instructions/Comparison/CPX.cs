using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Comparison
{
    // compare with x
    public class CPX : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CPX", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.CPX_IMM, 2),
            new Operation("CPX", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.CPX_ZP, 3),

            new Operation("CPX", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.CPX_ABS, 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            Common.Compare(operation, cpu, cpu.XRegister);
        }
    }
}
