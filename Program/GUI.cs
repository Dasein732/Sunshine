using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Program
{
    public class GUI
    {
        public SpriteFont Font { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        private readonly SpriteBatch _spriteBatch;

        public GUI(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public GUI(SpriteBatch spriteBatch, int x, int y)
        {
            _spriteBatch = spriteBatch;
            X = x;
            Y = y;
        }

        public void DrawFPS(GameTime gametime) =>
            _spriteBatch.DrawString(Font, FPS(gametime), new Vector2(X - X / 20, 0 + Y / 20), Color.Black);

        public static string FPS(GameTime gametime) => FPS(gametime.ElapsedGameTime.TotalSeconds);

        public static string FPS(double elapsedSeconds) => ((int)(1 / elapsedSeconds)).ToString();
    }
}