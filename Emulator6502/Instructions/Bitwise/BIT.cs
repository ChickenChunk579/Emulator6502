namespace Emulator6502.Instructions.Bitwise
{
    public class BIT : IInstruction
    {
        
        public List<Operation> Opcodes => new List<Operation>()
        {
          
            new Operation("BIT", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.BIT_ZP, 3),

            new Operation("BIT", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.BIT_ABS , 4),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            byte memoryValue = cpu.ReadMemoryValue(address);

            int valueToCompare = memoryValue & cpu.Accumulator;

            cpu.SR.OverflowFlag = (memoryValue & 0x40) != 0;

            cpu.SR.SetNegativeFlagByResult(memoryValue);
            cpu.SR.SetZeroFlagByResult((byte)valueToCompare);
        }
    }
}
