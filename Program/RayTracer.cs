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
            return frameBuffer;
        }
    }
}