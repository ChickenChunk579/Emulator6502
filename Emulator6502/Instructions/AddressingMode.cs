namespace Emulator6502.Instructions
{
    public enum AddressingMode
    {
        // full address is given in next to bytes, lo then hi
        Absolute = 1,
        // same as absolute but add X register to address
        AbsoluteX = 2,
        // same as absolute but add Y register to address
        AbsoluteY = 3,

        // instruction operates on accumulator - no operands needed;
        Accumulator = 4,

        // value to operate on stored immediately after the instruction in literal
        Immediate = 5,

        // no data required
        Implied = 6,

        IndirectX = 7,

        IndirectY = 8,
        
        // operand is address
        Indirect = 9,
        
        // allows program to change pc by 127 in either direction
        Relative = 10,

        // reads zero page address to operate on
        ZeroPage = 11,

        // same as zero page but x register is added to address
        ZeroPageX = 12,

        // same as zero page but y register is added to address
        ZeroPageY = 13,
    }
}
