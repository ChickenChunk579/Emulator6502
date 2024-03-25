using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Comparison
{
    // compare with x
    public class CPX : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("CPX", AddressingMode.Immediate, OpcodeEnum.CPX_IMM, 2),
            new Operation("CPX", AddressingMode.ZeroPage, OpcodeEnum.CPX_ZP, 3),

            new Operation("CPX", AddressingMode.Absolute, OpcodeEnum.CPX_ABS, 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            Common.Compare(cpu, operation.AddressingMode, cpu.XRegister);
        }
    }
}
