using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Loads
{
    public class LDA : IInstruction
    {
        // lda opcodes with different addressing modes
        public List<Opcode> Opcodes
        {
            get => new List<Opcode>()
            {
                new Opcode("LDA", AddressingMode.IndirectX, 0xA1),
                new Opcode("LDA", AddressingMode.IndirectY, 0xB1),

                new Opcode("LDA", AddressingMode.ZeroPage, 0xA5),
                new Opcode("LDA", AddressingMode.ZeroPageX, 0xB5),

                new Opcode("LDA", AddressingMode.AbsoluteX, 0xBD),
                new Opcode("LDA", AddressingMode.AbsoluteY, 0xB9),

                new Opcode("LDA", AddressingMode.Absolute, 0xAD),
                new Opcode("LDA", AddressingMode.Immediate, 0xA9),

            };
        }

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {

            // Read in byte from memory and set accumulator value to it
            cpu.Accumulator = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));

            // Set zero flag based on value of accumulator
            cpu.SetZeroFlagByResult(cpu.Accumulator);

            // Set zero flag based on value of accumulator
            cpu.SetNegativeFlagByResult(cpu.Accumulator);
        }
    }
}
