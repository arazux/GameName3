using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameName3
{
    public class EasyDraw
    {
        SpriteFont font;
        SpriteBatch sba;
        public void setFont(SpriteFont f, SpriteBatch s)
        {
            font = f;
            sba = s;
        }

        public void drawString(string s, int var, int x, int y)
        {
            sba.DrawString(font, s + var.ToString(), new Vector2(x, y), Color.Black);
        }
    }
}
