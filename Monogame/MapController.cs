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
        private static Texture2D decorSprite;
        public Point mapSize { get => new(currentLevel.mapWidth * cellSize, currentLevel.mapHeight * cellSize); }

        public MapController(ILevel level)
        {
            currentLevel = level;
            grassSprite = Globals.Content.Load<Texture2D>("grass");
            plainsSprite = Globals.Content.Load<Texture2D>("plainsEnlarged");
            decorSprite = Globals.Content.Load<Texture2D>("decorEnlarged");
        }

        public void UpdateCurrentLevel(ILevel newLevel, Player player)
        {
            currentLevel.objects.Remove(player);
            currentLevel = newLevel;
            currentLevel.objects.Add(player);
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
                        case 12:
                            DrawTiles(0, 192, i, j);
                            break;
                        case 13:
                            DrawTiles(64, 64, i, j);
                            break;
                        case 14:
                            DrawTiles(128, 64, i, j);
                            break;
                        case 15:
                            DrawTiles(192, 64, i, j);
                            break;
                        case 16:
                            DrawTiles(64, 128, i, j);
                            break;
                        case 17:
                            DrawTiles(128, 128, i, j);
                            break;
                        case 18:
                            DrawTiles(192, 128, i, j);
                            break;
                        case 19:
                            DrawTiles(256, 0, i, j);
                            break;
                        case 20:
                            DrawTiles(320, 0, i, j);
                            break;
                        case 21:
                            DrawTiles(256, 64, i, j);
                            break;
                        case 22:
                            DrawTiles(320, 64, i, j);
                            break;
                        case 23:
                            DrawTiles(256, 128, i, j);
                            break;
                        case 24:
                            DrawTiles(320, 128, i, j);
                            break;
                        case 25:
                            DrawDecor(0, 0, i, j);
                            break;
                        case 26:
                            DrawDecor(64, 0, i, j);
                            break;
                        case 27:
                            DrawDecor(0, 0, i, j);
                            break;
                        case 28:
                            DrawDecor(192, 0, i, j);
                            break;
                        case 29:
                            DrawTiles(128, 64, i, j);
                            DrawDecor(0, 256, i, j);
                            break;
                        case 30:
                            DrawTiles(128, 64, i, j);
                            DrawDecor(64, 256, i, j);
                            break;
                        case 31:
                            DrawTiles(128, 64, i, j);
                            DrawDecor(128, 256, i, j);
                            break;
                        case 32:
                            DrawTiles(128, 64, i, j);
                            DrawDecor(192, 256, i, j);
                            break;
                        case 33:
                            DrawGrass(i, j);
                            DrawTiles(64, 384, i, j);
                            break;
                        case 34:
                            DrawGrass(i, j);
                            DrawTiles(64, 256, i, j);
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

        private void DrawDecor(int srcPosX, int srcPosY, int posI, int posJ)
        {
            Globals.SpriteBatch.Draw(decorSprite, new Vector2(posI * cellSize, posJ * cellSize),
                new Rectangle(srcPosX + 1, srcPosY + 1, 64, 64),
                Color.White, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 1);
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
