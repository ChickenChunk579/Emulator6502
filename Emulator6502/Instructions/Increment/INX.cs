using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Decrement
{
    // decrement memory by one
    public class INX : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("INX", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.INX, 2),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            int valueToInc = cpu.XRegister;
            byte result = (byte)(valueToInc + 1);

            cpu.XRegister = result;

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
