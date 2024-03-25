namespace Emulator6502.Instructions.StatusSetters
{
    // set decimal
    public class SED : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("SED", AddressingMode.Implied, 0xF8, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.DecimalFlag = true;
        }
    }
}
