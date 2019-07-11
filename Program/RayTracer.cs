using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Program
{
    public sealed class RayTracer : IRenderer
    {
        private int width;
        private int height;

        private readonly Color[] frameBuffer;

        // TODO: turn these into calculated values based on aspect ratio
        // TODO: implement camera class
        private readonly Vector3 lowerLeftCorner = new Vector3(-2f, -1f, -1f);

        private readonly Vector3 Horizontal = new Vector3(4f, 0f, 0f);

        private readonly Vector3 Vertical = new Vector3(0f, 2f, 0f);

        private readonly Vector3 Origin = new Vector3(0f, 0f, 0f);

        public RayTracer(int x, int y)
        {
            width = x;
            height = y;
            frameBuffer = new Color[width * height];

            for(int i = 0; i < frameBuffer.Length; i++)
            {
                frameBuffer[i] = new Color();
            }
        }

        public Color[] NextFrame()
        {
            for(int y = height - 1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    // chapter 1 outputs the image to the image file, we're flipping the
                    // row pointer so that we start from the other end in order to get same visual result.
                    var index = (height - 1 - y) * width + x;

                    float u = (float)x / width;
                    float v = (float)y / height;

                    var ray = new Ray(Origin, lowerLeftCorner + u * Horizontal + v * Vertical);
                    PixelColor(ray, ref frameBuffer[index]);
                }
            }

            return frameBuffer;
        }

        // Temp, only using these for sake of clarity, will remove once the collision method becomes modular
        private static Vector3 SpherePosition = new Vector3(0f, 0f, -1f);

        private static float SphereRadius = 0.5f;

        private static void PixelColor(in Ray ray, ref Color color)
        {
            if(IsSphereHit(SpherePosition, SphereRadius, ray))
            {
                color.R = 255;
                color.G = 0;
                color.B = 0;
                color.A = 255;
                return;
            }

            float t = 0.5f * (Vector3.Normalize(ray.Direction).Y + 1);

            //  lerp => blended_value = (1-t)*start_value + t*end_value​
            var result = (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
            color.R = (byte)(result.X * 255.99f);
            color.G = (byte)(result.Y * 255.99f);
            color.B = (byte)(result.Z * 255.99f);
            color.A = 255;
        }

        private static bool IsSphereHit(in Vector3 center, float radius, in Ray ray)
        {
            var rayToCenter = ray.Origin - center;
            float a = Vector3.Dot(ray.Direction, ray.Direction);
            float b = 2.0f * Vector3.Dot(rayToCenter, ray.Direction);
            float c = Vector3.Dot(rayToCenter, rayToCenter) - radius * radius;
            float discriminant = b * b - 4 * a * c;

            return discriminant > 0;
        }
    }
}