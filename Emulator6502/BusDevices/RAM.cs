using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502.BusDevices
{
    // simple ram that takes up all addressable space
    public class RAM : IBusDevice
    {
        public byte[] ram = new byte[0x10000];

        public byte Read(int address)
        {
            Console.WriteLine(address.ToString("X4"));
            return ram[address];
        }

        public bool ShouldUse(int address)
        {
            // no matter what return true - maps to entire range
            return true;
        }

        public void Write(int address, byte data)
        {
            ram[address] = data;
        }
    }
}