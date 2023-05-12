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

        public Vector2 Pos { get => position; set => position = value; }
        public HealthBar HealthPoint { get; set; }
        public bool IsAlive { get => HealthPoint.currentValue > 0; }
        public float Speed { get; set; }

        public bool IsImmunity { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool IsMovingRight { get; set; }
        public bool IsMovingUp { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsAttack { get; set; }
        public bool DeathAnimationFlag { get; set; }
        public bool Flag { get; set; }

        public int CurrentLimit { get; set; }
        public int IdleFrames { get; set; }
        public int RunFrames { get; set; }
        public int AttackFrames { get; set; }
        public int DeathFrames { get; set; }

        public int SpriteSize { get; set; }
        public int Size { get; set; }
        public SpriteEffects Flip { get; set; }

        public Texture2D SpriteSheet { get; set; }

        public Vector2 Direction { get ; set; }
        public RectangleF HitBox => new RectangleF(position.X + 80, position.Y + 142, 36, 20);
        public RectangleF attackUp => new RectangleF(position.X + 61, position.Y + 88, 72, 20);
        public RectangleF attackDown => new RectangleF(position.X + 61, position.Y + 172, 72, 20);
        public RectangleF attackRight => new RectangleF(position.X + 140, position.Y + 128, 24, 48);
        public RectangleF attackLeft => new RectangleF(position.X + 28, position.Y + 128, 24, 48);

        public int Delta => 168;
        private static Vector2 _minPos, _maxPos;
        private readonly Timer immunityTimer = new(750);

        readonly int period = 5;
        private int currentTime = 0;

        public Player(Vector2 position, ICreature model, Texture2D spriteSheet)
        {
            this.position = position;
            IdleFrames = model.IdleFrames;
            RunFrames = model.RunFrames;
            AttackFrames = model.AttackFrames;
            DeathFrames = model.DeathFrames;
            SpriteSheet = spriteSheet;
            currentFrame = new Point(0, 0);
            Flip = SpriteEffects.None;
            CurrentLimit = IdleFrames;
            Size = model.Size;
            Speed = model.Speed;
            immunityTimer.Elapsed += ImmunityTimer_Elapsed;
            HealthPoint = new HealthBar(null, null, 100, new Vector2(Globals.WindowSize.X - 50, Globals.WindowSize.Y - 200));
        }
        private void ImmunityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsImmunity = false;
            immunityTimer.Stop();
        }

        public void Update()
        {
            HealthPoint.Update();
            
            if (IsImmunity && !immunityTimer.Enabled)
            {
                immunityTimer.Start();
            }

            if (IsAlive)
            {
                CollisionAndMove();
            }

            if (!Flag && ++currentTime > period)
            {
                currentTime = 0;
                currentFrame.X = ++currentFrame.X % CurrentLimit;
                SetRunAnimation();
            }
        }

        private void CollisionAndMove()
        {
            var flag1 = true;
            var flag2 = true;
            var dirX = PlayerController.Direction.X * Globals.Time * Speed;
            var dirY = PlayerController.Direction.Y * Globals.Time * Speed;

            foreach (var e in map.currentLevel.Level.Objects)
            {
                if (new RectangleF(HitBox.X + dirX, HitBox.Y, HitBox.Width, HitBox.Height).Intersects(e.HitBox))
                {
                    flag1 = false;
                }
                if (new RectangleF(HitBox.X, HitBox.Y + dirY, HitBox.Width, HitBox.Height).Intersects(e.HitBox))
                {
                    flag2 = false;
                }
            }
            if (flag1) position.X += dirX;
            if (flag2) position.Y += dirY;

            position = Vector2.Clamp(position, _minPos, _maxPos);
            ChangeMap(dirX);
        }

        private void ChangeMap(float dirX)
        {
            if (map.currentLevel.Level.Entities.All(x => !x.IsAlive))
            {
                if (new RectangleF(HitBox.X + dirX + 5, HitBox.Y, HitBox.Width, HitBox.Height).Intersects(map.currentLevel.Level.Exit))
                {
                    map.ChangeCurrentLevel(true);
                }
                if (new RectangleF(HitBox.X + dirX - 5, HitBox.Y, HitBox.Width, HitBox.Height).Intersects(map.currentLevel.Level.Enter))
                {
                    map.ChangeCurrentLevel(false);
                }
            }
        }

        public bool IsMoving() => IsMovingDown || IsMovingUp || IsMovingLeft || IsMovingRight;

        public void Draw()
        {
            Globals.SpriteBatch.Draw(SpriteSheet, position,
                new Rectangle(currentFrame.X * Size, currentFrame.Y * Size, Size, Size),
                Color.White, 0, Vector2.Zero, 1, Flip, 0);
            //Globals.SpriteBatch.DrawRectangle(HitBox, Color.Black);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 192, delta), Color.Black);
            //Globals.SpriteBatch.DrawRectangle(currentAttack, Color.Black);

            if (!IsAlive)
            {
                StopEntity();
                if (!DeathAnimationFlag) SetAnimation(9);
                DeathAnimationFlag = true;
                if (currentFrame.X == 2) Flag = true;
            }

            if (IsAttack)
            {
                StopEntity();
                if (currentFrame.X == 3)
                {
                    IsAttack = false;
                    SetAnimationAfterAttack();
                }
            }
        }

        public void SetRunAnimation()
        {
            if (IsMovingRight || IsMovingLeft) currentFrame.Y = 4;
            if (IsMovingUp) currentFrame.Y = 5;
            else if (IsMovingDown) currentFrame.Y = 3;
            CurrentLimit = RunFrames;
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
                    CurrentLimit = RunFrames; break;
                case 6:
                case 7:
                case 8:
                    currentFrame.X = 0;
                    CurrentLimit = AttackFrames; break;
                case 9:
                    currentFrame.X = 0;
                    CurrentLimit = DeathFrames; break;
            }
        }

        public void StopEntity()
        {
            PlayerController.Direction = Vector2.Zero;
            IsMovingUp = false;
            IsMovingDown = false;
            IsMovingLeft = false;
            IsMovingRight = false;
        }

        public void SetBounds(Point mapSize, Point tileSize)
        {
            _minPos = new(-tileSize.X / 2 + 20, -tileSize.Y);
            _maxPos = new(mapSize.X - (2 * tileSize.X + tileSize.X / 2 + 20), mapSize.Y - (3 * tileSize.X + 24));
        }

        public void Attack()
        {
            foreach (var entity in map.currentLevel.Level.Entities.Where(x => x.GetType() != typeof(Player)))
            {
                if (currentAttack.Intersects(entity.HitBox))
                {
                    entity.HealthPoint.currentValue -= 10;
                    entity.Direction = -entity.Direction;
                }
            }
        }
    }
}
