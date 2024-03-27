using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.StatusSetters
{
    // clear carry flag
    public class CLV : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLV", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.CLV, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.OverflowFlag = false;
        }
    }
}
