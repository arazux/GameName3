using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3
{

    public class Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool isEqual(Position p)
        {
            return this.x == p.y && this.y == p.y;
        }
    }
}
