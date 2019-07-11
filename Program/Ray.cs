using Microsoft.Xna.Framework;

namespace Program
{
    public struct Ray
    {
        public Ray(in Vector3 origin, in Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Origin { get; }

        public Vector3 Direction { get; }

        public Vector3 PointAtParameter(float point) => Origin + point * Direction;
    }
}