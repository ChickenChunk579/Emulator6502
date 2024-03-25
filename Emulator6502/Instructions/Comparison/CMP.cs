using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Comparison
{
    // compare with accumulator
    public class CMP : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CMP", AddressingMode.Immediate, OpcodeEnum.CMP_IMM, 2),
            new Operation("CMP", AddressingMode.ZeroPage, OpcodeEnum.CMP_ZP, 3),
            new Operation("CMP", AddressingMode.ZeroPageX, OpcodeEnum.CMP_ZPX, 4),

            new Operation("CMP", AddressingMode.Absolute, OpcodeEnum.CMP_ABS, 4),
            new Operation("CMP", AddressingMode.AbsoluteX, OpcodeEnum.CMP_ABSX, 4),
            new Operation("CMP", AddressingMode.AbsoluteY, OpcodeEnum.CMP_ABSY, 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            Common.Compare(cpu, operation.AddressingMode, cpu.Accumulator);
        }
    }
}
