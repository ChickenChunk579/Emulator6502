using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.BusDevices
{
    public interface IBusDevice
    {
        public bool ShouldUse(int address);

        public void Write(int address, byte data);
        public byte Read(int address);
    }
}
