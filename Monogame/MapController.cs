using Microsoft.Xna.Framework.Graphics;
using Monogame.interfaces;
using Monogame.Models;
using System;
using System.Collections.Generic;
using Global;
using Microsoft.Xna.Framework;
using System.Collections;
using Monogame.Levels;
using Monogame.objects;
using MonoGame.Extended;
using static Monogame.GameManager;

namespace Monogame
{
    public class MapController
    {
        public static int cellSize = 64;
        public static int spriteSize = 64;
        private static Texture2D grassSprite;
        private static Texture2D plainsSprite;
        private static Texture2D decorSprite;
        public LevelNode currentLevel;
        public LinckedLevels levels;

        public Point mapSize { get => new(currentLevel.Level.MapWidth * cellSize, currentLevel.Level.MapHeight * cellSize); }

        public MapController()
        {
            grassSprite = Globals.Content.Load<Texture2D>("grass");
            plainsSprite = Globals.Content.Load<Texture2D>("plainsEnlarged");
            decorSprite = Globals.Content.Load<Texture2D>("decorEnlarged");
            var patterns = new LevelPatterns();
            levels = new LinckedLevels
            {
                new LevelStart(),
                new Level1(patterns.patternEntities1, patterns.patternObjects1),
                new Level1(patterns.patternEntities2, patterns.patternObjects2),
            };
            currentLevel = levels.Head;
        }
        public void ChangeCurrentLevel(bool direction)
        {
            if (direction)
            {
                currentLevel = currentLevel.NextLevel;
                player.Pos = currentLevel.Level.EnterPosition;
            }
            else
            {
                currentLevel = currentLevel.PreviousLevel;
                player.Pos = currentLevel.Level.ExitPosition;
            }
            currentLevel.Level.Entities.ForEach(e => e.SetBounds(map.mapSize, new Point(cellSize, cellSize)));
            player.SetBounds(map.mapSize, new Point(cellSize, cellSize));
        }

        public void UpdateCurrentLevel(ILevel newLevel, Player player)
        {
            currentLevel.Level.Objects.Remove(player);
            currentLevel.Level = newLevel;
            currentLevel.Level.Objects.Add(player);
        }

        public void Draw()
        {
            for (int i = 0; i < currentLevel.Level.MapWidth; i++)
            {
                for (int j = 0; j < currentLevel.Level.MapHeight; j++)
                {
                    var e = currentLevel.Level.Map[j, i];
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
                        case 35:
                            DrawGrass(i, j);
                            DrawTiles(192, 384, i, j);
                            break;
                        case 36:
                            DrawGrass(i, j);
                            DrawTiles(192, 256, i, j);
                            break;
                    }
                }
            }
            //Globals.SpriteBatch.DrawRectangle(currentLevel.Level.Exit, Color.Black);
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
    }
    public class LevelNode
    {
        public ILevel Level { get; set; }
        public LevelNode PreviousLevel { get; set; }
        public LevelNode NextLevel { get; set; }

        public LevelNode(ILevel level)
        {
            Level = level;
        }
    }

    public class LinckedLevels : IEnumerable<LevelNode>
    {
        public LevelNode Head { get; set; }
        public LevelNode Tail { get; set; }

        public void Add(ILevel level)
        {
            LevelNode node = new(level);

            if (Head == null)
                Head = node;
            else
            {
                Tail.NextLevel = node;
                node.PreviousLevel = Tail;
            }
            Tail = node;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();

        IEnumerator<LevelNode> IEnumerable<LevelNode>.GetEnumerator()
        {
            LevelNode current = Head;
            while (current != null)
            {
                yield return current;
                current = current.NextLevel;
            }
        }
    }

    class LevelPatterns
    {
        private static readonly SlimeModel model = new SlimeModel(); 
        private static readonly Texture2D texture = Globals.Content.Load<Texture2D>("slimeEnlarged");
        
        public List<IEntity> patternEntities1 = new List<IEntity>
        {
            new Slime(new Vector2(128, 32), model, texture),
            new Slime(new Vector2(128, 532), model, texture),
            new Slime(new Vector2(800, 532), model, texture),
            new Slime(new Vector2(800, 32), model, texture),
        };
        public List<IObject> patternObjects1 = new List<IObject>
        {
            new Rock(new Vector2(64, 256)),
            new Rock(new Vector2(128, 256)),
            new Rock(new Vector2(192, 256)),
            new Rock(new Vector2(256, 256)),
            new Rock(new Vector2(320, 256)),
            new Rock(new Vector2(320, 210)),
            new Rock(new Vector2(320, 110)),
            new Rock(new Vector2(320, 64)),

            new Rock(new Vector2(64, 384)),
            new Rock(new Vector2(128, 384)),
            new Rock(new Vector2(192, 384)),
            new Rock(new Vector2(256, 384)),
            new Rock(new Vector2(320, 384)),
            new Rock(new Vector2(320, 430)),
            new Rock(new Vector2(320, 558)),
            new Rock(new Vector2(320, 606)),

            new Rock(new Vector2(576, 384)),
            new Rock(new Vector2(640, 384)),
            new Rock(new Vector2(704, 384)),
            new Rock(new Vector2(768, 384)),
            new Rock(new Vector2(832, 384)),
            new Rock(new Vector2(576, 430)),
            new Rock(new Vector2(576, 558)),
            new Rock(new Vector2(576, 606)),

            new Rock(new Vector2(576, 256)),
            new Rock(new Vector2(640, 256)),
            new Rock(new Vector2(704, 256)),
            new Rock(new Vector2(768, 256)),
            new Rock(new Vector2(832, 256)),
            new Rock(new Vector2(576, 210)),
            new Rock(new Vector2(576, 110)),
            new Rock(new Vector2(576, 64)),

        };
        public List<IObject> patternObjects2 = new List<IObject>
        {
            new Rock(new Vector2(200, 360)),
            new Rock(new Vector2(720, 440)),
            new Rock(new Vector2(300, 32)),
            new Tree(new Vector2(600, 0)),
            new Rock(new Vector2(832, 256)),
            new Rock(new Vector2(832, 304)),
            new Rock(new Vector2(832, 352)),
            new Rock(new Vector2(832, 400))
        };
        public List<IEntity> patternEntities2 = new List<IEntity>
        {
            new Slime(new Vector2(800, 532), model, texture),
            new Slime(new Vector2(200, 453), model, texture),
            new Slime(new Vector2(500, 300), model, texture),
        };
    }
}
