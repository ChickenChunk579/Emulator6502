namespace Emulator6502.Instructions.Branches
{
    // branch result not zero (not equal)
    public class BNE : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BNE", AddressingMode.Relative, OpcodeEnum.BNE, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is clear
            if (!cpu.ZeroFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
