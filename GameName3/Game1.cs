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

        public GameMap gameMap;

        private Texture2D fire;
        private Texture2D grass;
        private Texture2D water;
        private Texture2D wall;
        private Texture2D dragon;
        private Texture2D cat;

        private Texture2D[] test;

        private SpriteFont font;


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

            font = Content.Load<SpriteFont>("Test");


            // TODO: Add your initialization logic here
            test = new Texture2D[] { grass, water, fire, wall, cat };

            gameMap = new GameMap(20, 10, test);


            player = new Player(4, 6, 6);


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
            player.Update(gameMap);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            gameMap.draw(spriteBatch);

            switch(player.spriteType)
            { 
                case 1:
                    break;
                case 2:
                    spriteBatch.Draw(dragon, new Vector2(player.x * 64, player.y * 64));
                    break;
                case 6:
                    spriteBatch.Draw(cat, new Vector2(player.x * 64, player.y * 64));
                    break;
            
            }


            spriteBatch.DrawString(font, " X : " + player.x.ToString(), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, " Y : " + player.y.ToString(), new Vector2(120, 10), Color.Black);

            spriteBatch.DrawString(font, " Walkable : " + gameMap.map[player.x][player.y].walkable.ToString(), new Vector2(10, 40), Color.Black);
            spriteBatch.DrawString(font, gameMap.map[player.x][player.y].getType().ToString(), new Vector2(10, 70), Color.Black);


            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
