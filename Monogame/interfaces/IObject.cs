using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.ProgressBar;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.interfaces
{
    public interface IObject
    {
        Vector2 pos { get; set; }
        RectangleF hitBox { get; }
        Texture2D spriteSheet { get; }
        int delta { get; }

        void Draw();
    }
}
