using Microsoft.Xna.Framework;

namespace Sunshine.Utility
{
    public static class VectorExt
    {
        public static void CopyTo(this Vector3 source, ref Vector3 target)
        {
            target.X = source.X;
            target.Y = source.Y;
            target.Z = source.Z;
        }
    }
}