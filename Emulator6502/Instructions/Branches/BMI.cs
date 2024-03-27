using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Branches
{
    // branch negative
    public class BMI : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BMI", AddressModeExtensions.GetRelativeAddress, OpcodeEnum.BMI, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if carry bit is clear
            if (cpu.SR.NegativeFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
