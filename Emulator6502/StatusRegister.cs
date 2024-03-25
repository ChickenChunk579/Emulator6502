namespace Emulator6502
{
    public class StatusRegister
    {
        
        private byte statusRegister = 0x00;

        public bool CarryFlag { 
            get => statusRegister.GetFlag(Flags.CarryBit);
            set => statusRegister.SetFlag(Flags.CarryBit, value);
        }

        public bool ZeroFlag { 
            get => statusRegister.GetFlag(Flags.Zero);
            set => statusRegister.SetFlag(Flags.Zero, value);
        }
        public bool DisableInterruptsFlag { 
            get => statusRegister.GetFlag(Flags.DisableInterrupts);
            set => statusRegister.SetFlag(Flags.DisableInterrupts, value);
        }
        public bool BreakFlag { 
            get => statusRegister.GetFlag(Flags.Break);
            set => statusRegister.SetFlag(Flags.Break, value);
        }
        public bool DecimalFlag { 
            get => statusRegister.GetFlag(Flags.Decimal);
            set => statusRegister.SetFlag(Flags.Decimal, value);
        }
        public bool UnusedFlag { 
            get => statusRegister.GetFlag(Flags.Unused);
            set => statusRegister.SetFlag(Flags.Unused, value);
        }
        public bool OverflowFlag { 
            get => statusRegister.GetFlag(Flags.Overflow);
            set => statusRegister.SetFlag(Flags.Overflow, value);
        }
        public bool NegativeFlag { 
            get => statusRegister.GetFlag(Flags.Negative);
            set => statusRegister.SetFlag(Flags.Negative, value);
        }

        public byte Value
        {
            get => this.statusRegister;
            set => this.statusRegister = value;
        }

        // if value passed is zero set zero flag to true
        public void SetZeroFlagByResult(byte result)
        {
            this.ZeroFlag = result == 0;
        }

        // sets the negative flag if the intager is greater than 127 - thats how 6502 handles negatives
        // also means if 8th bit is set (same thing)
        public void SetNegativeFlagByResult(byte result)
        {
            this.NegativeFlag = result > 127;
        }
    }
}
