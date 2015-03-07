using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
            xTiles = x;
            yTiles = y;
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



             

        }



        public void Draw(SpriteBatch sb, Player p)
        {
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[0].Length; col++)
                { 
                    p.cameraX = (p.x * 64) - 640;
                    p.cameraY = (p.y * 64) - 320;
                    if (p.cameraX <= 640)
                        p.cameraX = 0;
                    if (p.cameraY <= 320)
                        p.cameraY = 0;

                    sb.Draw(tileSprites[map[row][col].getType()], new Vector2((map[row][col].x * 64) - p.cameraX, (map[row][col].y * 64) - p.cameraY));
                  }
            }
        }

    }
}
