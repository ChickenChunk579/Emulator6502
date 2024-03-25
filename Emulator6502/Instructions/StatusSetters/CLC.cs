namespace Emulator6502.Instructions.StatusSetters
{
    // clear carry flag
    public class CLC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLC", AddressingMode.Implied, OpcodeEnum.CLC, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.CarryFlag = false;
        }
    }
}
