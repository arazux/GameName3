using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameName3
{
    public class EasyLoad
    {
        ContentManager cm;
        public EasyLoad(ContentManager c)
        {
            cm = c;
        }

        public Texture2D LoadSprite(string s, int index)
        {
           if (index == 0)
           {
               Console.WriteLine(s);
               return (cm.Load<Texture2D>("TileSprites/" + s));
           }
           else if (index == 1)
           {
               return (cm.Load<Texture2D>("Animations/" + s));
           }
           return null;
        }
    }
}
