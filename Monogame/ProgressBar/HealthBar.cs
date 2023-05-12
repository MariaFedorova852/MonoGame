using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.ProgressBar
{
    public class HealthBar : ProgressBar
    {
        private Texture2D heart;
        public HealthBar(Texture2D bg, Texture2D fg, float max, Vector2 pos) : base(bg, fg, max, pos) 
        {
            heart = Globals.Content.Load<Texture2D>("heart");
        }

        public override void Update()
        {
            part.Height = (int)(currentValue / maxValue * foreground.Height);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(background, position, Color.White);
            Globals.SpriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            Globals.SpriteBatch.Draw(heart, new Vector2(position.X - 8, position.Y - 25), Color.White);
        }
    }
}
