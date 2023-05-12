using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.ProgressBar;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.objects
{
    public class Rock : IObject
    {
        public RectangleF hitBox { get => new(pos.X, pos.Y + 25, 64, 30); }

        public int delta => 64;

        public Texture2D texture { get; set; }

        public Vector2 pos { get; set; }

        public Texture2D spriteSheet { get; set; }
        public HealthBar healthPoint { get; set; }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(spriteSheet, pos,
                new Rectangle(new Point(0, 63), new Size(63, 63)), Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 1);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 64, delta), Color.Red);
            //Globals.SpriteBatch.DrawRectangle(hitBox, Color.Black);
        }
        public Rock(Vector2 position)
        {
            pos = position;
            spriteSheet = Globals.Content.Load<Texture2D>("objects_64x64");
        }
    }
}
