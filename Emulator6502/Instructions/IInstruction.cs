namespace Emulator6502.Instructions
{
    public interface IInstruction
    {
        public List<Operation> Opcodes { get; }

        public void Execute(Operation operation, Processor cpu);
    }
}
