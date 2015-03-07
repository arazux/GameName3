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
                    map[row][col] = new Tile(4, row, col);
                    col++;
                    index++;
                }
                row++;
            }

             

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

public class Tile
{
    public int x;
    public int y;
    int type;
    public bool walkable;

    public Tile()
    {
        type = 1;
    }

    public Tile(int t)
    {

            walkable = true;

        type = t;
    }

    public Tile(int t, int x, int y)
    {
        type = t;
        this.x = x;
        this.y = y;

        if (t == 4)
            walkable = false;
        else
            walkable = true;

  
    }

    public int getType()
    {
        return type;
    }

    public void setType(int t)
    {
        type = t;
    }

}