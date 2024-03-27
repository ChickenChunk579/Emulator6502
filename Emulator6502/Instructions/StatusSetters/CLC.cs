﻿using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.StatusSetters
{
    // clear carry flag
    public class CLC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>
        {
            new Operation("CLC", AddressModeExtensions.GetImpliedAddress, OpcodeEnum.CLC, 2)
        };

        public void Execute(Operation operation, Processor cpu)
        {
            cpu.SR.CarryFlag = false;
        }
    }
}
