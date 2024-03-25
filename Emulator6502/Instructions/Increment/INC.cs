using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Increment
{
    // increment memory by one
    public class INC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("INC", AddressingMode.ZeroPage, OpcodeEnum.INC_ZP, 5),
            new Operation("INC", AddressingMode.ZeroPageX, OpcodeEnum.INC_ZPX, 6),

            new Operation("INC", AddressingMode.Absolute, OpcodeEnum.INC_ABS, 6),
            new Operation("INC", AddressingMode.AbsoluteX, OpcodeEnum.INC_ABSX, 7),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int addressToInc = cpu.GetAddressByAddressingMode(addressingMode);
            byte result = (byte)(cpu.ReadMemoryValue(addressToInc) + 1);

            cpu.WriteMemoryValue(addressToInc, result);

            cpu.SetNegativeFlagByResult(result);
            cpu.SetZeroFlagByResult(result);
        }
    }
}
