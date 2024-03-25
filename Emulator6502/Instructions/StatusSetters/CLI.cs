namespace Emulator6502.Instructions.StatusSetters
{
    // clear interrupts flag
    public class CLI : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLI", AddressingMode.Implied, OpcodeEnum.CLI, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.DisableInterruptsFlag = false;
        }
    }
}
