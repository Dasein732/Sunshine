using System;
using Microsoft.Xna.Framework;

namespace Program
{
    public class Camera
    {
        public Vector3 Origin { get; }
        public Vector3 LowerLeftCorner { get; }
        public Vector3 Horizontal { get; }
        public Vector3 Vertical { get; }

        public Camera(Vector3 origin, Vector3 direction, Vector3 up, float verticalFOVDeg, float aspect)
        {
            Vector3 u, v, w;

            var theta = verticalFOVDeg * MathF.PI / 180;
            var halfHeight = MathF.Tan(theta / 2);
            var halfWidth = aspect * halfHeight;

            w = Vector3.Normalize(origin - direction);
            u = Vector3.Normalize(Vector3.Cross(up, w));
            v = Vector3.Cross(w, u);

            Origin = origin;
            LowerLeftCorner = Origin - halfWidth * u - halfHeight * v - w;
            Horizontal = 2 * halfWidth * u;
            Vertical = 2 * halfHeight * v;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
        }
    }
}