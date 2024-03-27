using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.StatusSetters
{
    // clear interrupts flag
    public class CLI : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLI", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.CLI, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.DisableInterruptsFlag = false;
        }
    }
}
