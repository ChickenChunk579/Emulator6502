namespace Emulator6502.Instructions.StatusSetters
{
    // clear decimal flag
    public class CLD : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLD", AddressingMode.Implied, OpcodeEnum.CLD, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.SR.DecimalFlag = false;
        }
    }
}
