namespace Emulator6502.Instructions
{

    public class Opcode
    {
        public string Name { get; set; }
        public AddressingMode AddressingMode { get; set; }
        public byte OpcodeByte { get; set; }

        public short Cycles {get; set;}
        public short ExtraCyclesOnPageBoundry {get; set;}
        public int TotalCycles {get => Cycles + ExtraCyclesOnPageBoundry; }

        public Opcode(string name, AddressingMode addressingMode, byte opcodeByte, short cycles, short extraCycles = 0)
        {
            Name = name;
            AddressingMode = addressingMode;
            OpcodeByte = opcodeByte;
            
            Cycles = cycles;
            ExtraCyclesOnPageBoundry = extraCycles;
        }
    }
}
