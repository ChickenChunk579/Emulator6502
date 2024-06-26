﻿namespace Emulator6502.Instructions.Branches
{
    // branch equal / zero
    public class BEQ : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BEQ", AddressingMode.Relative, OpcodeEnum.BEQ, 2, 1)
        };

        // branches if zero (equal) is set
        public void Execute(Operation operation, Processor cpu)
        {
            // check if carry bit is set
            if (cpu.SR.ZeroFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
