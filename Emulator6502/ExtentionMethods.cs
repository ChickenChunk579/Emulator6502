using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator6502
{
    public static class ExtentionMethods
    {
        public static bool GetFlag(this byte b, Flags flag)
        {
            return (b & (byte)flag) > 0;
        }

        public static byte SetFlag(this byte b, Flags flag, bool value)
        {
            if (value)
            {
                b |= (byte)flag;
            } else
            {
                b &= (byte)~(byte)flag;
            }

            return b;
        }
    }
}
