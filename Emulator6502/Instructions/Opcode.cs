namespace Emulator6502.Instructions
{

    public class Operation
    {
        public string Name { get; set; }
        public Func<Processor, Operation, int> AddressingModeFunction { get; set; }

        public OpcodeEnum OpcodeEnum { get; set; }
        public byte OpcodeByte => (byte)OpcodeEnum;

        public short Cycles {get; set;}
        public short ExtraCyclesOnPageBoundry {get; set;}
        public int TotalCycles {get => Cycles + ExtraCyclesOnPageBoundry; }

        public Operation(string name, Func<Processor, Operation, int> addressingMode, OpcodeEnum opcodeEnum, short cycles, short extraCycles = 0)
        {
            Name = name;
            AddressingModeFunction = addressingMode;
            OpcodeEnum = opcodeEnum;
            
            Cycles = cycles;
            ExtraCyclesOnPageBoundry = extraCycles;
        }
    }
}
