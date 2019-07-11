using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Program
{
    public sealed class Engine : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly RendererConfiguration _renderConfig;

        private GUI _gui;
        private IRenderer _tracer;

        private Texture2D _frameBuffer;

        public Engine(int x, int y)
        {
            _renderConfig = new RendererConfiguration
            {
                Width = x,
                Height = y
            };

            // Manager must be set before Initialize is called
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = _renderConfig.Width,
                PreferredBackBufferHeight = _renderConfig.Height
            };
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _renderConfig.Antialiasing = true;
            _renderConfig.AASamples = 8;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gui = new GUI(_spriteBatch, _renderConfig.Width, _renderConfig.Height);
            _tracer = new RayTracer(_renderConfig);
            _frameBuffer = new Texture2D(_graphics.GraphicsDevice, _renderConfig.Width, _renderConfig.Height);
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