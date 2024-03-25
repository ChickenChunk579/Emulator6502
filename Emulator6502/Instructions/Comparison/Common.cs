namespace Emulator6502.Instructions.Comparison
{
    public class Common
    {
        public static void Compare(Operation operation, Processor cpu, int value)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            var memoryValue = cpu.ReadMemoryValue(address);
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
