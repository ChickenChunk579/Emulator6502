namespace Emulator6502.Instructions.Interrupts
{
    public class BRK : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("BRK", AddressingMode.Implied, 0x00, 7)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            var resetVector = cpu.ReadIRQVector();

            var bytes = BitConverter.GetBytes(cpu.ProgramCounter+1);
            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, bytes[1]);
            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, bytes[0]);

            cpu.BreakFlag = true;

            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, cpu.GetFlagByte());

            cpu.DisableInterruptsFlag = true;
             
            cpu.ProgramCounter = resetVector;            
        }
    }
}
