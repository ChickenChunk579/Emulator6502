using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Decrement
{
    // decrement memory by one
    public class DEX : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("DEX", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.DEX, 2),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            int valueToDec = cpu.XRegister;
            byte result = (byte)(valueToDec - 1);

            cpu.XRegister = result;

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
