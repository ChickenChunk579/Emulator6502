namespace Emulator6502.Instructions.Interrupts
{
    public class BRK : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BRK", AddressingMode.Implied, OpcodeEnum.BRK, 7)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            var resetVector = cpu.ReadIRQVector();

            cpu.PushPCToStack();

            cpu.SR.BreakFlag = true;
            cpu.SR.UnusedFlag = true;

            cpu.PushSRToStack();

            cpu.SR.DisableInterruptsFlag = true;
             
            cpu.ProgramCounter = resetVector;            
        }
    }
}
