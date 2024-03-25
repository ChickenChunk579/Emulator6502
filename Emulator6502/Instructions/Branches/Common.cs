namespace Emulator6502.Instructions.Branches
{
    public static class Common
    {
        public static void PerformBranch(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            int addressOffset = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));

            cpu.MoveProgramCounterByRelativeValue(addressOffset);
        }
    }
}
