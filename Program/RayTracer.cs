using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Program.Surfaces;

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

        // TODO: create some sort of entity manager or scene graph later on
        private readonly HitableList World;

        public RayTracer(int x, int y)
        {
            width = x;
            height = y;
            frameBuffer = new Color[width * height];

            for(int i = 0; i < frameBuffer.Length; i++)
            {
                frameBuffer[i] = new Color();
            }

            List<Hitable> objectList = new List<Hitable>()
            {
            new Sphere(new Vector3(0f, 0f, -1f), 0.5f),
            new Sphere(new Vector3(0f, -100.5f, -1f), 100f)
            };

            World = new HitableList(objectList);
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
                    PixelColor(ray, World, ref frameBuffer[index]);
                }
            }

            return frameBuffer;
        }

        private static void PixelColor(in Ray ray, HitableList hitableList, ref Color color)
        {
            var hitRecord = new HitRecord();

            if(hitableList.Hit(ray, 0f, float.MaxValue, ref hitRecord))
            {
                var result = 0.5f * new Vector3(hitRecord.Normal.X + 1, hitRecord.Normal.Y + 1, hitRecord.Normal.Z + 1);
                result *= 255.99f;

                color.R = (byte)result.X;
                color.G = (byte)result.Y;
                color.B = (byte)result.Z;

                color.A = 255;
            }
            else
            {
                float t = 0.5f * (Vector3.Normalize(ray.Direction).Y + 1);

                //  lerp => blended_value = (1-t)*start_value + t*end_value​
                var result = (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
                result *= 255.99f;

                color.R = (byte)result.X;
                color.G = (byte)result.Y;
                color.B = (byte)result.Z;
                color.A = 255;
            }
        }
    }
}