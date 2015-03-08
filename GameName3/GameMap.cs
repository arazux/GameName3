using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace GameName3
{
    enum TileTitle { Grass, Water, Fire, Wall }
    public class GameMap
    {
        public Tile[][] map;
        public static int tileWidth = 64;
        public static int tileHeight = 64;
        public Texture2D[] tileSprites;
        public int xTiles;
        public int yTiles;

        public GameMap(int x, int y, Texture2D[] tileSprites)
        {
            this.tileSprites = tileSprites;
            try
            {

                using (StreamReader sr = new StreamReader("Content/map1.txt"))
                {
                    int lineCount = File.ReadLines("Content/map1.txt").Count();
                    map = new Tile[lineCount][];
                    String line;
                    int rows = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        map[rows] = new Tile[line.Count()];
                        for (int i = 0; i < line.Count(); i++)
                        {
                            map[rows][i] = new Tile(Convert.ToInt16(Convert.ToString(line[i])), rows, i);
                        }

                        rows++;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            xTiles = x;
            yTiles = y;
        }

        public GameMap(Texture2D[] tileSprites)
        {
            this.tileSprites = tileSprites;

        }


        public void Draw(SpriteBatch sb, Player p)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[0].Length; col++)
                { 
                    p.cameraX = (p.x * 64) - 640;
                    p.cameraY = (p.y * 64) - 320;
                    if (p.cameraX < 0)
                        p.cameraX = 0;

                    if (p.cameraX > 5120)
                        p.cameraX = 5120;

                    if (p.cameraY < 0)
                        p.cameraY = 0;

                    if (p.cameraY > 2560)
                        p.cameraY = 2560;
                    sb.Draw(tileSprites[map[row][col].getType()], new Vector2((map[row][col].x * 64) - p.cameraX, (map[row][col].y * 64) - p.cameraY));

                  }
            }
        }

    }
}
