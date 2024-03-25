using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Decrement
{
    // decrement y by one
    public class INY : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("INY", AddressingMode.Implied, OpcodeEnum.INY, 2),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int valueToInc = cpu.YRegister;
            byte result = (byte)(valueToInc + 1);

            cpu.YRegister = result;

            cpu.SetNegativeFlagByResult(result);
            cpu.SetZeroFlagByResult(result);
        }
    }
}
