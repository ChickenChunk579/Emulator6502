namespace Emulator6502.Instructions.StatusSetters
{
    // clear carry flag
    public class CLC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLC", AddressingMode.Implied, OpcodeEnum.CLC, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.SR.CarryFlag = false;
        }
    }
}
