namespace Emulator6502
{
    public static class ExtentionMethods
    {
        public static bool GetFlag(this byte b, Flags flag)
        {
            return (b & (byte)flag) > 0;
        }

        public static void SetFlag(this ref byte b, Flags flag, bool value)
        {
            if (value)
            {
                b |= (byte)flag;
            } else
            {
                b &= (byte)~(byte)flag;
            }
        }
    }
}
