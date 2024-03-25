using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions
{

    public class Opcode
    {
        public string Name { get; set; }
        public AddressingMode AddressingMode { get; set; }
        public byte OpcodeByte { get; set; }

        public Opcode(string name, AddressingMode addressingMode, byte opcodeByte)
        {
            Name = name;
            AddressingMode = addressingMode;
            OpcodeByte = opcodeByte;
        }
    }
}
