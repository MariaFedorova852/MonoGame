using Microsoft.Xna.Framework.Graphics;
using Monogame.ProgressBar;
using Microsoft.Xna.Framework;

namespace Monogame.interfaces
{
    public interface IEntity : IObject
    {
        HealthBar HealthPoint { get; set; }
        bool IsAlive { get; }
        float Speed { get; set; }

        bool IsMovingLeft { get; }
        bool IsMovingRight { get; }
        bool IsMovingUp { get; }
        bool IsMovingDown { get; }
        bool IsAttack { get; }
        bool DeathAnimationFlag { get; }

        int CurrentLimit { get; set; }
        int IdleFrames { get; set; }
        int RunFrames { get; set; }
        int AttackFrames { get; set; }
        int DeathFrames { get; set; }

        int SpriteSize { get; set; }
        int Size { get; set; }

        SpriteEffects Flip { get; set; }

        Vector2 Direction { get; set; }

        void Update();

        bool IsMoving();

        void SetRunAnimation();

        void SetAnimationAfterAttack();

        void SetAnimation(int currentAnimation);

        void StopEntity();

        void Attack();
        void SetBounds(Point mapSize, Point tileSize);
    }
}
