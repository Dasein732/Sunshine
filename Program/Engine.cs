using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Program
{
    public sealed class Engine : Game
    {
        private int X = 800;
        private int Y = 400;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        private IRenderer _tracer;
        private Texture2D _frameContainer;

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
            _frameContainer = new Texture2D(_graphics.GraphicsDevice, X, Y);
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Font");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //_frameContainer.SetData(_tracer.NextFrame());

            _spriteBatch.Begin();
            // _spriteBatch.Draw(_frameContainer, new Vector2(0, 0), Color.White);
            // TODO: Position FPS based on font size.
            _spriteBatch.DrawString(font, GUI.FPS(gameTime), new Vector2(X - X / 20, 0 + Y / 20), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}