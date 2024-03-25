using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Bitwise
{
    public class BIT : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>()
        {
            new Opcode("BIT", AddressingMode.ZeroPage, 0x24, 3),

            new Opcode("BIT", AddressingMode.Absolute, 0x2C, 4),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            byte memoryValue = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));
            int valueToCompare = memoryValue & cpu.Accumulator;

            cpu.OverflowFlag = (memoryValue & 0x40) != 0;

            cpu.SetNegativeFlagByResult(memoryValue);
            cpu.SetZeroFlagByResult((byte)valueToCompare);
        }
    }
}
