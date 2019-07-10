using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Program
{
    public sealed class Engine : Game
    {
        private readonly int X = 800;
        private readonly int Y = 400;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GUI _gui;
        private IRenderer _tracer;

        private Texture2D _frameBuffer;

        public Engine()
        {
            // Manager must be set before Initialize is called
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = X,
                PreferredBackBufferHeight = Y
            };
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gui = new GUI(_spriteBatch, X, Y);
            _tracer = new RayTracer(X, Y);
            _frameBuffer = new Texture2D(_graphics.GraphicsDevice, X, Y);

            Content.RootDirectory = "Content";
            Window.AllowUserResizing = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _gui.Font = Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _frameBuffer.SetData(_tracer.NextFrame());

            _spriteBatch.Begin();
            _spriteBatch.Draw(_frameBuffer, new Vector2(0, 0), Color.White);
            _gui.DrawFPS(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}