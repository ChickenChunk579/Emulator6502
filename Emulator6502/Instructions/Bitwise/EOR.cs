using System.Reflection.Emit;

namespace Emulator6502.Instructions.Bitwise
{
    public class EOR : IInstruction
    {
        
        public List<Operation> Opcodes => new List<Operation>()
        {
            new Operation("EOR", AddressingMode.Absolute, OpcodeEnum.EOR_ABS, 4),
            new Operation("EOR", AddressingMode.AbsoluteX, OpcodeEnum.EOR_ABSX, 4),
            new Operation("EOR", AddressingMode.AbsoluteY, OpcodeEnum.EOR_ABSY, 4),

            new Operation("EOR", AddressingMode.Immediate, OpcodeEnum.EOR_IMM, 2),

            new Operation("EOR", AddressingMode.ZeroPage, OpcodeEnum.EOR_ZP, 3),
            new Operation("EOR", AddressingMode.ZeroPageX, OpcodeEnum.EOR_ZPX, 4),

            new Operation("EOR", AddressingMode.IndirectX, OpcodeEnum.EOR_INDX, 6),
            new Operation("EOR", AddressingMode.IndirectY, OpcodeEnum.EOR_INDY, 5),
        };

        public void Execute(byte opcode, AddressingMode addressingMode, Processor cpu)
        {
            cpu.Accumulator ^= cpu.ReadMemoryValue(cpu.GetAddressByAddressingMode(addressingMode));

            cpu.SR.SetNegativeFlagByResult(cpu.Accumulator);
            cpu.SR.SetZeroFlagByResult(cpu.Accumulator);
        }
    }
}
