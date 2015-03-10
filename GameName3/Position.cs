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

        public static bool operator !=(Position p1, Position p2)
        {
            if (System.Object.ReferenceEquals(p1, p2))
                return true;

            if (p1 == null || p2 == null)
                return false;

            return p1.x != p2.x && p1.y != p2.y;
        }

        public static bool operator == (Position p1, Position p2)
        {
            if (System.Object.ReferenceEquals(p1, p2))
                return true;

            if (p1 == null || p2 == null)
                return false;

            return p1.x == p2.x && p1.y == p2.y;
        }

        public static Position operator +(Position p1, Position p2)
        {
            if (p1 == null || p2 == null)
                return null;

            return new Position(p1.x + p2.x, p1.y + p2.y);
        }

        public static Position operator -(Position p1, Position p2)
        {
            if (p1 == null || p2 == null)
                return null;

            return new Position(p1.x - p2.x, p1.y - p2.y);
        }
    }
}
