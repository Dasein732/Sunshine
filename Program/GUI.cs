using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Program
{
    public static class GUI
    {
        public static string FPS(double elapsedSeconds) => ((int)(1 / elapsedSeconds)).ToString();

        public static string FPS(GameTime gametime) => FPS(gametime.ElapsedGameTime.TotalSeconds);
    }
}