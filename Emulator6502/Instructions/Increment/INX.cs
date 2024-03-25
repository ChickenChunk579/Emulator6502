using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Decrement
{
    // decrement memory by one
    public class INX : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("INX", AddressingMode.Implied, OpcodeEnum.INX, 2),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int valueToInc = cpu.XRegister;
            byte result = (byte)(valueToInc + 1);

            cpu.XRegister = result;

            cpu.SetNegativeFlagByResult(result);
            cpu.SetZeroFlagByResult(result);
        }
    }
}
