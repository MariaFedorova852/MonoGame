using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.ProgressBar
{
    public class ProgressBar
    {
        public Texture2D background;
        public Texture2D foreground;
        public Vector2 position;
        public float maxValue;
        public float currentValue;
        public Rectangle part;

        public ProgressBar(Texture2D bg, Texture2D fg, float max, Vector2 pos)
        {
            background = Globals.Content.Load<Texture2D>("background");
            foreground = Globals.Content.Load<Texture2D>("foreground");
            maxValue = max;
            currentValue = max;
            position = pos;
            part = new(0, 0, foreground.Width, foreground.Height);
        }

        public virtual void Update()
        {
            part.Height = (int)(currentValue / maxValue * foreground.Height);
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(background, position, Color.White);
            Globals.SpriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
