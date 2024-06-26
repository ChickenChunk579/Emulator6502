﻿using System.Reflection.Emit;

namespace Emulator6502.Instructions.Bitwise
{
    public class BIT : IInstruction
    {
        
        public List<Operation> Opcodes => new List<Operation>()
        {
          
            new Operation("BIT", AddressingMode.ZeroPage, OpcodeEnum.BIT_ZP, 3),

            new Operation("BIT", AddressingMode.Absolute, OpcodeEnum.BIT_ABS , 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            byte memoryValue = cpu.ReadMemoryValue(cpu.GetAddressForOperation(operation));
            int valueToCompare = memoryValue & cpu.Accumulator;

            cpu.SR.OverflowFlag = (memoryValue & 0x40) != 0;

            cpu.SR.SetNegativeFlagByResult(memoryValue);
            cpu.SR.SetZeroFlagByResult((byte)valueToCompare);
        }
    }
}
