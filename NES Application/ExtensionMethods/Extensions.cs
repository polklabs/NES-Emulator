namespace ExtensionMethods
{
    public static class Extensions
    {
        public static string ToHex(this byte b)
        {
            return "0x" + b.ToString("X2");
        }

        public static string ToHex(this ushort s)
        {
            return "0x" + s.ToString("X4");
        }
    }
}