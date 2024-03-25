using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.Instructions.Comparison
{
    public class Common
    {
        public static void Compare(Processor cpu, AddressingMode mode, int value)
        {
            var memoryValue = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(mode));
            var comparedValue = value - memoryValue;

            if (comparedValue < 0)
            {
                comparedValue += 0x10000;
            }

            cpu.SR.SetZeroFlagByResult((byte)comparedValue);

            cpu.SR.CarryFlag = memoryValue <= value;

            cpu.SR.SetNegativeFlagByResult((byte)comparedValue);
        }
    }
}
