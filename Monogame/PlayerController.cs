using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Monogame.GameManager;

namespace Monogame
{
    public class PlayerController
    {
        private static Vector2 direction;
        public static Vector2 Direction { get { return direction; } set { direction = value; } }

        public static void Update()
        {
            var key = Keyboard.GetState();
            direction = Vector2.Zero;

            #region Move
            if (player.isAlive && !player.isAttack)
            {
                if (key.IsKeyDown(Keys.W))
                {
                    direction.Y--;
                    player.isMovingUp = true;
                }
                if (key.IsKeyDown(Keys.S))
                {
                    direction.Y++;
                    player.isMovingDown = true;
                }
                if (key.IsKeyDown(Keys.A))
                {
                    direction.X--;
                    player.isMovingLeft = true;
                    player.flip = SpriteEffects.FlipHorizontally;
                }
                if (key.IsKeyDown(Keys.D))
                {
                    direction.X++;
                    player.isMovingRight = true;
                    player.flip = SpriteEffects.None;
                }

                if (key.IsKeyUp(Keys.W) && player.isMovingUp)
                {
                    player.isMovingUp = false;
                    if (player.isAlive && !player.IsMoving()) player.currentFrame.Y = 2;
                }
                if (key.IsKeyUp(Keys.S) && player.isMovingDown)
                {
                    player.isMovingDown = false;
                    if (player.isAlive && !player.IsMoving()) player.currentFrame.Y = 0;
                }
                if (key.IsKeyUp(Keys.A) && player.isMovingLeft)
                {
                    player.isMovingLeft = false;
                    if (player.isAlive && !player.IsMoving()) player.currentFrame.Y = 1;
                }
                if (key.IsKeyUp(Keys.D) && player.isMovingRight)
                {
                    player.isMovingRight = false;
                    if (player.isAlive && !player.IsMoving()) player.currentFrame.Y = 1;
                }
            }
            #endregion

            if (player.isAlive && !player.isAttack)
            {
                if (key.IsKeyDown(Keys.Up))
                {
                    player.SetAnimation(8);
                    player.isAttack = true;
                    player.currentAttack = player.attackUp;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Down))
                {
                    player.SetAnimation(6);
                    player.isAttack = true;
                    player.currentAttack = player.attackDown;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Left))
                {
                    player.SetAnimation(7);
                    player.isAttack = true;
                    player.flip = SpriteEffects.FlipHorizontally;
                    player.currentAttack = player.attackLeft;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Right))
                {
                    player.SetAnimation(7);
                    player.isAttack = true;
                    player.flip = SpriteEffects.None;
                    player.currentAttack = player.attackRight;
                    player.Attack();
                }
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }
    }
}
