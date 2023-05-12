using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;


namespace Monogame.interfaces
{
    public interface IObject
    {
        Vector2 Pos { get; set; }
        RectangleF HitBox { get; }
        Texture2D SpriteSheet { get; }
        int Delta { get; }

        void Draw();
    }
}
