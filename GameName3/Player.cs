using System;
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
        public int cameraX;
        public int cameraY;
        public int level;
        public int health;
        public int damage;
        public NPC target;
        public bool canAttack;
        public float attackTimer;
        public int gold;

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
            level = 0;
            health = 10;
            damage = 1;
            target = null;
            canAttack = true;
            gold = 0;
        }

        public bool attack()
        {
            if( target != null && canAttack )
            {
                target.health -= damage;
                canAttack = false;
                attackTimer = 1000;
                return true;
            }
            return false;
        }

        public void levelUp()
        {
            level++;
            health += 5;
            damage += 2;
            walkDelay--;
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
                if (y < m.yTiles - 1 )
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
                if (x < m.xTiles - 1 )
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
                sb.Draw(tex, new Vector2((x * 64 + 64 * getWalkTimer() / 1000) - cameraX, y * (64) - cameraY));
            }
            else if (getIsMoving() && moveRight)
            {
                sb.Draw(tex, new Vector2((x * 64 - 64 * getWalkTimer() / 1000) - cameraX, y * (64) - cameraY));
            }
            else if (getIsMoving() && moveDown)
            {
                sb.Draw(tex, new Vector2((x * 64) - cameraX, (y * 64 - 64 * getWalkTimer() / 1000) - cameraY));
            }
            else if (getIsMoving() && moveUp)
            {
                sb.Draw(tex, new Vector2((x * 64) - cameraX, (y * 64 + 64 * getWalkTimer() / 1000) - cameraY));
            }
            else
            {
                sb.Draw(tex, new Vector2(x * 64 - cameraX, y * 64 - cameraY));
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

            attackTimer -= gameTime.ElapsedGameTime.Milliseconds;
            if( attackTimer <= 0 )
            {
                attackTimer = 0;
                canAttack = true;
            }


        }

    }


}
