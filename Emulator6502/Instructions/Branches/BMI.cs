﻿namespace Emulator6502.Instructions.Branches
{
    // branch negative
    public class BMI : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("BMI", AddressingMode.Relative, OpcodeEnum.BMI, 2, 1)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // check if carry bit is clear
            if (cpu.NegativeFlag)
            {
                // perform branch
                Common.PerformBranch(opcode, addressingMode, cpu);
            }
        }
    }
}
