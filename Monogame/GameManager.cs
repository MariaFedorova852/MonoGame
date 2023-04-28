using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Models;
using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Monogame.Levels;

namespace Monogame
{
    public class GameManager
    {
        public static Player player;
        public static MapController map;
        private Matrix _translation;

        public GameManager()
        {
            player = new Player(new Vector2(0, 0), new PlayerModel(), Globals.Content.Load<Texture2D>("playerEnlarged++"));
            map = new MapController(new LevelStart());
            player.SetBounds(map.mapSize, new Point(MapController.cellSize, MapController.cellSize));
        }

        private void CalculateTranslation()
        {
            var dx = (Globals.WindowSize.X / 2) - player.position.X - player.size / 2;
            dx = MathHelper.Clamp(dx, -map.mapSize.X + Globals.WindowSize.X, 0);
            var dy = (Globals.WindowSize.Y / 2) - player.position.Y - player.size / 2;
            dy = MathHelper.Clamp(dy, -map.mapSize.Y + Globals.WindowSize.Y, 0);
            _translation = Matrix.CreateTranslation(dx, dy, 0f);
        }

        public void Update()
        {
            PlayerController.Update();
            map.currentLevel.entities.ForEach(e => e.Update());
            player.Update();
            CalculateTranslation();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(transformMatrix: _translation);
            map.Draw();
            map.currentLevel.entities.ForEach(e => e.Draw());
            player.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
