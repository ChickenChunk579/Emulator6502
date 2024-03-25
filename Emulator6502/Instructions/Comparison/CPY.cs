using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Comparison
{
    // compare with y
    public class CPY : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CPY", AddressingMode.Immediate, OpcodeEnum.CPY_IMM, 2),
            new Operation("CPY", AddressingMode.ZeroPage, OpcodeEnum.CPY_ZP, 3),

            new Operation("CPY", AddressingMode.Absolute, OpcodeEnum.CPY_ABS, 4),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            Common.Compare(cpu, addressingMode, cpu.YRegister);
        }
    }
}
