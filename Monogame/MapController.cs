using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.Models;
using System;
using System.Collections.Generic;
using Global;
using Microsoft.Xna.Framework;

namespace Monogame
{
    public class MapController
    {
        public ILevel currentLevel;
        public static int cellSize = 64;
        public static int spriteSize = 64;
        private static Texture2D grassSprite;
        private static Texture2D plainsSprite;
        public Point mapSize { get => new(currentLevel.mapWidth * cellSize, currentLevel.mapHeight * cellSize); }

        public MapController(ILevel level)
        {
            currentLevel = level;
            grassSprite = Globals.Content.Load<Texture2D>("grass");
            plainsSprite = Globals.Content.Load<Texture2D>("plainsEnlarged");
        }

        public void UpdateCurrentLevel(ILevel newLevel, Player player)
        {
            currentLevel.entities.Remove(player);
            currentLevel = newLevel;
            currentLevel.entities.Add(player);
        }

        public void Draw()
        {
            for (int i = 0; i < currentLevel.mapWidth; i++)
            {
                for (int j = 0; j < currentLevel.mapHeight; j++)
                {
                    var e = currentLevel.map[j, i];
                    switch (e)
                    {
                        case 0:
                            DrawGrass(i, j);
                            break;
                        case 1:
                        case 2:
                        case 3:
                            DrawTiles(spriteSize * e, 0, i, j);
                            break;
                        case 4:
                            DrawGrass(i, j);
                            DrawTiles(256, 320, i, j);
                            break;
                        case 5:
                            DrawGrass(i, j);
                            DrawTiles(256, 256, i, j);
                            break;
                        case 6:
                            DrawGrass(i, j);
                            DrawTiles(320, 256, i, j);
                            break;
                        case 7:
                            DrawGrass(i, j);
                            DrawTiles(320, 320, i, j);
                            break;
                        case 8:
                            DrawGrass(i, j);
                            DrawTiles(128, 384, i, j);
                            break;
                        case 9:
                            DrawGrass(i, j);
                            DrawTiles(192, 320, i, j);
                            break;
                        case 10:
                            DrawGrass(i, j);
                            DrawTiles(128, 256, i, j);
                            break;
                        case 11:
                            DrawGrass(i, j);
                            DrawTiles(64, 320, i, j);
                            break;
                    }
                }
            }
        }

        private void DrawTiles(int srcPosX, int srcPosY, int posI, int posJ)
        {
            Globals.SpriteBatch.Draw(plainsSprite, new Vector2(posI * cellSize, posJ * cellSize),
                new Rectangle(new Point(srcPosX, srcPosY),
                new Point(spriteSize, spriteSize)),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        private void DrawGrass(int posI, int posJ)
        {
            Globals.SpriteBatch.Draw(grassSprite,
                new Vector2(posI * cellSize, posJ * cellSize),
                new Rectangle(Point.Zero, new Point(spriteSize, spriteSize)),
                Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        public int GetWidth()
        {
            return cellSize * currentLevel.mapWidth + 15;
        }

        public int GetHeight()
        {
            return cellSize * currentLevel.mapHeight + 14;
        }
    }
}
