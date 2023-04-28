using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using MonoGame.Extended;
using static Monogame.GameManager;

namespace Monogame.Models
{
    public class Slime : IEntity
    {
        public Rectangle currentAttack;
        public Vector2 position;
        public Point currentFrame;
        public int healthPoint { get; set; }
        public bool isAlive { get; set; }
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

        private static Vector2 _minPos, _maxPos;

        int currentTime = 0;
        int period = 8;

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
            isAlive = true;
        }

        public void Update()
        {

            if (!flag && ++currentTime > period)
            {
                currentTime = 0;
                currentFrame.X = ++currentFrame.X % currentLimit;
            }
            
            if (isAlive) Attack();
        }

        public bool IsMoving() => isMovingDown || isMovingUp || isMovingLeft || isMovingRight;

        public void Draw()
        {
            Globals.SpriteBatch.Draw(spriteSheet, position,
                new Rectangle(currentFrame.X * size,
                currentFrame.Y * size, size, size),
                Color.White, 0, Vector2.Zero, 1, flip, 0);
            Globals.SpriteBatch.DrawRectangle(hitBox, Color.Black);

            if (!isAlive)
            {
                if (!deathAnimationFlag) SetAnimation(4);
                deathAnimationFlag = true;
                if (currentFrame.X == 4) flag = true;
            }
        }

        public void SetRunAnimation()
        {
            if (IsMoving()) currentFrame.Y = 1;
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
            switch (currentAnimation)
            {
                case 4:
                    currentFrame.X = 0;
                    currentFrame.Y = currentAnimation;
                    currentLimit = deathFrames;
                    break;
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

        public void SetBounds(Point mapSize, Point tileSize)
        {
            _minPos = new(-tileSize.X / 2 + 20, -tileSize.Y);
            _maxPos = new(mapSize.X - (2 * tileSize.X + tileSize.X / 2 + 20), mapSize.Y - (3 * tileSize.X + 24));
        }

        public void Attack()
        {
            if (player.hitBox.Intersects(hitBox)) player.isAlive = false;
        }
    }
}
