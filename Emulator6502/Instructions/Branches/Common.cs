namespace Emulator6502.Instructions.Branches
{
    public static class Common
    {
        public static void PerformBranch(Operation operation, Processor cpu)
        {
            int addressOffset = cpu.ReadMemoryValue(cpu.GetAddressForOperation(operation));

            Console.WriteLine(addressOffset);

            cpu.MoveProgramCounterByRelativeValue(addressOffset);
        }
    }
}
