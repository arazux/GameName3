using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3
{

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

        public Tile(int t, int x, int y)
        {
            type = t;
            this.x = x;
            this.y = y;

            if (t == 3){
                walkable = false;
            }

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

            if (t == 3)
                walkable = false;
            else
                walkable = true;
        }

    }
}
