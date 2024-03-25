namespace Emulator6502.Instructions.Branches
{
    // branch negative
    public class BMI : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("BMI", AddressingMode.Relative, 0x30, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is clear
            if (cpu.NegativeFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
