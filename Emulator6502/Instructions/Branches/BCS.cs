namespace Emulator6502.Instructions.Branches
{
    // branch carry set
    public class BCS : IInstruction
    {
        // 
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("BCS", AddressingMode.Relative, 0xB0, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is set
            if (cpu.CarryFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
