using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Program.Materials;
using Program.Surfaces;
using Sunshine.Utility;

namespace Program
{
    public sealed class RayTracer : IRenderer
    {
        private readonly Color[] frameBuffer;
        private readonly Camera camera;

        // TODO: create some sort of entity manager or scene graph later on
        private readonly HitableList World;

        private readonly RendererConfiguration renderConfig;

        public RayTracer(RendererConfiguration renderConfig)
        {
            this.renderConfig = renderConfig;
            frameBuffer = new Color[renderConfig.Width * renderConfig.Height];

            for(int i = 0; i < frameBuffer.Length; i++)
            {
                frameBuffer[i] = new Color();
            }

            World = new HitableList(RandomScene());

            camera = new Camera(
                    new Vector3(13f, 2f, 3f),
                    new Vector3(0f, 0f, 0f),
                    new Vector3(0f, 1f, 0f),
                    20,
                    (float)renderConfig.Width / renderConfig.Height,
                    0.1f,
                    10f
                );
        }

        private List<Hitable> RandomScene()
        {
            var result = new List<Hitable>();

            result.Add(new Sphere(new Vector3(0f, -1000f, 0f), 1000f, new Lambertian(new Vector3(0.5f, 0.5f, 0.5f))));
            result.Add(new Sphere(new Vector3(0f, 1f, 0f), 1f, new Dielectric(1.5f)));
            result.Add(new Sphere(new Vector3(-4f, 1f, 0f), 1f, new Lambertian(new Vector3(0.4f, 0.2f, 0.1f))));
            result.Add(new Sphere(new Vector3(4f, 1f, 0f), 1f, new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0.0f)));

            for(int a = -11; a < 11; a++)
            {
                for(int b = -11; b < 11; b++)
                {
                    float material = XorShiftRandom.NextFloat();
                    Vector3 center = new Vector3(a + 0.9f * XorShiftRandom.NextFloat(), 0.2f, b + 0.9f * XorShiftRandom.NextFloat());

                    if((center - new Vector3(4f, 0f, 0.2f)).Length() > 0.9f)
                    {
                        if(material < 0.8f)
                        {
                            result.Add(new Sphere(center, 0.2f, new Lambertian(new Vector3(XorShiftRandom.NextFloat() * XorShiftRandom.NextFloat(), XorShiftRandom.NextFloat() * XorShiftRandom.NextFloat(), XorShiftRandom.NextFloat() * XorShiftRandom.NextFloat()))));
                        }
                        else if(material < 0.95f)
                        {
                            result.Add(new Sphere(center, 0.2f, new Metal(new Vector3(0.5f * (1 + XorShiftRandom.NextFloat()), 0.5f * (1 + XorShiftRandom.NextFloat()), 0.5f * (1 + XorShiftRandom.NextFloat())), 0.5f * XorShiftRandom.NextFloat())));
                        }
                        else
                        {
                            result.Add(new Sphere(center, 0.2f, new Dielectric(1.5f)));
                        }
                    }
                }
            }

            return result;
        }

        public Color[] NextFrame()
        {
            Parallel.For(0, renderConfig.Height, y =>
            {
                Parallel.For(0, renderConfig.Width, x =>
                {
                    // chapter 1 outputs the image to the image file, we're flipping the
                    // row pointer so that we start from the other end in order to get same visual result.
                    var index = (renderConfig.Height - 1 - y) * renderConfig.Width + x;

                    if(renderConfig.Antialiasing)
                    {
                        Vector3 color = new Vector3();

                        for(int i = 0; i < renderConfig.AASamples; i++)
                        {
                            float u = (x + XorShiftRandom.NextFloat()) / renderConfig.Width;
                            float v = (y + XorShiftRandom.NextFloat()) / renderConfig.Height;

                            color += PixelColor(camera.GetRay(u, v), World, 0);
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

                        var color = PixelColor(camera.GetRay(u, v), World, 0);

                        color = new Vector3(MathUtil.FastSqrt(color.X), MathUtil.FastSqrt(color.Y), MathUtil.FastSqrt(color.Z));

                        color *= 255.99f;
                        frameBuffer[index].R = (byte)color.X;
                        frameBuffer[index].G = (byte)color.Y;
                        frameBuffer[index].B = (byte)color.Z;
                        frameBuffer[index].A = 255;
                    }
                });
            });

            return frameBuffer;
        }

        private Vector3 PixelColor(in Ray ray, HitableList hitableList, int depth)
        {
            var hitRecord = new HitRecord();

            if(hitableList.Hit(ray, 0.001f, float.MaxValue, ref hitRecord))
            {
                var scattered = new Ray();
                var attenuation = new Vector3();

                if(depth < 50 && hitRecord.Material.Scatter(ray, hitRecord, ref attenuation, ref scattered))
                {
                    return attenuation * PixelColor(scattered, World, depth + 1);
                }
                else
                {
                    return Vector3.Zero;
                }
            }
            else
            {
                float t = 0.5f * (Vector3.Normalize(ray.Direction).Y + 1);

                //  lerp => blended_value = (1-t)*start_value + t*end_value​
                return (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
            }
        }
    }
}