using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3
{

    public class Tile
    {
        #region Neighbour Coordinates
        public Position North { get { return new Position(x, y - 1); } }
        public Position South { get { return new Position(x, y + 1); } }
        public Position West { get { return new Position(x - 1, y); } }
        public Position East { get { return new Position(x + 1, y); } }
        public Position NorthWest { get { return new Position(x - 1, y - 1); } }
        public Position NorthEast { get { return new Position(x + 1, y - 1); } }
        public Position SouthWest { get { return new Position(x - 1, y + 1); } }
        public Position SouthEast { get { return new Position(x + 1, y + 1); } }
        #endregion

        public int x;
        public int y;
        int type;
        // Is the tile type itself walkable?
        public bool walkable;
        // Is the tile currently occupied?
        public bool occupied;

        public Tile()
        {
            type = 0;
        }

        public Tile(int t, int x, int y)
        {
            this.x = x;
            this.y = y;
            walkable = true;

            setType(t);


        }

        public int getType()
        {
            return type;
        }

        public void setType(int t)
        {
            this.type = t;

            if (t == 3)
                walkable = false;
            else
                walkable = true;
        }

    }
}
