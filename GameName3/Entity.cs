using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

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

        public void Move(GameMap m)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (y > 0)
                {
                    if (m.map[x][y - 1].walkable)
                        this.y--;
                }
                

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (y < 9)
                {
                    if (m.map[x][y + 1].walkable)
                        this.y++;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (x < 19)
                {
                    if (m.map[x + 1][y].walkable)
                        this.x++;
                }
 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (x > 0)
                {
                    if (m.map[x - 1][y].walkable)
                        this.x--;
                }
            }
        }

        public void Update(GameMap m)
    {
        if (Keyboard.GetState().GetPressedKeys().Length > 0)
        {
            Move(m);


        }
    }
    }
}
