using System;

namespace Program.Util
{
    /// <summary>
    /// Taken from http://codingha.us/2018/12/17/xorshift-fast-csharp-random-number-generator/
    /// </summary>
    public static class XorShiftRandom
    {
        private static ulong x_;
        private static ulong y_;

        static XorShiftRandom()
        {
            x_ = (ulong)Guid.NewGuid().GetHashCode();
            y_ = (ulong)Guid.NewGuid().GetHashCode();
        }

        private const double DOUBLE_UNIT = 1.0 / (int.MaxValue + 1.0);

        public static float NextFloat()
        {
            return (float)NextDouble();
        }

        public static double NextDouble()
        {
            double _;
            ulong temp_x, temp_y, temp_z;

            temp_x = y_;
            x_ ^= x_ << 23;
            temp_y = x_ ^ y_ ^ (x_ >> 17) ^ (y_ >> 26);

            temp_z = temp_y + y_;
            _ = DOUBLE_UNIT * (0x7FFFFFFF & temp_z);

            x_ = temp_x;
            y_ = temp_y;

            return _;
        }

        public static int NextInt32()
        {
            int _;
            ulong temp_x, temp_y;

            temp_x = y_;
            x_ ^= x_ << 23;
            temp_y = x_ ^ y_ ^ (x_ >> 17) ^ (y_ >> 26);

            _ = (int)(temp_y + y_);

            x_ = temp_x;
            y_ = temp_y;

            return _;
        }
    }
}