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


public class MikeDraw
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
        public NPC enemy1;
        public NPC enemy2;

        public NPC[] npcs;

        public GameMap gameMap;

        private Texture2D fire;
        private Texture2D grass;
        private Texture2D water;
        private Texture2D wall;
        private Texture2D dragon;
        private Texture2D cat;
        private Texture2D troll;
        private Texture2D background;

        private int tType;

        private Texture2D[] test;

        private SpriteFont font;

        public MikeDraw draw;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grass = Content.Load<Texture2D>("TileSprites/grass");
            water = Content.Load<Texture2D>("TileSprites/water");
            fire = Content.Load<Texture2D>("TileSprites/fire");
            wall = Content.Load<Texture2D>("TileSprites/wall"); // THIS STUFF SHOULD BE IN LOAD CONTENT ^^
            dragon = Content.Load<Texture2D>("TileSprites/dragon");
            cat = Content.Load<Texture2D>("TileSprites/katt");
            troll = Content.Load<Texture2D>("TileSprites/troll");
            background = Content.Load<Texture2D>("TileSprites/background");

            font = Content.Load<SpriteFont>("Test");

            this.IsMouseVisible = true;



            // TODO: Add your initialization logic here
            test = new Texture2D[] { grass, water, fire, wall, cat, troll, background };

            gameMap = new GameMap(100, 50, test);
            draw = new MikeDraw();
            draw.setFont(font, spriteBatch);


            player = new Player(17, 15, 6, cat);
            enemy1 = new NPC(7, 7, 0, dragon);
            enemy2 = new NPC(9, 9, 0, troll);
            npcs = new NPC[] { enemy1, enemy2 };
            npcs[0].health = 5;
            npcs[1].health = 10;

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

            foreach(NPC n in npcs)
            {
                spriteBatch.Draw(n.tex, new Vector2(n.x * 64 - player.cameraX, n.y * 64 - player.cameraY));
            }

            /* 
            for (int index = 0; index < 10; index++ )
                spriteBatch.Draw(background, new Vector2(index*64, 640-64)); Rita ut fula gråa rutor för UI start
            */

            if (gameMap.map[player.x][player.y].getType() == 2)
                player.levelUp();

            foreach (NPC n in npcs)
            {
                if (player.x == n.x && player.y == n.y)
                {
                    player.target = n;
                    player.attack();
                    if (n.health <= 0)
                    {
                        n.x = 200;
                        player.target = null;
                        player.levelUp();
                    }
                    spriteBatch.DrawString(font, " Target Health : " + n.health.ToString(), new Vector2(450, 40), Color.Black);
                    
                }

            }

            spriteBatch.DrawString(font, " X : " + player.x.ToString(), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, " Y : " + player.y.ToString(), new Vector2(120, 10), Color.Black);

            spriteBatch.DrawString(font, " Walkable : " + gameMap.map[player.x][player.y].walkable.ToString(), new Vector2(10, 40), Color.Black);
            spriteBatch.DrawString(font, " TileTypeID : " + gameMap.map[player.x][player.y].getType().ToString(), new Vector2(10, 70), Color.Black);
            spriteBatch.DrawString(font, " walkDelay : " + player.getWalkDelay(), new Vector2(10, 100), Color.Black);

            spriteBatch.DrawString(font, " Level : " + player.level.ToString(), new Vector2(1000, 10), Color.Black);
            spriteBatch.DrawString(font, " Health : " + player.health.ToString(), new Vector2(1000, 40), Color.Black);
            spriteBatch.DrawString(font, " Damage : " + player.damage.ToString(), new Vector2(1000, 70), Color.Black);

            spriteBatch.DrawString(font, " Next attack : " + (int)player.attackTimer/100, new Vector2(1000, 100), Color.Black);

            draw.drawString(" Mouse X : ", oldState.X, 400, 500);
            draw.drawString(" Mouse Y : ", oldState.Y, 400, 540);

            //draw.drawString(" Health : ", player.health, 500, 500 ); Egen klass för ett enklare rita ut strängar
            // Slipper skicka med font, göra ny vector och slipper skicka med färg


            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
