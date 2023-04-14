using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame.Models;

namespace Monogame
{
    public class Game1 : Game
    {
        public Texture2D playerSheet;
        public static Entity player;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        int currentTime = 0;
        int period = 90;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // Создание персонажа по модели Player
            player = new Entity(new Vector2(0, 0),
                    Player.idleFrames,
                    Player.runFrames,
                    Player.attackFrames,
                    Player.deathFrames,
                    playerSheet);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSheet = Content.Load<Texture2D>("playerEnlarged++");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); 
            
            currentTime += gameTime.ElapsedGameTime.Milliseconds;

            // частота обновления анимации
            if (currentTime > period)
            {
                currentTime -= period;
                player.currentFrame.X = ++player.currentFrame.X % player.currentLimit;
                player.SetRunAnimation();
            }

            KeyboardState key = Keyboard.GetState();
            
            // Контроллер - управелние персонажем
            #region
            if (key.IsKeyDown(Keys.W))
            {
                player.position.Y -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.isMovingUp = true;
            }
            if (key.IsKeyDown(Keys.S))
            {
                player.position.Y += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.isMovingDown = true;
            }
            if (key.IsKeyDown(Keys.A))
            {
                player.position.X -= player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.isMovingLeft = true;
                player.flip = SpriteEffects.FlipHorizontally;
            }
            if (key.IsKeyDown(Keys.D))
            {
                player.position.X += player.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.isMovingRight = true;
                player.flip = SpriteEffects.None;
            }

            if (key.IsKeyUp(Keys.W) && player.isMovingUp)
            {
                player.isMovingUp = false;
                if (!player.IsMoving()) player.currentFrame.Y = 2;
            }
            if (key.IsKeyUp(Keys.S) && player.isMovingDown)
            {
                player.isMovingDown = false;
                if (!player.IsMoving()) player.currentFrame.Y = 0;
            }
            if (key.IsKeyUp(Keys.A) && player.isMovingLeft)
            {
                player.isMovingLeft = false;
                if (!player.IsMoving()) player.currentFrame.Y = 1;
            }
            if (key.IsKeyUp(Keys.D) && player.isMovingRight)
            {
                player.isMovingRight = false;
                if (!player.IsMoving()) player.currentFrame.Y = 1;
            }
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            
            spriteBatch.Draw(playerSheet, player.position,
                new Rectangle(player.currentFrame.X * player.size,
                player.currentFrame.Y * player.size, player.size, player.size),
                Color.White, 0, Vector2.Zero, 1, player.flip, 0);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }

    public class Entity
    {
        public Texture2D spriteSheet;
        public Vector2 position;
        public float speed;

        public bool isMovingLeft = false;
        public bool isMovingRight = false;
        public bool isMovingUp = false;
        public bool isMovingDown = false;
        public bool isAttack = false;

        public Point currentFrame;
        public int currentLimit;
        public int idleFrames;
        public int runFrames;
        public int attackFrames;
        public int deathFrames;

        public int size;
        public SpriteEffects flip;

        public Entity(Vector2 position, int idleFrames, int runFrames, int attackFrames, int deathFrames, Texture2D spriteSheet)
        {
            this.position = position;
            this.idleFrames = idleFrames;
            this.runFrames = runFrames;
            this.attackFrames = attackFrames;
            this.deathFrames = deathFrames;
            this.spriteSheet = spriteSheet;
            currentFrame = new Point(0, 0);
            flip = SpriteEffects.None;
            currentLimit = idleFrames;
            size = 192;
            speed = 200;
        }

        public void SetRunAnimation()
        {
            if (isMovingRight || isMovingLeft) currentFrame.Y = 4;
            if (isMovingUp) currentFrame.Y = 5;
            else if (isMovingDown) currentFrame.Y = 3;
            currentLimit = runFrames;
        }

        public bool IsMoving() => isMovingDown || isMovingUp || isMovingLeft || isMovingRight;
    }
}