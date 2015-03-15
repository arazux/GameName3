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

        private Texture2D[] playerAnimSprites;
        private Animation playerAnim;
        private EasyLoad load;

        public Inventory inventory;


        public Player(int x, int y, int t, EasyLoad load)
        {
            pos = new Position(x, y);
            //this.tex = tex;
            this.load = load;
            this.tex = load.LoadSprite("katt");

            playerAnimSprites = new Texture2D[4];
            playerAnimSprites[0] = load.LoadAnimation("smile1");
            playerAnimSprites[1] = load.LoadAnimation("smile2");
            playerAnimSprites[2] = load.LoadAnimation("smile3");
            playerAnimSprites[3] = load.LoadAnimation("smile4");

            playerAnim = new Animation(playerAnimSprites, 1/12f);
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
            inventory = new Inventory();
        }

        public void setCamera()
        {
            cameraX = (pos.x * GameMap.tileSize) - 448;
            cameraY = (pos.y * GameMap.tileSize) - 256;

            if (cameraX < 0)
                cameraX = 0;

            if (cameraX > (GameMap.tileSize * GameMap.xTiles) - GameMap.tileSize * 15)
                cameraX = (GameMap.tileSize * GameMap.xTiles) - GameMap.tileSize * 15;

            if (cameraY < 0)
                cameraY = 0;

            if (cameraY > (GameMap.tileSize * GameMap.yTiles) - GameMap.tileSize * 9 )
                cameraY = (GameMap.tileSize * GameMap.yTiles) - GameMap.tileSize * 9;

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

        public void drawInventory(Texture2D t, SpriteBatch sb)
        {
            for (int i = 0; i < inventory.size / 2; i++)
            {
                sb.Draw(t, new Vector2(960 + i*64, 480));
            }

            for (int i = 0; i < inventory.size / 2; i++)
            {
                sb.Draw(t, new Vector2(960 + i * 64, 480 + 64));
            }
        }

        public void Move(GameMap m)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (pos.y > 0)
                {
                    if (m.map[pos.x][pos.y - 1].walkable)
                    {
                        moveUp = true;
                        pos.y--;
                    }
                        
                   
                }


            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (pos.y < m.mapY - 1 )
                {
                    if (m.map[pos.x][pos.y + 1].walkable)
                    {
                        moveDown = true;
                        pos.y++;
                    }
                        
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (pos.x < m.mapX - 1 )
                {
                    if (m.map[pos.x + 1][pos.y].walkable)
                    {
                        moveRight = true;
                        pos.x++;
                    }
                        
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (pos.x > 0)
                {
                    if (m.map[pos.x - 1][pos.y].walkable)
                    {
                        moveLeft = true;
                        pos.x--;
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
                sb.Draw(playerAnim.getFrame(), new Vector2((pos.x * 64 + 64 * getWalkTimer() / 1000) - cameraX, pos.y * (64) - cameraY));
            }
            else if (getIsMoving() && moveRight)
            {
                sb.Draw(playerAnim.getFrame(), new Vector2((pos.x * 64 - 64 * getWalkTimer() / 1000) - cameraX, pos.y * (64) - cameraY));
            }
            else if (getIsMoving() && moveDown)
            {
                sb.Draw(playerAnim.getFrame(), new Vector2((pos.x * 64) - cameraX, (pos.y * 64 - 64 * getWalkTimer() / 1000) - cameraY));
            }
            else if (getIsMoving() && moveUp)
            {
                sb.Draw(playerAnim.getFrame(), new Vector2((pos.x * 64) - cameraX, (pos.y * 64 + 64 * getWalkTimer() / 1000) - cameraY));
            }
            else
            {
                sb.Draw(tex, new Vector2(pos.x * 64 - cameraX, pos.y * 64 - cameraY));
            }
        }

        public void Update(GameMap m, GameTime gameTime)
        {
            float delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            playerAnim.update(delta);

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
