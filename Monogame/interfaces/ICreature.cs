namespace Monogame.interfaces
{
    public interface ICreature
    {
        int IdleFrames { get; }
        int RunFrames { get; }
        int AttackFrames { get; }
        int DeathFrames { get; }
        int Size { get; }
        int SpriteSize { get; }
        int Speed { get; }
    }
}
