using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Quarter4Project.Managers;
using GameLevels;
using Quarter4Project.Libraries;

namespace Quarter4Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region Constants

        // How wide is our window? (in pixels)
        private const int windowWidth = 960;

        // How tall is our window? (in pixels)
        private const int windowHeight = 620;

        #endregion

        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MenuManager menuManager;
        GameManager gameManager;
        SplashManager splashManager;

        public GameLevels.GameLevels currentLevel { get; private set; }
        public GameLevels.GameLevels previousLevel { get; private set; }

        #endregion

        #region Initialization

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set the size of our window.
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;


            // Set our mouse to visible to we can see it.
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            splashManager = new SplashManager(this);
            Components.Add(splashManager);

            menuManager = new MenuManager(this);
            Components.Add(menuManager);

            gameManager = new GameManager(this);
            Components.Add(gameManager);

            setCurrentLevel(GameLevels.GameLevels.Splash);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

        #endregion

        #region Update and Draw

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        public void setCurrentLevel(GameLevels.GameLevels gameLevel)
        {
            splashManager.Enabled = false;
            splashManager.Visible = false;
            menuManager.Enabled = false;
            menuManager.Visible = false;
            gameManager.Enabled = false;
            gameManager.Visible = false;

            switch (gameLevel)
            {
                case GameLevels.GameLevels.Splash:
                    previousLevel = currentLevel;
                    splashManager.Enabled = true;
                    splashManager.Visible = true;
                    currentLevel = gameLevel;
                    break;
                case GameLevels.GameLevels.Menu:
                    previousLevel = currentLevel;
                    menuManager.Enabled = true;
                    menuManager.Visible = true;
                    currentLevel = gameLevel;
                    break;
                case GameLevels.GameLevels.Game:
                    previousLevel = currentLevel;
                    gameManager.Enabled = true;
                    gameManager.Visible = true;
                    currentLevel = gameLevel;
                    break;
            }
        }

        #endregion

    }
}