using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.ProgressBar;
using MonoGame.Extended;
using System.Linq;

namespace Monogame.objects
{
    public class Tree : IObject
    {

        public RectangleF hitBox { get => new(pos.X + 50, pos.Y + 200, 110, 50); }

        public int delta => 254;

        public Texture2D texture { get; set; }

        public Vector2 pos { get; set; }

        public Texture2D spriteSheet { get; set; }
        public HealthBar healthPoint { get; set; }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(spriteSheet, pos, 
                new Rectangle(new Point(0, 321), new Size(190, 254)), Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 1);
            //Globals.SpriteBatch.DrawRectangle(new RectangleF(pos.X, pos.Y, 190, delta), Color.Black);
            //Globals.SpriteBatch.DrawRectangle(hitBox, Color.Black);
        }
        public Tree(Vector2 position)
        {
            pos = position;
            spriteSheet = Globals.Content.Load<Texture2D>("objects_64x64");
        }
    }
}
