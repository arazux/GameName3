#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion


namespace GameName3
{
 
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Player player;

        public List<NPC> npcs;
        public List<Texture2D> tileSprites;
        public List<Texture2D> npcSprites;
        public List<Texture2D> UISprites;

        public GameMap gameMap;

        private Texture2D cat;

        private int tType;

        private SpriteFont font;

        public EasyLoad load;

        public EasyDraw draw;

        private MouseState oldState;

        public Game1()
            : base()
        {
        
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 640;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            load = new EasyLoad(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileSprites = new List<Texture2D> { load.LoadSprite("grass2", 0), load.LoadSprite("water", 0), load.LoadSprite("fire", 0), load.LoadSprite("wall", 0) };
            npcSprites = new List<Texture2D> { load.LoadSprite("dragon", 0), load.LoadSprite("troll", 0) };
            UISprites = new List<Texture2D> { load.LoadSprite("background", 0), load.LoadSprite("background2", 0) };

            font = Content.Load<SpriteFont>("Test");

            this.IsMouseVisible = true;

            // TODO: Add your initialization logic here

            gameMap = new GameMap(tileSprites);
            draw = new EasyDraw();
            draw.setFont(font, spriteBatch);

            player = new Player(2, 2, 6, load);

            npcs = new List<NPC>();
            npcs.Add(new NPC(7, 7, 0));
            npcs.Add(new NPC(9, 9, 1));
            npcs[0].health = 3;
            npcs[1].health = 12;

            tType = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // TODO: use this.Content to load your game content here


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameMap, gameTime);

            MouseState newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                gameMap.map[((int)oldState.X + player.cameraX) / 64][((int)oldState.Y + player.cameraY) / 64].setType(tType);

            }

            oldState = newState;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.O))
                player.incMoveDelay();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                player.decMoveDelay();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D0))
                tType = 0;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D1))
                tType = 1;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D2))
                tType = 2;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D3))
                tType = 3;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S))
                gameMap.saveFile();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            gameMap.Draw(spriteBatch, player);
            player.Draw(spriteBatch);
            

            spriteBatch.Draw(UISprites[0], new Vector2(0, 576));
            spriteBatch.Draw(UISprites[1], new Vector2(960, 0));
            player.drawInventory(UISprites[2], spriteBatch);

            foreach(NPC n in npcs)
            {
                spriteBatch.Draw(npcSprites[n.ID], new Vector2(n.pos.x * 64 - player.cameraX, n.pos.y * 64 - player.cameraY));
            }

            foreach (NPC n in npcs)
            {
                if ( player.pos.isEqual(n.pos) )
                {
                    player.target = n;

                    if( player.attack() )
                    {
                        n.Retaliate(player);
                    }

                    if (n.health <= 0)
                    {
                        npcs.Remove(n);
                        player.target = null;
                        player.levelUp();
                        break;

                    }

                    if(player.health <= 0)
                    {
                        Exit();
                    }

                    spriteBatch.DrawString(font, " Target Health : " + n.health.ToString(), new Vector2(450, 40), Color.Black);
                    
                }

            }

            spriteBatch.DrawString(font, " Walkable : " + gameMap.map[player.pos.x][player.pos.y].walkable.ToString(), new Vector2(10, 40), Color.Black);

            draw.drawString(" X : ", player.pos.x, 10, 10);
            draw.drawString(" Y : ", player.pos.y, 120, 10);

            //draw.drawString(" Walkable : ", gameMap.map[player.x][player.y].walkable.toString(), 10, 40);
            draw.drawString(" TileTypeID : ", gameMap.map[player.pos.x][player.pos.y].getType(), 10, 70);
            draw.drawString(" walkDelay : ", (int)player.getWalkDelay(), 10, 100);



            draw.drawString(" Level : ", player.level, 1000, 10);
            draw.drawString(" Health : ", player.health, 1000, 40);
            draw.drawString(" Damage : ", player.damage, 1000, 70);


            draw.drawString(" Next attack :", (int)player.attackTimer / 100, 1000, 100);

            draw.drawString(" Mouse X : ", oldState.X, 400, 500);
            draw.drawString(" Mouse Y : ", oldState.Y, 400, 540);

            draw.drawString(" Camera X : ", player.cameraX, 400, 420);
            draw.drawString(" Camera Y : ", player.cameraY, 400, 460);

            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
