namespace Emulator6502.Instructions
{
    public interface IInstruction
    {
        public List<Opcode> Opcodes { get; }

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu);
    }
}
