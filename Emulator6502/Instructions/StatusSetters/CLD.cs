namespace Emulator6502.Instructions.StatusSetters
{
    // clear decimal flag
    public class CLD : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLD", AddressingMode.Implied, OpcodeEnum.CLD, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.DecimalFlag = false;
        }
    }
}
