using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Comparison
{
    // compare with y
    public class CPY : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CPY", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.CPY_IMM, 2),
            new Operation("CPY", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.CPY_ZP, 3),

            new Operation("CPY", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.CPY_ABS, 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            Common.Compare(operation, cpu, cpu.YRegister);
        }
    }
}
