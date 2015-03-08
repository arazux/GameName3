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
        public int mapX;
        public int mapY;

        public GameMap(int x, int y, Texture2D[] tileSprites)
        {
            this.tileSprites = tileSprites;
            try
            {
                StreamReader tempSR = new StreamReader("Content/map2.txt");
                map = new Tile[tempSR.ReadLine().Count()][];

                using (StreamReader sr = new StreamReader("Content/map2.txt"))
                {
                    int xCols = 0;
                    int yVal = File.ReadLines("Content/map2.txt").Count();

                    String line;
                    int yRow = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(yRow + ": " + line);
                        xCols = line.Count();
                        for (int i = 0; i < xCols; i++)
                        {
                            if (yRow == 0)
                            {
                                map[i] = new Tile[yVal];
                            }
                            map[i][yRow] = new Tile(Convert.ToInt16(Convert.ToString(line[i])), i, yRow);
                        }

                        yRow++;
                        mapY = yRow;
                    }
                    mapX = xCols;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public GameMap(Texture2D[] tileSprites)
        {
            this.tileSprites = tileSprites;

        }


        public void Draw(SpriteBatch sb, Player p)
        {
            for (int row = 0; row < mapY; row++)
            {
                for (int col = 0; col < mapX; col++)
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
