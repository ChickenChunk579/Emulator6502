namespace Emulator6502.Instructions.Branches
{
    // branch carry clear
    public class BCC : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("BCC", AddressingMode.Relative, 0x90, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is clear
            if (!cpu.CarryFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
