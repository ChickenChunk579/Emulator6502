namespace Emulator6502.Instructions.StatusSetters
{
    // set carry
    public class SEC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("SEC", AddressingMode.Implied, OpcodeEnum.SEC, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.SR.CarryFlag = true;
        }
    }
}
