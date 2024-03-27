using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Increment
{
    // increment memory by one
    public class INC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("INC", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.INC_ZP, 5),
            new Operation("INC", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.INC_ZPX, 6),

            new Operation("INC", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.INC_ABS, 6),
            new Operation("INC", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.INC_ABSX, 7),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            var addressToInc = operation.AddressingModeFunction.Invoke(cpu, operation);

            byte result = (byte)(cpu.ReadMemoryValue(addressToInc) + 1);

            cpu.WriteMemoryValue(addressToInc, result);

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
