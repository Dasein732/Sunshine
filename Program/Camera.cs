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
        private Vector3 u { get; }
        private Vector3 w { get; }
        private Vector3 v { get; }
        private float lensRadius { get; }

        public Camera(Vector3 origin, Vector3 direction, Vector3 up, float verticalFOVDeg, float aspect, float aperture, float focusDistance)
        {
            lensRadius = aperture / 2;
            var theta = verticalFOVDeg * MathF.PI / 180;
            var halfHeight = MathF.Tan(theta / 2);
            var halfWidth = aspect * halfHeight;

            w = Vector3.Normalize(origin - direction);
            u = Vector3.Normalize(Vector3.Cross(up, w));
            v = Vector3.Cross(w, u);

            Origin = origin;
            LowerLeftCorner = Origin - halfWidth * focusDistance * u - halfHeight * focusDistance * v - focusDistance * w;
            Horizontal = 2 * halfWidth * focusDistance * u;
            Vertical = 2 * halfHeight * focusDistance * v;
        }

        public Ray GetRay(float s, float t)
        {
            var rd = lensRadius * RayTracer.RandomInUnitSphere();
            var offset = u * rd.X + v * rd.Y;
            return new Ray(Origin + offset, LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
        }
    }
}