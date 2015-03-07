using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameName3
{
    public class GameMap
    {
        public Tile[][] map;
        public static int tileWidth = 64;
        public static int tileHeight = 64;
        public Texture2D[] tileSprites;



        public GameMap(int x, int y, Texture2D[] tileSprites)
        {
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
                    map[row][col] = new Tile(1, row, col);
                    col++;
                    index++;
                }
                row++;
            }

            map[1][1].setType(4);
            map[1][2].setType(4);
            map[1][3].setType(4);
            map[1][4].setType(4);
            map[1][5].setType(4);


             

        }



        public void draw(SpriteBatch sb)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[0].Length; col++)
                    switch (map[row][col].getType())
                    {
                        case 1:
                            sb.Draw(tileSprites[0], new Vector2(map[row][col].x * 64, map[row][col].y * 64));
                            break;
                        case 2:
                            sb.Draw(tileSprites[1], new Vector2(map[row][col].x * 64, map[row][col].y * 64));
                            break;
                        case 3:
                            sb.Draw(tileSprites[2], new Vector2(map[row][col].x * 64, map[row][col].y * 64));
                            break;
                        case 4:
                            sb.Draw(tileSprites[3], new Vector2(map[row][col].x * 64, map[row][col].y * 64));
                            break;
                    }

            }
        }

    }
}
