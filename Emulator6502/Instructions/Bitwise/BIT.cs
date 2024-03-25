using System.Reflection.Emit;

namespace Emulator6502.Instructions.Bitwise
{
    public class BIT : IInstruction
    {
        
        public List<Operation> Opcodes => new List<Operation>()
        {
          
            new Operation("BIT", AddressingMode.ZeroPage, OpcodeEnum.BIT_ZP, 3),

            new Operation("BIT", AddressingMode.Absolute, OpcodeEnum.BIT_ABS , 4),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            byte memoryValue = cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));
            int valueToCompare = memoryValue & cpu.Accumulator;

            cpu.OverflowFlag = (memoryValue & 0x40) != 0;

            cpu.SetNegativeFlagByResult(memoryValue);
            cpu.SetZeroFlagByResult((byte)valueToCompare);
        }
    }
}
