﻿using System.Reflection.Emit;

namespace Emulator6502.Instructions.Loads
{
    public class LDX : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Operation> Opcodes
        {
            get => new List<Operation>()
            {
                new Operation("LDX", AddressingMode.ZeroPage, OpcodeEnum.LDX_ZP, 3),

                new Operation("LDX", AddressingMode.AbsoluteY, OpcodeEnum.LDX_ABSY, 4),

                new Operation("LDX", AddressingMode.Absolute, OpcodeEnum.LDX_ABS, 4),
                new Operation("LDX", AddressingMode.Immediate, OpcodeEnum.LDX_IMM, 2),
            };
        }

        public void Execute(Operation operation, Processor cpu)
        {

            // Read in byte from memory and set accumulator value to it
            cpu.XRegister = cpu.ReadMemoryValue(cpu.GetAddressForOperation(operation));

            // Set zero flag based on value of accumulator
            cpu.SR.SetZeroFlagByResult(cpu.XRegister);

            // Set zero flag based on value of accumulator
            cpu.SR.SetNegativeFlagByResult(cpu.XRegister);
        }
    }
}
