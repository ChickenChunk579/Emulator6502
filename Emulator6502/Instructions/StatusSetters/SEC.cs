using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.StatusSetters
{
    // set carry
    public class SEC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("SEC", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.SEC, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.CarryFlag = true;
        }
    }
}
