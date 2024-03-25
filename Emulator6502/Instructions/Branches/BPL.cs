namespace Emulator6502.Instructions.Branches
{
    // branch negative clear
    public class BPL : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BPL", AddressingMode.Relative, OpcodeEnum.BPL, 2, 1),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is clear
            if (!cpu.SR.NegativeFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
