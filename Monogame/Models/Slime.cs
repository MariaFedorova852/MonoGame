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

        public Vector2 pos { get => position; set => position = value; }
        public HealthBar healthPoint { get; set; }
        public bool isAlive { get => healthPoint.currentValue > 0; }
        public float speed { get; set; }

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
        public RectangleF hitBox => new RectangleF(position.X + 40, position.Y + 52, 48, 40);

        public int delta => 90;

        private static Vector2 _minPos, _maxPos;
        public Vector2 direction { get; set; }

        int currentTime = 0;
        int period = 8;
        private int attackTime = 20;
        private int attackPeriod = 100;

        public Slime(Vector2 position, ICreature model, Texture2D spriteSheet)
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
            healthPoint = new HealthBar(null, null, 20, new Vector2(100, 100));
        }

        private void Move()
        {
            direction = Vector2.Normalize(new Vector2(player.hitBox.Right / 2 - hitBox.Right / 2,
                player.hitBox.Bottom / 2 - hitBox.Bottom / 2));
        }

        public void Update()
        {
            var flag1 = true;
            var flag2 = true;
            var dirX = direction.X * Globals.Time * speed;
            var dirY = direction.Y * Globals.Time * speed;

            if (isAlive)
            {
                if (++attackTime > attackPeriod && !isAttack && GetDistance(hitBox, player.hitBox) < 120)
                {
                    SetAnimation(2);
                    Move();
                    flip = player.hitBox.Right / 2 > hitBox.Right / 2 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    isAttack = true;
                    attackTime = 0;
                }

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
            }

            if (isAlive) Attack();
        }

        public double GetDistance(RectangleF rS, RectangleF rP)
        {
            return Math.Sqrt(Math.Pow((rS.Right / 2) - (rP.Right / 2), 2)
               + Math.Pow(rS.Bottom / 2 - (rP.Bottom / 2), 2));
        }

        public bool IsMoving() => isMovingDown || isMovingUp || isMovingLeft || isMovingRight;

        public void Draw()
        {
            Globals.SpriteBatch.Draw(spriteSheet, position,
                new Rectangle(currentFrame.X * size,
                currentFrame.Y * size, size, size),
                Color.White, 0, Vector2.Zero, 1, flip, 0);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 128, delta), Color.Black);

            if (!isAlive)
            {
                if (!deathAnimationFlag) SetAnimation(4);
                deathAnimationFlag = true;
                if (currentFrame.X == 4) flag = true;
            }

            if (isAttack && currentFrame.X == attackFrames - 1)
            {
                SetAnimation(0);
                isAttack = false;
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
                    currentLimit = idleFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = 0;
                    break;
                case 2:
                    currentLimit = attackFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = currentAnimation;
                    break;
                case 4:
                    currentLimit = deathFrames;
                    currentFrame.X = 0;
                    currentFrame.Y = currentAnimation;
                    break;
            }
        }

        public void StopEntity()
        {
            direction = Vector2.Zero;
            isMovingUp = false;
            isMovingDown = false;
            isMovingLeft = false;
            isMovingRight = false;
        }

        public static void SetBounds(Point mapSize, Point tileSize)
        {
            _minPos = new(tileSize.X / 2 - 16, tileSize.Y / 2 - 20);
            _maxPos = new(mapSize.X - (2 * tileSize.X + 20), mapSize.Y - (2 * tileSize.X + 10));
        }

        public void Attack()
        {
            if (!player.isImmunity && player.hitBox.Intersects(hitBox))
            {
                player.isImmunity = true;
                player.healthPoint.currentValue -= 10;
            }
        }
    }
}
