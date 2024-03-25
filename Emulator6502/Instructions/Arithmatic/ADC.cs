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

        public void Execute(Operation operation, Processor cpu)
        {
            // read operand
            var memoryValue = cpu.ReadMemoryValue(cpu.GetAddressForOperation(operation));

            // perform calculation
            var newValue = memoryValue + cpu.Accumulator + (cpu.SR.CarryFlag ? 1 : 0);

            // set overflow flag
            cpu.SR.OverflowFlag = ((cpu.Accumulator ^ newValue) & 0x80) != 0 && ((cpu.Accumulator ^ memoryValue) & 0x80) == 0;
        
            if (cpu.SR.DecimalFlag)
            {
                // TODO: Add decimal mode
            } else
            {
                // set carry flag
                if (newValue > 255)
                {
                    cpu.SR.CarryFlag = true;
                    newValue -= 256;
                } else
                {
                    cpu.SR.CarryFlag = false;
                }
            }

            // set zero and negative flags
            cpu.SR.SetNegativeFlagByResult((byte)newValue);
            cpu.SR.SetZeroFlagByResult((byte)newValue);

            // set accumulator to result
            cpu.Accumulator = (byte)newValue;
        }
    }
}
