using Monogame.interfaces;
using Monogame.Models;
using System;
using System.Collections.Generic;
using Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using Monogame.objects;
using System.Linq;

namespace Monogame.Levels
{
    public class LevelStart : ILevel
    {
        public Slime slime1;
        public Slime slime2;
        public Slime slime3;

        public LevelStart()
        {
            var texture = Globals.Content.Load<Texture2D>("slimeEnlarged");
            var model = new SlimeModel();
            slime1 = new Slime(new Vector2(945, 290), model, texture);
            slime2 = new Slime(new Vector2(1115, 580), model, texture);
            slime3 = new Slime(new Vector2(1470, 265), model, texture);
        }

        public int[,] map => new int[height, width]
            {
                {5, 8, 8,  8,    8,    8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 6},
                {9, 0, 25,   0,    0,    0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 25, 0, 0, 0, 0, 25, 0, 0, 0, 11},
                {9, 0, 0,   0,    26,   0, 0, 0, 0, 0, 0, 0, 0, 0, 26,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    1,    2, 3, 27, 0,0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    13,   29, 21, 3, 0, 0, 0, 0, 25, 0, 0, 25, 0, 0, 0, 26,0, 0, 0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   25,   16,   17, 20, 21, 3, 27,0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    28, 16, 20, 21, 3,0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 26, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 13, 14, 15, 0,0, 0, 27, 0, 0, 0, 0, 0, 0, 0, 27, 0, 25, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 13, 14, 15, 0,0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 26, 0, 0, 0, 11},
                {9, 25, 0,   0,    0,    0, 0, 13, 31, 21, 3, 26,0, 0, 0, 0, 26, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 16, 20, 14, 15,      0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 0, 0, 0, 11},
                {9, 26, 0,   27,    0,    0, 0, 25,  13, 14, 15,     0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 0,  13, 14, 15, 0,   0,  0,  0,  0,  0,  0,  27,  0,  0,  0,  0,  0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 0,  13, 30, 15, 26,  0,  0,  27,  0,  0,  0,  0,  0,  0,  0,  0,  0, 27, 0, 0, 0, 26, 33},
                {9, 0, 25,   0,    0,    0, 0, 0,  13, 14, 15,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  26,  1, 2, 2, 2, 2, 2, 2},
                {9, 0, 0,   0,    0,    0, 0, 0,  16, 20,  21, 2,  3,  0,  0,  0,  26,  0,  0,  28,  1,  2,  2,  22, 32, 14, 14, 14, 31, 14},
                {9, 0, 0,   0,    0,    0, 0, 0,  25,  16,  20, 32, 21, 2,  2,  2,  2,  2,  2,  2,  22, 31, 14, 14, 19, 17, 17, 17, 17, 17},
                {9, 26, 0,   0,    0,    0, 0, 26,  0,  0,   16, 17, 20, 14, 14, 32, 14, 14, 14, 32, 14, 19, 17, 17, 18, 0, 0, 25, 0, 34},
                {9, 0, 0,   0,    0,    0, 0, 0,  0,  0,   0,  0,  16, 17, 17, 17, 17, 17, 17, 17, 17, 18, 26,  0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 0,  0,  0, 0, 0, 0, 0, 0, 27, 0, 0, 0, 0, 28, 0, 0, 27, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    25,    0, 25, 0,  0,  0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 27, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 0,  0,  0, 27, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 11},
                {9, 0, 0,   0,    0,    0, 0, 0,  0,  0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 27, 0, 0, 0, 0, 0, 0, 11},
                {4, 10, 10, 10,   10,  10, 10, 10, 10, 10, 10,10, 10, 10, 10, 10, 10, 10, 10, 10,10, 10, 10, 10, 10, 10, 10, 10, 10, 7}
            };

        const int height = 24;
        const int width = 30;

        public int mapWidth => width;
        public int mapHeight => height;
        public List<IObject> objects => new()
        {
            new Tree(new Vector2(800, 700)),
                new Tree(new Vector2(1600, 500)),
                new Tree(new Vector2(1200, 1200)),
                new Tree(new Vector2(650, 150)),
                new Tree(new Vector2(400, 900)),
                //new Bush(new Point(200, 450)),
                new Rock(new Vector2(550, 120)),
                new Rock(new Vector2(295, 1200)),
                new Rock(new Vector2(380, 800)),
                new Rock(new Vector2(1675, 1195)),
                new Rock(new Vector2(1465, 500)),
        };
        public List<IEntity> enemys => new() 
        {  
            slime1, slime2, slime3
        };
        public IEnumerable<IObject> enemysAndObjects => new List<IObject>(objects).Concat(enemys);
    }
}
