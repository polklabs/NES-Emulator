using System;

namespace NES_Emulator
{
    static class Utils
    {
        public static void hexDump(byte[] data)
        {
            Console.WriteLine(BitConverter.ToString(data).Replace("-", " "));
        }
    }
}
