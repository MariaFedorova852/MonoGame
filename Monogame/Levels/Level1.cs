using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.Models;
using Monogame.objects;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Levels
{
    public class Level1 : ILevel
    {
        private List<IEntity> entities;
        private List<IObject> objects;
        const int height = 11;
        const int width = 15;
        public int MapWidth => width;
        public int MapHeight => height;

        public Level1(IEnumerable<IEntity> entities, IEnumerable<IObject> objects)
        {
            var texture = Globals.Content.Load<Texture2D>("slimeEnlarged");
            var model = new SlimeModel();

            this.entities = entities.ToList();
            this.objects = objects.ToList();
        }

        public int[,] Map => new int[height, width]
            {
                {5,8,8,8,8,8,8,8,8,8,8,8,8,8,6},
            {9,0,0,0,0,0,25,0,0,0,0,0,0,26,11},
            {9,0,25,0,0,0,0,25,0,0,26,0,0,0,11},
            {9,0,0,0,0,0,0,0,0,0,0,26,0,0,11},
            {35,0,25,0,0,25,0,27,0,0,0,25,0,0,33},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {36,0,26,0,27,0,0,26,0,0,0,0,0,0,34},
            {9,0,0,0,25,0,0,0,0,25,0,0,0,0,11},
            {9,0,26,0,0,0,0,0,0,0,0,0,27,0,11},
            {9,0,0,0,27,0,0,0,25,0,0,0,0,0,11},
            {4,10,10,10,10,10,10,10,10,10,10,10,10,10,7},
            };

        public List<IObject> Objects { get => objects; set => objects = value; }
        public List<IEntity> Entities { get => entities; set => entities = value; }
        public IEnumerable<IObject> EntitiesAndObjects => new List<IObject>(Objects).Concat(Entities);
        public RectangleF Enter => new(0, height * MapController.cellSize / 2 - MapController.cellSize + 32,
            MapController.cellSize, 2 * MapController.cellSize - 64);
        public RectangleF Exit => new(width * MapController.cellSize - 64,
            height * MapController.cellSize / 2 - MapController.cellSize + 32, MapController.cellSize, 2 * MapController.cellSize - 64);

        public Vector2 EnterPosition => new(0, 200);

        public Vector2 ExitPosition => new(870, 200);
    }
}
