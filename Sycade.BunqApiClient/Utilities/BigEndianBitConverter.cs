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

        public static int ToInt32(byte[] value, long startIndex = 0)
        {
            return (value[startIndex] << 0x18) | (value[startIndex + 1] << 0x10) | (value[startIndex + 2] << 0x08) | value[startIndex + 3];
        }

        public static long ToInt64(byte[] value, long startIndex = 0)
        {
            return ((long)value[startIndex] << 0x38) | ((long)value[startIndex + 1] << 0x30) | ((long)value[startIndex + 2] << 0x28) | ((long)value[startIndex + 3] << 0x20)
                | ((long)value[startIndex + 4] << 0x18) | ((long)value[startIndex + 5] << 0x10) | ((long)value[startIndex + 6] << 0x08) | value[startIndex + 7];
        }

        public static ushort ToUInt16(byte[] value, long startIndex = 0)
        {
            return (ushort)ToInt16(value, startIndex);
        }

        public static uint ToUInt32(byte[] value, long startIndex = 0)
        {
            return (uint)ToInt32(value, startIndex);
        }

        public static ulong ToUInt64(byte[] value, long startIndex = 0)
        {
            return (ulong)ToInt64(value, startIndex);
        }
    }
}
