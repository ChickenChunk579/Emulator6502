using Emulator6502.Instructions.Bitwise;

namespace Emulator6502.Instructions.Decrement
{
    // decrement memory by one
    public class DEC : IInstruction
    {
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("DEC", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.DEC_ZP, 5),
            new Operation("DEC", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.DEC_ZPX, 6),

            new Operation("DEC", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.DEC_ABS, 6),
            new Operation("DEC", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.DEC_ABSX, 7),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);

            byte result = (byte)(cpu.ReadMemoryValue(address) - 1);

            cpu.WriteMemoryValue(address, result);

            cpu.SR.SetNegativeFlagByResult(result);
            cpu.SR.SetZeroFlagByResult(result);
        }
    }
}
