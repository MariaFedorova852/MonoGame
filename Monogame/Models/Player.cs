using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.objects;
using Monogame.ProgressBar;
using MonoGame.Extended;
using System.Linq;
using static Monogame.GameManager;
using System.Timers;

namespace Monogame.Models
{
    public class Player : IEntity , IObject
    {
        public RectangleF currentAttack;
        public Vector2 position;
        public Point currentFrame;

        public Vector2 pos { get => position; set => position = value; }
        public HealthBar healthPoint { get; set; }
        public bool isAlive { get => healthPoint.currentValue > 0; }
        public float speed { get; set; }

        public bool isImmunity { get; set; }
        public bool isMovingLeft { get; set; }
        public bool isMovingRight { get; set; }
        public bool isMovingUp { get; set; }
        public bool isMovingDown { get; set; }
        public bool isAttack { get; set; }
        public bool deathAnimationFlag { get; set; }
        public bool flag { get; set; }

        public int currentLimit { get; set; }
        public int idleFrames { get; set; }
        public int runFrames { get; set; }
        public int attackFrames { get; set; }
        public int deathFrames { get; set; }

        public int spriteSize { get; set; }
        public int size { get; set; }
        public SpriteEffects flip { get; set; }

        public Texture2D spriteSheet { get; set; }

        public Vector2 direction { get ; set; }
        public RectangleF hitBox => new RectangleF(position.X + 80, position.Y + 142, 36, 20);
        //public RectangleF hitBox => new RectangleF(position.X, position.Y, 192, 192);
        public RectangleF attackUp => new RectangleF(position.X + 61, position.Y + 88, 72, 20);
        public RectangleF attackDown => new RectangleF(position.X + 61, position.Y + 172, 72, 20);
        public RectangleF attackRight => new RectangleF(position.X + 140, position.Y + 128, 24, 48);
        public RectangleF attackLeft => new RectangleF(position.X + 28, position.Y + 128, 24, 48);

        public int delta => 168;
        private static Vector2 _minPos, _maxPos;
        private Timer immunityTimer = new Timer(750);

        int currentTime = 0;
        int period = 5;

        public Player(Vector2 position, ICreature model, Texture2D spriteSheet)
        {
            this.position = position;
            idleFrames = model.idleFrames;
            runFrames = model.runFrames;
            attackFrames = model.attackFrames;
            deathFrames = model.deathFrames;
            this.spriteSheet = spriteSheet;
            currentFrame = new Point(0, 0);
            flip = SpriteEffects.None;
            currentLimit = idleFrames;
            size = model.size;
            speed = model.speed;
            immunityTimer.Elapsed += ImmunityTimer_Elapsed;
            healthPoint = new HealthBar(null, null, 100, new Vector2(Globals.WindowSize.X - 50, Globals.WindowSize.Y - 200));
        }
        private void ImmunityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            isImmunity = false;
            immunityTimer.Stop();
        }

        public void Update()
        {
            var flag1 = true;
            var flag2 = true;
            var dirX = PlayerController.Direction.X * Globals.Time * speed;
            var dirY = PlayerController.Direction.Y * Globals.Time * speed;
            healthPoint.Update();
            
            if (isImmunity && !immunityTimer.Enabled)
            {
                immunityTimer.Start();
            }

            if (isAlive)
            {
                foreach (var e in map.currentLevel.objects)
                {
                    if (new RectangleF(hitBox.X + dirX, hitBox.Y, hitBox.Width, hitBox.Height).Intersects(e.hitBox))
                    {
                        flag1 = false;
                    }
                    if (new RectangleF(hitBox.X, hitBox.Y + dirY, hitBox.Width, hitBox.Height).Intersects(e.hitBox))
                    {
                        flag2 = false;
                    }
                }

                if (flag1) position.X += dirX;
                if (flag2) position.Y += dirY;

                position = Vector2.Clamp(position, _minPos, _maxPos);
            }

            if (!flag && ++currentTime > period)
            {
                currentTime = 0;
                currentFrame.X = ++currentFrame.X % currentLimit;
                SetRunAnimation();
            }
        }

        public bool IsMoving() => isMovingDown || isMovingUp || isMovingLeft || isMovingRight;

        public void Draw()
        {
            Globals.SpriteBatch.Draw(spriteSheet, position,
                new Rectangle(currentFrame.X * size, currentFrame.Y * size, size, size),
                Color.White, 0, Vector2.Zero, 1, flip, 0);
            //Globals.SpriteBatch.DrawRectangle(hitBox, Color.Black);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 192, delta), Color.Black);
            //Globals.SpriteBatch.DrawRectangle(currentAttack, Color.Black);

            if (!isAlive)
            {
                StopEntity();
                if (!deathAnimationFlag) SetAnimation(9);
                deathAnimationFlag = true;
                if (currentFrame.X == 2) flag = true;
            }

            if (isAttack)
            {
                StopEntity();
                if (currentFrame.X == 3)
                {
                    isAttack = false;
                    SetAnimationAfterAttack();
                }
            }
        }

        public void SetRunAnimation()
        {
            if (isMovingRight || isMovingLeft) currentFrame.Y = 4;
            if (isMovingUp) currentFrame.Y = 5;
            else if (isMovingDown) currentFrame.Y = 3;
            currentLimit = runFrames;
        }

        public void SetAnimationAfterAttack()
        {
            switch (currentFrame.Y)
            {
                case 8:
                    SetAnimation(2);
                    break;
                case 6:
                    SetAnimation(0);
                    break;
                case 7:
                    SetAnimation(1);
                    break;
            }
        }

        public void SetAnimation(int currentAnimation)
        {
            currentFrame.Y = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                case 1:
                case 2:
                    currentLimit = runFrames; break;
                case 6:
                case 7:
                case 8:
                    currentFrame.X = 0;
                    currentLimit = attackFrames; break;
                case 9:
                    currentFrame.X = 0;
                    currentLimit = deathFrames; break;
            }
        }

        public void StopEntity()
        {
            PlayerController.Direction = Vector2.Zero;
            isMovingUp = false;
            isMovingDown = false;
            isMovingLeft = false;
            isMovingRight = false;
        }

        public static void SetBounds(Point mapSize, Point tileSize)
        {
            _minPos = new(-tileSize.X / 2 + 20, -tileSize.Y);
            _maxPos = new(mapSize.X - (2 * tileSize.X + tileSize.X / 2 + 20), mapSize.Y - (3 * tileSize.X + 24));
        }

        public void Attack()
        {
            foreach (var entity in map.currentLevel.enemys.Where(x => x.GetType() != typeof(Player)))
            {
                if (currentAttack.Intersects(entity.hitBox))
                {
                    entity.healthPoint.currentValue -= 10;
                    entity.direction = -entity.direction;
                }
            }
        }
    }
}
