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
            // Создание персонажа по модели Player
            player = new Entity(new Vector2(0, 0),
                    Player.idleFrames, //Отвечают за количество кадров анимации
                    Player.runFrames,
                    Player.attackFrames,
                    Player.deathFrames,
                    playerSheet); 
        }

        protected override void Initialize()
        {
            Globals.WindowSize = new(1024, 768);
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
            GraphicsDevice.Clear(Color.White);

            gameManager.Draw();

            base.Draw(gameTime);
        }
    }
}