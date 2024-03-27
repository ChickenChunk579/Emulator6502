using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Decrement
{
    // decrement y by one
    public class DEY : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("DEY", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.DEY, 2),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            int valueToDec = cpu.YRegister;
            byte result = (byte)(valueToDec - 1);

            cpu.YRegister = result;

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
