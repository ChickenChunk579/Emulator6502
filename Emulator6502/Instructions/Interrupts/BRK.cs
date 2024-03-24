using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Interrupts
{
    public class BRK : IInstruction
    {
        public List<Opcode> Opcodes => new List<Opcode>
        {
            new Opcode("BRK", AddressingMode.Implied, 0x00)
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.ReadMemoryValue(++cpu.ProgramCounter);

            // put the high value on the stack
            // when we RTI, the address will be incremented by one, and the address after a break will not be used
            cpu.PokeStack((byte)((cpu.ProgramCounter >> 8) & 0xFF));
            cpu.StackPointer--;
            
            // put low value onto stack
            cpu.PokeStack((byte)(ConvertFlagsToByte))
        }
    }
}
