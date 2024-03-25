using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Branches
{
    // branch overflow set
    public class BVS : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BVS", AddressModeExtensions.GetRelativeAddress, OpcodeEnum.BVS, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if overflow is set
            if (cpu.SR.OverflowFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
