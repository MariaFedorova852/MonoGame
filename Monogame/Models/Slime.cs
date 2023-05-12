using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.ProgressBar;
using MonoGame.Extended;
using System;
using static Monogame.GameManager;

namespace Monogame.Models
{
    public class Slime : IEntity, IObject
    {
        public Rectangle currentAttack;
        public Vector2 position;
        public Point currentFrame;

        public Vector2 Pos { get => position; set => position = value; }
        public HealthBar HealthPoint { get; set; }
        public bool IsAlive { get => HealthPoint.currentValue > 0; }
        public float Speed { get; set; }

        public bool IsMovingLeft { get; set; }
        public bool IsMovingRight { get; set; }
        public bool IsMovingUp { get; set; }
        public bool IsMovingDown { get; set; }
        public bool IsAttack { get; set; }
        public bool DeathAnimationFlag { get; set; }
        public bool flag { get; set; }

        public int CurrentLimit { get; set; }
        public int IdleFrames { get; set; }
        public int RunFrames { get; set; }
        public int AttackFrames { get; set; }
        public int DeathFrames { get; set; }

        public int SpriteSize { get; set; }
        public int Size { get; set; }
        public SpriteEffects Flip { get; set; }

        public Texture2D SpriteSheet { get; set; }
        public RectangleF HitBox => new(position.X + 40, position.Y + 52, 48, 40);

        public int Delta => 90;

        private static Vector2 _minPos, _maxPos;
        public Vector2 Direction { get; set; }

        private readonly int attackPeriod = 100;
        readonly int period = 8;
        private int attackTime = 20;
        private int currentTime = 0;

        public Slime(Vector2 position, ICreature model, Texture2D spriteSheet)
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
            HealthPoint = new HealthBar(null, null, 20, new Vector2(100, 100));
        }

        private void Move()
        {
            Direction = Vector2.Normalize(new Vector2(player.HitBox.Right / 2 - HitBox.Right / 2,
                player.HitBox.Bottom / 2 - HitBox.Bottom / 2));
        }

        public void Update()
        {
            if (IsAlive)
            {
                AttackMove();
                CollisionAndMove();
                Attack();
            }

            if (!flag && ++currentTime > period)
            {
                currentTime = 0;
                currentFrame.X = ++currentFrame.X % CurrentLimit;
            }
        }

        private void AttackMove()
        {
            if (++attackTime > attackPeriod && !IsAttack && GetDistance(HitBox, player.HitBox) < 120)
            {
                SetAnimation(2);
                Move();
                Flip = player.HitBox.Right / 2 > HitBox.Right / 2 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                IsAttack = true;
                attackTime = 0;
            }
        }

        private void CollisionAndMove()
        {
            var flag1 = true;
            var flag2 = true;
            var dirX = Direction.X * Globals.Time * Speed;
            var dirY = Direction.Y * Globals.Time * Speed;

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
        }

        private static double GetDistance(RectangleF rS, RectangleF rP)
        {
            return Math.Sqrt(Math.Pow((rS.Right / 2) - (rP.Right / 2), 2)
               + Math.Pow(rS.Bottom / 2 - (rP.Bottom / 2), 2));
        }

        public bool IsMoving() => IsMovingDown || IsMovingUp || IsMovingLeft || IsMovingRight;

        public void Draw()
        {
            Globals.SpriteBatch.Draw(SpriteSheet, position,
                new Rectangle(currentFrame.X * Size,
                currentFrame.Y * Size, Size, Size),
                Color.White, 0, Vector2.Zero, 1, Flip, 0);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 128, delta), Color.Black);

            if (!IsAlive)
            {
                if (!DeathAnimationFlag) SetAnimation(4);
                DeathAnimationFlag = true;
                if (currentFrame.X == 4) flag = true;
            }

            if (IsAttack && currentFrame.X == AttackFrames - 1)
            {
                SetAnimation(0);
                IsAttack = false;
                StopEntity();
            }
        }

        public void SetRunAnimation() { }

        public void SetAnimationAfterAttack() { }

        public void SetAnimation(int currentAnimation)
        {
            switch (currentAnimation)
            {
                case 0:
                    CurrentLimit = IdleFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = 0;
                    break;
                case 2:
                    CurrentLimit = AttackFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = currentAnimation;
                    break;
                case 4:
                    CurrentLimit = DeathFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = currentAnimation;
                    break;
            }
        }

        public void StopEntity()
        {
            Direction = Vector2.Zero;
            IsMovingUp = false;
            IsMovingDown = false;
            IsMovingLeft = false;
            IsMovingRight = false;
        }

        public void SetBounds(Point mapSize, Point tileSize)
        {
            _minPos = new(tileSize.X / 2 - 16, tileSize.Y / 2 - 20);
            _maxPos = new(mapSize.X - (2 * tileSize.X + 20), mapSize.Y - (2 * tileSize.X + 10));
        }

        public void Attack()
        {
            if (!player.IsImmunity && player.HitBox.Intersects(HitBox))
            {
                player.IsImmunity = true;
                player.HealthPoint.currentValue -= 20;
            }
        }
    }
}
