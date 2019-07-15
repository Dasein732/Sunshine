using Microsoft.Xna.Framework;

namespace Sunshine.Utility
{
    public static class MathUtil
    {
        public static float FastSqrt(float number)
        {
            // https://en.wikipedia.org/wiki/Fast_inverse_square_root
            unsafe
            {
                number = 1.0f / number;
                // original uses long instead of uint, since C# long is 8 bytes, I've replaced it with a 4 byte structure
                uint i;
                float x, y;

                x = number * 0.5f;
                y = number;
                i = *(uint*)&y;
                i = 0x5f3759df - (i >> 1);
                y = *(float*)&i;
                y *= (1.5f - (x * y * y));

                return y;
            }
        }

        public static Vector3 RandomInUnitSphere()
        {
            Vector3 p;

            do
            {
                p = 2.0f * new Vector3(XorShiftRandom.NextFloat(), XorShiftRandom.NextFloat(), 0) - new Vector3(1f, 1f, 0f);
            } while(Vector3.Dot(p, p) >= 1);

            return p;
        }
    }
}