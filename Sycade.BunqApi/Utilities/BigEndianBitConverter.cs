namespace Sycade.BunqApi.Utilities
{
    public static class BigEndianBitConverter
    {
        public static byte[] GetBytes(short value)
        {
            return new byte[]
            {
                (byte)(value >> 8),
                (byte)value
            };
        }

        public static short ToInt16(byte[] value, long startIndex = 0)
        {
            return (short)((value[startIndex] << 0x08) | value[startIndex + 1]);
        }
    }
}
