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
        private SpriteFont font;

        private IRenderer _renderer;
        private Texture2D _frame;

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
            _frame = new Texture2D(_graphics.GraphicsDevice, X, Y);
            Content.RootDirectory = "Content";

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

            //_rect.SetData(_renderer.NextFrame());

            _spriteBatch.Begin();
            // _spriteBatch.Draw(_frame, new Vector2(0, 0), Color.White);
            // TODO: Position FPS based on font size.
            _spriteBatch.DrawString(font, ((int)(1 / gameTime.ElapsedGameTime.TotalSeconds)).ToString(), new Vector2(X - X / 20, 0 + Y / 20), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}