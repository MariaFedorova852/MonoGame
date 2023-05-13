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
            if (player.IsAlive && !player.IsAttack)
            {
                if (key.IsKeyDown(Keys.W))
                {
                    direction.Y--;
                    player.IsMovingUp = true;
                }
                if (key.IsKeyDown(Keys.S))
                {
                    direction.Y++;
                    player.IsMovingDown = true;
                }
                if (key.IsKeyDown(Keys.A))
                {
                    direction.X--;
                    player.IsMovingLeft = true;
                    player.Flip = SpriteEffects.FlipHorizontally;
                }
                if (key.IsKeyDown(Keys.D))
                {
                    direction.X++;
                    player.IsMovingRight = true;
                    player.Flip = SpriteEffects.None;
                }

                if (key.IsKeyUp(Keys.W) && player.IsMovingUp)
                {
                    player.IsMovingUp = false;
                    if (player.IsAlive && !player.IsMoving()) player.currentFrame.Y = 2;
                }
                if (key.IsKeyUp(Keys.S) && player.IsMovingDown)
                {
                    player.IsMovingDown = false;
                    if (player.IsAlive && !player.IsMoving()) player.currentFrame.Y = 0;
                }
                if (key.IsKeyUp(Keys.A) && player.IsMovingLeft)
                {
                    player.IsMovingLeft = false;
                    if (player.IsAlive && !player.IsMoving()) player.currentFrame.Y = 1;
                }
                if (key.IsKeyUp(Keys.D) && player.IsMovingRight)
                {
                    player.IsMovingRight = false;
                    if (player.IsAlive && !player.IsMoving()) player.currentFrame.Y = 1;
                }
            }
            #endregion

            if (player.IsAlive && !player.IsAttack)
            {
                if (key.IsKeyDown(Keys.Up))
                {
                    player.SetAnimation(8);
                    player.IsAttack = true;
                    player.currentAttack = player.attackUp;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Down))
                {
                    player.SetAnimation(6);
                    player.IsAttack = true;
                    player.currentAttack = player.attackDown;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Left))
                {
                    player.SetAnimation(7);
                    player.IsAttack = true;
                    player.Flip = SpriteEffects.FlipHorizontally;
                    player.currentAttack = player.attackLeft;
                    player.Attack();
                }
                if (key.IsKeyDown(Keys.Right))
                {
                    player.SetAnimation(7);
                    player.IsAttack = true;
                    player.Flip = SpriteEffects.None;
                    player.currentAttack = player.attackRight;
                    player.Attack();
                }
            }
            if (key.IsKeyDown(Keys.C))
            {
                player.HealthPoint.currentValue = 100;
                player.Flag = false;
                player.DeathAnimationFlag = false;
            }
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }
    }
}
