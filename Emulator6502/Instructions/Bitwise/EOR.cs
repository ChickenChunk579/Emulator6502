namespace Emulator6502.Instructions.Bitwise
{
    public class EOR : IInstruction
    {
        
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("EOR", AddressModeExtensions.GetAbsoluteAddress, OpcodeEnum.EOR_ABS, 4),
            new Operation("EOR", AddressModeExtensions.GetAbsoluteXAddress, OpcodeEnum.EOR_ABSX, 4),
            new Operation("EOR", AddressModeExtensions.GetAbsoluteYAddress, OpcodeEnum.EOR_ABSY, 4),

            new Operation("EOR", AddressModeExtensions.GetImmediateAddress, OpcodeEnum.EOR_IMM, 2),

            new Operation("EOR", AddressModeExtensions.GetZeroPageAddress, OpcodeEnum.EOR_ZP, 3),
            new Operation("EOR", AddressModeExtensions.GetZeroPageXAddress, OpcodeEnum.EOR_ZPX, 4),

            new Operation("EOR", AddressModeExtensions.GetIndirectXAddress, OpcodeEnum.EOR_INDX, 6),
            new Operation("EOR", AddressModeExtensions.GetIndirectYAddress, OpcodeEnum.EOR_INDY, 5),
        };

        public void Execute(Operation operation, Processor cpu)
        {
            var address = operation.AddressingModeFunction.Invoke(cpu, operation);
            cpu.Accumulator ^= cpu.ReadMemoryValue(address);

            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);
        }
    }
}
