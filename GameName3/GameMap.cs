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
        public static int tileSize = 64;
        public static int xTiles = 72;
        public static int yTiles = 14;
        public List<Texture2D> tileSprites;
        public int mapX;
        public int mapY;
        public String filePath;
        public FileStream fs;

        public GameMap(List<Texture2D> tileSprites)
        {
            filePath = "Content/map2.txt";
            this.tileSprites = tileSprites;
            fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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

        //Save Method. Every ROW has to be the same WIDTH or INDEXOUTOFBOUNDS
        //BUG: Saves tiles to the file, but does not seem to be updated in file?
        public void saveFile()
        {
            String[] newFile = new String[map[0].Length];
            for (int i = 0; i < map[0].Length; i++)
            {
                String s = String.Empty;
                for (int x = 0; x < map.Length; x++)
                {
                    s += Convert.ToString(map[x][i].getType());
                }
                newFile[i] = s;
            }

            try
            {
                //using (FileStream fs = new FileStream("Content/map3.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                fs = new FileStream("Content/map4.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                {
                    //Console.WriteLine("Save File: " + filePath);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.AutoFlush = true;
                    for (int i = 0; i < newFile.Length; i++)
                    {
                        sw.WriteLine(newFile[i]);
                    }
                }

                /*PROOF THAT FILE HAS BEEN OVERWRITTEN CORRECTLY. BUT REVERTS BACK AFTER APP EXIT.
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs);
                    fs.Position = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                */

            }
            catch (IOException e)
            {
                Console.WriteLine("Could not save file: ");
                Console.WriteLine(e.Message);
            }
        }

        public void Draw(SpriteBatch sb, Player p)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[0].Length; col++)
                { 

                    p.setCamera();

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
