using Emulator6502.BusDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502
{
    public class Bus
    {
        public List<IBusDevice> Devices { get; set; }

        public Bus()
        {
            Devices = new List<IBusDevice>();
        }

        public IBusDevice GetBusDeviceByAddress(int addr)
        {
            foreach (IBusDevice device in Devices)
            {
                if (device.ShouldUse(addr))
                {
                    return device;
                }
            }

            throw new Exception($"Invalid address {addr:X4}: Not contained within any bus devices");
        }

        public void Write(int addr, byte data)
        {
            GetBusDeviceByAddress(addr).Write(addr, data);
        }

        public byte Read(int addr)
        {
            return GetBusDeviceByAddress(addr).Read(addr);
        }
    }
}
