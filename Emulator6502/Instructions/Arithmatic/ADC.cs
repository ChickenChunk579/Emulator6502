using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Arithmatic
{
    public class ADC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("ADC", AddressingMode.Absolute, OpcodeEnum.ADC_ABS, 4),
            new Operation("ADC", AddressingMode.Absolute, OpcodeEnum.ADC_ABSX, 4),
            new Operation("ADC", AddressingMode.Absolute, OpcodeEnum.ADC_ABSY, 4),

            new Operation("ADC", AddressingMode.Immediate, OpcodeEnum.ADC_IMM, 2),

            new Operation("ADC", AddressingMode.ZeroPageX, OpcodeEnum.ADC_ZP, 3),
            new Operation("ADC", AddressingMode.ZeroPageX, OpcodeEnum.ADC_ZPX, 4),

            new Operation("ADC", AddressingMode.IndirectX, OpcodeEnum.ADC_INDX, 6),
            new Operation("ADC", AddressingMode.IndirectX, OpcodeEnum.ADC_INDX, 5),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            // read operand
            var memoryValue = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));

            // perform calculation
            var newValue = memoryValue + cpu.Accumulator + (cpu.CarryFlag ? 1 : 0);

            // set overflow flag
            cpu.OverflowFlag = ((cpu.Accumulator ^ newValue) & 0x80) != 0 && ((cpu.Accumulator ^ memoryValue) & 0x80) == 0;
        
            if (cpu.DecimalFlag)
            {
                // TODO: Add decimal mode
            } else
            {
                // set carry flag
                if (newValue > 255)
                {
                    cpu.CarryFlag = true;
                    newValue -= 256;
                } else
                {
                    cpu.CarryFlag = false;
                }
            }

            // set zero and negative flags
            cpu.SetNegativeFlagByResult((byte)newValue);
            cpu.SetZeroFlagByResult((byte)newValue);

            // set accumulator to result
            cpu.Accumulator = (byte)newValue;
        }
    }
}
