namespace Emulator6502.Instructions.StatusSetters
{
    // clear carry flag
    public class CLV : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLV", AddressingMode.Implied, OpcodeEnum.CLV, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.OverflowFlag = false;
        }
    }
}
