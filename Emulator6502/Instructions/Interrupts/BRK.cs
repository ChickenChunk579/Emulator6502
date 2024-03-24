﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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
            var resetVector = cpu.ReadIRQVector();

            var bytes = BitConverter.GetBytes(cpu.ProgramCounter+1);
            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, bytes[1]);
            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, bytes[0]);

            cpu.WriteMemoryValue(0x100 + cpu.StackPointer--, cpu.GetFlagByte().SetFlag(Flags.Break,true));

            cpu.DisableInterruptsFlag = true;
             
            cpu.ProgramCounter = resetVector;            
        }
    }
}
