using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameName3
{
    public class Entity
    {

        public int x;
        public int y;
        public int spriteType;

        public Entity(int x, int y, int t)
        {
            this.y = y;
            this.x = x;
            spriteType = t;
        }

        public Entity()
        {

        }
    }
}
