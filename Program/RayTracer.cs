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

        public RayTracer(int x, int y)
        {
            width = x;
            height = y;
            frameBuffer = new Color[width * height];
        }

        public Color[] NextFrame()
        {
            for(int y = height - 1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    var pixelColor = new Color();
                    pixelColor.R = (byte)(255.99 * ((float)x / width));
                    pixelColor.G = (byte)(255.99 * ((float)y / height));
                    pixelColor.B = (byte)(255.99 * 0.2);
                    pixelColor.A = 255;

                    // chapter 1 outputs the image to the image file, we're flipping the
                    // row pointer so that we start from the other end in order to get same visual result.
                    frameBuffer[(height - 1 - y) * width + x] = pixelColor;
                }
            }

            return frameBuffer;
        }
    }
}