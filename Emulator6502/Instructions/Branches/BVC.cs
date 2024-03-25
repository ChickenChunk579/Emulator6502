namespace Emulator6502.Instructions.Branches
{
    // branch overflow clear
    public class BVC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BVC", AddressingMode.Relative, OpcodeEnum.BVC, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if overflow is clear
            if (!cpu.SR.OverflowFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
