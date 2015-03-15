using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameName3
{
    public class NPC : Entity
    {
        public int health;
        public int damage;
        public int ID;
        public NPC(int x, int y, int id, int health )
        {
            pos = new Position(x, y);
            damage = 2;
            this.ID = id;
            this.health = health;
        }

        public void Retaliate(Player p)
        {
            p.health -= damage;
        }
    }
}
