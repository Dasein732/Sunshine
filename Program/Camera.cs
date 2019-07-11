using Microsoft.Xna.Framework;

namespace Program
{
    public class Camera
    {
        public Vector3 Origin { get; } = new Vector3(0f, 0f, 0f);
        public Vector3 LowerLeftCorner { get; }
        public Vector3 Horizontal { get; }
        public Vector3 Vertical { get; }

        public Camera(Vector3 lowerLeftCorner, Vector3 horizontal, Vector3 vertical)
        {
            LowerLeftCorner = lowerLeftCorner;
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
        }
    }
}