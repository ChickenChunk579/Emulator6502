﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Decrement
{
    // decrement memory by one
    public class DEC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("DEC", AddressingMode.ZeroPage, OpcodeEnum.DEC_ZP, 5),
            new Operation("DEC", AddressingMode.ZeroPageX, OpcodeEnum.DEC_ZPX, 6),

            new Operation("DEC", AddressingMode.Absolute, OpcodeEnum.DEC_ABS, 6),
            new Operation("DEC", AddressingMode.AbsoluteX, OpcodeEnum.DEC_ABSX, 7),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int addressToDec = cpu.GetAddressByAddressingMode(addressingMode);
            byte result = (byte)(cpu.ReadMemoryValue(addressToDec) - 1);

            cpu.WriteMemoryValue(addressToDec, result);

            cpu.SetNegativeFlagByResult(result);
            cpu.SetZeroFlagByResult(result);
        }
    }
}
