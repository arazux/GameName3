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

        public EasyDraw(SpriteFont f, SpriteBatch s)
        {
            font = f;
            sba = s;
        }

        public void drawString(string s, int var, int x, int y)
        {
            sba.DrawString(font, s + var.ToString(), new Vector2(x, y), Color.Black);
        }

        public void drawString(string s, int x, int y)
        {
            sba.DrawString(font, s, new Vector2(x, y), Color.Black);
        }

        public void drawString(string s, int x, int y, Color c)
        {
            sba.DrawString(font, s, new Vector2(x, y), c);
        }
    }
}
