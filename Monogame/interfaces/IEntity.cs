using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;
using Monogame.ProgressBar;
using Microsoft.Xna.Framework;

namespace Monogame.interfaces
{
    public interface IEntity : IObject
    {
        HealthBar healthPoint { get; set; }
        bool isAlive { get; }
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

        SpriteEffects flip { get; set; }

        Vector2 direction { get; set; }

        void Update();

        bool IsMoving();

        void SetRunAnimation();

        void SetAnimationAfterAttack();

        void SetAnimation(int currentAnimation);

        void StopEntity();

        void Attack();
    }
}
