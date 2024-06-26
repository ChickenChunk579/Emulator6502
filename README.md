# Emulator6502
A C# library for emulating the 6502
NOTE - still in very early stages of development

# Progress
## Implemented instructions:
* LDA (Load accumulator)
* LDX (Load x)
* LDX (Load y)
* SEC (Set carry bit)
* BIT (Bit test)
* BCC (Branch carry clear)
* BCS (Branch carry set)
* BEQ (Branch equal)
* BMI (Branch negative)
* BNE (Branch not equal)
* BPL (Branch negative clear)
* ADC (Add with carry)
* SED (Set Decimal Flag)
* CLI (Clear interrupts flag)
* SEI (Set interrupts flag)
* BRK (Break or run software interrupt)
* BVC (Branch overflow clear)
* BVS (Branch overflow set)
* CLC (Clear carry bit)
* CLD (Clear decimal bit)
* CLV (Clear overflow flag)
* CMP (Compare accumulator)
* CMX (Comapre X Register)
* CMY (Compare Y Register)
* EOR (Exclusive OR)
* DEC (Decrement memory value)
* DEX (Decrement X)
* DEY (Decrement Y)
* INC (Increment memory value)
* INX (Increment x register)
* INY (Increment y register)
* JMP (Jump)
* JSR (Jump subroutine)

## Features
* Unit tests (based on [sim6502](https://github.com/barryw/sim6502/blob/master/sim6502tests/ProcessorTests.cs/) tests)
* Executing instructions
* All addressing modes
* Extendable bus by implementing the IBusDevice interface
* Stack
* Software interrupts

# Adding new instructions
1. Create a class in the instructions folder that implements the IInstruction interface
2. Specify the different opcodes and there addressing mode with the list of opcodes
3. Implement instruction logic in the Execute function
4. Add unit tests