using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Program.Surfaces;
using Program.Util;

namespace Program
{
    public sealed class RayTracer : IRenderer
    {
        private readonly Color[] frameBuffer;
        private readonly Camera camera;

        // TODO: create some sort of entity manager or scene graph later on
        private readonly HitableList World;

        private readonly XorShiftRandom rand;

        private readonly RendererConfiguration renderConfig;

        public RayTracer(RendererConfiguration renderConfig)
        {
            this.renderConfig = renderConfig;
            frameBuffer = new Color[renderConfig.Width * renderConfig.Height];

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

            camera = new Camera(new Vector3(-2f, -1f, -1f), new Vector3(4f, 0f, 0f), new Vector3(0f, 2f, 0f));
            rand = new XorShiftRandom();
        }

        public Color[] NextFrame()
        {
            for(int y = renderConfig.Height - 1; y >= 0; y--)
            {
                for(int x = 0; x < renderConfig.Width; x++)
                {
                    // chapter 1 outputs the image to the image file, we're flipping the
                    // row pointer so that we start from the other end in order to get same visual result.
                    var index = (renderConfig.Height - 1 - y) * renderConfig.Width + x;

                    if(renderConfig.Antialiasing)
                    {
                        Vector3 color = new Vector3();

                        for(int i = 0; i < renderConfig.AASamples; i++)
                        {
                            float u = (float)(x + rand.NextDouble()) / renderConfig.Width;
                            float v = (float)(y + rand.NextDouble()) / renderConfig.Height;

                            color += PixelColor(camera.GetRay(u, v), World);
                        }

                        color /= renderConfig.AASamples;
                        color = new Vector3(MathUtil.FastSqrt(color.X), MathUtil.FastSqrt(color.Y), MathUtil.FastSqrt(color.Z));

                        color *= 255.99f;
                        frameBuffer[index].R = (byte)color.X;
                        frameBuffer[index].G = (byte)color.Y;
                        frameBuffer[index].B = (byte)color.Z;
                        frameBuffer[index].A = 255;
                    }
                    else
                    {
                        float u = (float)x / renderConfig.Width;
                        float v = (float)y / renderConfig.Height;

                        var color = PixelColor(camera.GetRay(u, v), World);

                        color = new Vector3(MathUtil.FastSqrt(color.X), MathUtil.FastSqrt(color.Y), MathUtil.FastSqrt(color.Z));

                        color *= 255.99f;
                        frameBuffer[index].R = (byte)color.X;
                        frameBuffer[index].G = (byte)color.Y;
                        frameBuffer[index].B = (byte)color.Z;
                        frameBuffer[index].A = 255;
                    }
                }
            }

            return frameBuffer;
        }

        private Vector3 PixelColor(in Ray ray, HitableList hitableList)
        {
            var hitRecord = new HitRecord();

            if(hitableList.Hit(ray, 0.001f, float.MaxValue, ref hitRecord))
            {
                var target = hitRecord.P + hitRecord.Normal + RandomInUnitSphere();
                return 0.5f * PixelColor(new Ray(hitRecord.P, target - hitRecord.P), hitableList);
            }
            else
            {
                float t = 0.5f * (Vector3.Normalize(ray.Direction).Y + 1);

                //  lerp => blended_value = (1-t)*start_value + t*end_value​
                return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
            }
        }

        private Vector3 RandomInUnitSphere()
        {
            Vector3 p = new Vector3();

            do
            {
                p = 2.0f * new Vector3(rand.NextFloat(), rand.NextFloat(), rand.NextFloat()) - Vector3.One;
            } while(p.LengthSquared() >= 1);

            return p;
        }
    }
}