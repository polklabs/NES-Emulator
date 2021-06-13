namespace ExtensionMethods
{
    public static class Extensions
    {
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        public static string ToHex(this ushort s)
        {
            return s.ToString("X4");
        }
    }
}