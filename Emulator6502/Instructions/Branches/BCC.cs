﻿namespace Emulator6502.Instructions.Branches
{
    // branch carry clear
    public class BCC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BCC", AddressingMode.Relative, OpcodeEnum.BCC, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if carry bit is clear
            if (!cpu.SR.CarryFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
