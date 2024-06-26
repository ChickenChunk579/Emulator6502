﻿namespace Emulator6502.Instructions.Branches
{
    // branch result not zero (not equal)
    public class BNE : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BNE", AddressingMode.Relative, OpcodeEnum.BNE, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if carry bit is clear
            if (!cpu.SR.ZeroFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
