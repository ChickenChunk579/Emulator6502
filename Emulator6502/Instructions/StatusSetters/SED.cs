namespace Emulator6502.Instructions.StatusSetters
{
    // set decimal
    public class SED : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("SED", AddressingMode.Implied, OpcodeEnum.SED, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.DecimalFlag = true;
        }
    }
}
