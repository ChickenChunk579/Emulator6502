using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Branches
{
    // branch overflow clear
    public class BVC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BVC", AddressModeExtensions.GetRelativeAddress, OpcodeEnum.BVC, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if overflow is clear
            if (!cpu.SR.OverflowFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
