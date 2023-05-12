using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameManager gameManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.WindowSize = new(960, 704);
            graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            graphics.ApplyChanges();

            Globals.Content = Content;
            gameManager = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); 
            Globals.Update(gameTime);
            gameManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.Blue);
            gameManager.Draw();
            base.Draw(gameTime);
        }
    }
}