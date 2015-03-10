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
    public class GameMap
    {
        public Tile[][] map;
        public static int tileWidth = 64;
        public static int tileHeight = 64;
        public List<Texture2D> tileSprites;
        public int mapX;
        public int mapY;
        public String filePath;

        public GameMap(int x, int y, List<Texture2D> tileSprites)
        {
            filePath = "Content/map2.txt";
            this.tileSprites = tileSprites;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                int yVal = 0;
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (sr.ReadLine() != null)
                    {
                        yVal++;
                    }
                    fs.Position = 0;

                    int xCols = 0;

                    String line;
                    int yRow = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        xCols = line.Count();
                        if (yRow == 0)
                        {
                            map = new Tile[xCols][];
                        }

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
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }

        public GameMap(List<Texture2D> tileSprites)
        {
            this.tileSprites = tileSprites;

        }


        public void Draw(SpriteBatch sb, Player p)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[0].Length; col++)
                { 
                    p.cameraX = (p.pos.x * 64) - 640;
                    p.cameraY = (p.pos.y * 64) - 320;
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

        #region Utilities
        // Index operator to make for easier access of tiles.
        public Tile this[Position pos]
        {
            get
            {
                return map[pos.x][pos.y];
            }
            private set
            {
                map[pos.x][pos.y] = value;
            }
        }
        #endregion
    }
}
