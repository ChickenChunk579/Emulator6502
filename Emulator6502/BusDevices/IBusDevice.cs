namespace Emulator6502.BusDevices
{
    public interface IBusDevice
    {
        public bool ShouldUse(int address);

        public void Write(int address, byte data);
        public byte Read(int address);
    }
}
