namespace Emulator6502.Instructions.StatusSetters
{
    // set interrupds
    public class SEI : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("SEI", AddressingMode.Implied, OpcodeEnum.SEI, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.DisableInterruptsFlag = true;
        }
    }
}
