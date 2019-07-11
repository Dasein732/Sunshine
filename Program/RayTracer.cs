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

                    frameBuffer[index].R = (byte)(255.99 * ((float)x / width));
                    frameBuffer[index].G = (byte)(255.99 * ((float)y / height));
                    frameBuffer[index].B = (byte)(255.99 * 0.2);
                    frameBuffer[index].A = 255;
                }
            }

            return frameBuffer;
        }
    }
}