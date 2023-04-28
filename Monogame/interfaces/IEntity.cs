using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.interfaces
{
    public interface IEntity
    {
        int healthPoint { get; set; }
        bool isAlive { get; set; }
        float speed { get; set; }

        bool isMovingLeft { get; }
        bool isMovingRight { get; }
        bool isMovingUp { get; }
        bool isMovingDown { get; }
        bool isAttack { get; }
        bool deathAnimationFlag { get; }

        int currentLimit { get; set; }
        int idleFrames { get; set; }
        int runFrames { get; set; }
        int attackFrames { get; set; }
        int deathFrames { get; set; }

        int spriteSize { get; set; }
        int size { get; set; }

        Texture2D spriteSheet { get; set; }
        RectangleF hitBox { get; }
        SpriteEffects flip { get; set; }


        void Update();

        bool IsMoving();

        void Draw();

        void SetRunAnimation();

        void SetAnimationAfterAttack();

        void SetAnimation(int currentAnimation);

        void StopEntity();

        void Attack();
    }
}
