namespace Emulator6502.Instructions
{
    public interface IInstruction
    {
        public List<Operation> Opcodes { get; }

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu);
    }
}
