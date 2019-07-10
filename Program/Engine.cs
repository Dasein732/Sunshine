using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Program
{
    public sealed class Engine : Game
    {
        private int X = 800;
        private int Y = 400;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IRenderer _renderer;
        private Texture2D frameResult;

        public Engine()
        {
            // Manager must be set before Initialize is called
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = X;
            _graphics.PreferredBackBufferHeight = Y;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            frameResult = new Texture2D(_graphics.GraphicsDevice, X, Y);

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