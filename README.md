# Emulator6502
A C# library for emulating the 6502
NOTE - still in very early stages of development

# Progress
## Implemented instructions:
* LDA (Load accumulator)
* SEC (Set carry bit)
* BIT (Bit test)
* BCC (Branch carry clear)
* BCS (Branch carry set)
* BEQ (Branch equal)
* BMI (Branch negative)
* BNE (Branch not equal)
* BPL (Branch negative clear)

## Features
* Unit tests (based on [sim6502](https://github.com/barryw/sim6502/blob/master/sim6502tests/ProcessorTests.cs/) tests)
* Executing instructions
* All addressing modes
* Extendable bus by implementing the IBusDevice interface

# Adding new instructions
1. Create a class in the instructions folder that implements the IInstruction interface
2. Specify the different opcodes and there addressing mode with the list of opcodes
3. Implement instruction logic in the Execute function