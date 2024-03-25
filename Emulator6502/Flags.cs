namespace Emulator6502
{
    /* Flag structure
    
    Bit Number  Meaning                 Value (Dec)     Value (Hex)
    0           Carry Flag              1               0x01
    1           Zero Flag               2               0x02
    2           IRQ Disable Flag        4               0x04
    3           Decimal Mode Flag       8               0x08
    4           Break Command Flag      16              0x10
    5           Unused Flag             32              0x20
    6           Overflow Flag           64              0x40
    7           Negative Flag           128             0x80
    */

    public enum Flags : byte
    {
        CarryBit = (1 << 0), // carry bit
        Zero = (1 << 1), // zero
        DisableInterrupts = (1 << 2), // disable interrupts
        Decimal = (1 << 3), // decimal mode (unused in this impl)
        Break = (1 << 4), // break
        Unused = (1 << 5), // unused
        Overflow = (1 << 6), // overflow
        Negative = (1 << 7), // negative
    }
}
