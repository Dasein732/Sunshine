using System;
using Core;
using Microsoft.Xna.Framework;
using Sunshine.Utility;
using Ray = Core.Ray;

namespace Program
{
    public class Camera
    {
        public Vector3 Origin { get; }
        public Vector3 LowerLeftCorner { get; }
        public Vector3 Horizontal { get; }
        public Vector3 Vertical { get; }
        private Vector3 U { get; }
        private Vector3 W { get; }
        private Vector3 V { get; }
        private float LensRadius { get; }

        public Camera(Vector3 origin, Vector3 direction, Vector3 up, float verticalFOVDeg, float aspect, float aperture, float focusDistance)
        {
            LensRadius = aperture / 2;
            var theta = verticalFOVDeg * MathF.PI / 180;
            var halfHeight = MathF.Tan(theta / 2);
            var halfWidth = aspect * halfHeight;

            W = Vector3.Normalize(origin - direction);
            U = Vector3.Normalize(Vector3.Cross(up, W));
            V = Vector3.Cross(W, U);

            Origin = origin;
            LowerLeftCorner = Origin - halfWidth * focusDistance * U - halfHeight * focusDistance * V - focusDistance * W;
            Horizontal = 2 * halfWidth * focusDistance * U;
            Vertical = 2 * halfHeight * focusDistance * V;
        }

        public Ray GetRay(float s, float t)
        {
            var rd = LensRadius * MathUtil.RandomInUnitSphere();
            var offset = U * rd.X + V * rd.Y;
            return new Ray(Origin + offset, LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
        }
    }
}