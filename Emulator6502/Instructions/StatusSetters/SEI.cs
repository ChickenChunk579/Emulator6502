namespace Emulator6502.Instructions.StatusSetters
{
    // set interrupds
    public class SEI : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("SEI", AddressingMode.Implied, 0x78, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.DisableInterruptsFlag = true;
        }
    }
}
