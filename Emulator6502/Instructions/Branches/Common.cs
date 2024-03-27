namespace Emulator6502.Instructions.Branches
{
    public static class Common
    {
        public static void PerformBranch(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            int addressOffset = cpu.ReadMemoryValue(address);

            cpu.MoveProgramCounterByRelativeValue(addressOffset);
        }
    }
}
