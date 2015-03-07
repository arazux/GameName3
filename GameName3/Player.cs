﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameName3
{
    public class Player : Entity
    {
        private bool canWalk;
        private float walkTimer;
        private float walkDelay;
        private bool isMoving;
        private bool moveUp, moveDown, moveLeft, moveRight;

        public Player(int x, int y, int t, Texture2D tex)
        {
            this.y = y;
            this.x = x;
            spriteType = t;
            this.tex = tex;
            canWalk = true;
            isMoving = false;
            walkDelay = 400;
            walkTimer = walkDelay;
        }

        public void Move(GameMap m)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (y > 0)
                {
                    if (m.map[x][y - 1].walkable)
                    {
                        moveUp = true;
                        this.y--;
                    }
                        
                   
                }


            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (y < 9)
                {
                    if (m.map[x][y + 1].walkable)
                    {
                        moveDown = true;
                        this.y++;
                    }
                        
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (x < 19)
                {
                    if (m.map[x + 1][y].walkable)
                    {
                        moveRight = true;
                        this.x++;
                    }
                        
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (x > 0)
                {
                    if (m.map[x - 1][y].walkable)
                    {
                        moveLeft = true;
                        this.x--;
                    }
                        
                }
            }
        }

        public float getWalkDelay()
        {
            return walkDelay;
        }

        public float getWalkTimer()
        {
            return walkTimer;
        }

        public bool getIsMoving()
        {
            return isMoving;
        }

        public void incMoveDelay(){
            if (walkDelay < 1000)
            {
                walkDelay += 50;
            }
        }

        public void decMoveDelay()
        {
            if (walkDelay > 50)
            {
                walkDelay -= 50;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (getIsMoving() && moveLeft)
            {
                sb.Draw(tex, new Vector2((x * 64 + 64 * getWalkTimer() / 1000), y * (64)));
            }
            else if (getIsMoving() && moveRight)
            {
                sb.Draw(tex, new Vector2((x * 64 - 64 * getWalkTimer() / 1000), y * (64)));
            }
            else if (getIsMoving() && moveDown)
            {
                sb.Draw(tex, new Vector2((x * 64), (y * 64 - 64 * getWalkTimer() / 1000)));
            }
            else if (getIsMoving() && moveUp)
            {
                sb.Draw(tex, new Vector2((x * 64), (y * 64 + 64 * getWalkTimer() / 1000)));
            }
            else
            {
                sb.Draw(tex, new Vector2(x * 64, y * 64));
            }
        }

        public void Update(GameMap m, GameTime gameTime)
        {
            walkTimer -= gameTime.ElapsedGameTime.Milliseconds;
            if (walkTimer < 0)
            {
                canWalk = true;
            }
            if (Keyboard.GetState().GetPressedKeys().Length > 0 && canWalk)
            {
                canWalk = false;
                walkTimer = walkDelay;
                Move(m);
            }
            if (walkTimer > 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
                moveUp = false; ;
                moveDown = false;
                moveLeft = false;
                moveRight = false;
            }

        }

    }


}
