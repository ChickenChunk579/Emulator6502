namespace Emulator6502.Instructions.Branches
{
    // branch overflow set
    public class BVS : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BVS", AddressingMode.Relative, OpcodeEnum.BVS, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if overflow is set
            if (cpu.OverflowFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
