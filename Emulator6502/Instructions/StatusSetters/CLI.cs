namespace Emulator6502.Instructions.StatusSetters
{
    // clear interrupts flag
    public class CLI : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("CLI", AddressingMode.Implied, 0x58, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.DisableInterruptsFlag = false;
        }
    }
}
