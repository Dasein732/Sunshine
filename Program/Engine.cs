using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Program
{
    public class Engine : Game
    {
        private const int X = 800;
        private const int Y = 400;

        private readonly GraphicsDeviceManager _graphics;
        private readonly SpriteBatch _spriteBatch;
        private readonly IRenderer _renderer;
        private readonly Texture2D _rect;

        public Engine()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rect = new Texture2D(_graphics.GraphicsDevice, X, Y);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = X;
            _graphics.PreferredBackBufferHeight = Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //_rect.SetData(_renderer.NextFrame());

            _spriteBatch.Begin();
            // _spriteBatch.Draw(_rect, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}