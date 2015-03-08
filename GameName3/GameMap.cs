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
                    List<int> data = new List<int>();
                    int rows = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        map[rows] = new Tile[line.Count()];
                        for (int i = 0; i < line.Count(); i++)
                        {
                            data.Add(Convert.ToInt16(Convert.ToString(line[i])));
                            map[rows][i] = new Tile(Convert.ToInt16(Convert.ToString(line[i])), rows, i);
                        }

                        rows++;
                    }
                    /*
                    for (int a = 0; a < map.Length; a++)
                    {
                        for (int b = 0; b < map[a].Length; b++)
                        {

                        }
                    }
                    */
                    //Console.WriteLine(data.ElementAt(101));
                    //String line = sr.ReadToEnd();
                    //Console.WriteLine(line);



                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            xTiles = x;
            yTiles = y;
            /*
          
            map = new Tile[x][];
            this.tileSprites = tileSprites;
            int row = 0;
            int index = 0;
            while (row < x)
            {
                int col = 0;
                while (col < y)
                {
                    if (col == 0)
                    {
                        map[row] = new Tile[y];
                    }
                    map[row][col] = new Tile(0, row, col);
                    col++;
                    index++;
                }
                row++;
            }

            map[1][1].setType(3);
            map[1][2].setType(3);
            map[1][3].setType(3);
            map[1][4].setType(3);
            map[1][5].setType(3);
            map[1][6].setType(3);

            map[2][2].setType(2);

            map[2][1].setType(3);

            map[3][1].setType(3);
            map[3][2].setType(3);
            map[3][3].setType(3);
            map[3][4].setType(3);
            map[3][5].setType(3);
            map[3][6].setType(3);
            */
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
