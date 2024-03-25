using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.StatusSetters
{
    // set carry
    public class SEC : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("SEC", AddressingMode.Implied, 0x38, 2)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.CarryFlag = true;
        }
    }
}
