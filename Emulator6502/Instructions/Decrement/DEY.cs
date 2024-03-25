using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Decrement
{
    // decrement y by one
    public class DEY : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("DEY", AddressingMode.Implied, OpcodeEnum.DEY, 2),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int valueToDec = cpu.YRegister;
            byte result = (byte)(valueToDec - 1);

            cpu.YRegister = result;

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
