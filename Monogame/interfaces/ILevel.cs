using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Monogame.interfaces
{
    public interface ILevel
    {
        Vector2 EnterPosition { get; }
        Vector2 ExitPosition { get; }
        RectangleF Enter { get; }
        RectangleF Exit { get; }
        int[,] Map { get; }
        int MapWidth { get; }
        int MapHeight { get; }
        List<IObject> Objects { get; }
        List<IEntity> Entities { get; set; }
        IEnumerable<IObject> EntitiesAndObjects { get; }
    }
}
