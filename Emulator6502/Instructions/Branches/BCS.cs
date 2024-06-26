﻿namespace Emulator6502.Instructions.Branches
{
    // branch carry set
    public class BCS : IInstruction
    {
        // 
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BCS", AddressingMode.Relative, OpcodeEnum.BCS, 2, 1)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            // check if carry bit is set
            if (cpu.SR.CarryFlag)
            {
                // perform branch
                Common.PerformBranch(operation, cpu);
            }
        }
    }
}
