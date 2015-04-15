using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLevels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Managers
{
    /// <summary>
    /// This class will display the team logo and then continue to the menu.
    /// </summary>
    class SplashManager : DrawableGameComponent
    {

        #region Constants

        /* *** Make sure the fadeInOutTime does not exceed half of the totalTime, it will cause visual problems and throw an error! *** */

        // How long will we see the splash screen? (in milliseconds)
        private const int totalTime = 4000;

        // How long does the fade in and out take? (in milliseconds)
        private const int fadeInOutTime = 1000;

        // Where is the file to the splash screen logo?
        private const string splashLogoDir = "Backgrounds/Splash";

        #endregion

        #region Fields

        private Game1 game;
        private SpriteBatch spriteBatch;

        private Texture2D backgroundImage,
                  fadeTexture;

        private int countdown = totalTime,
                    fadeIn = fadeInOutTime,
                    fadeOut = 0;

        #endregion

        #region Initialization

        public SplashManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set the image background to the team logo.
            backgroundImage = Game.Content.Load<Texture2D>(splashLogoDir);

            // Fade texture is used to create the fade in and fade out effect.
            fadeTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fadeTexture.SetData<Color>(new Color[] { new Color(0, 0, 0, 1.0f) });

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the fade in and fade out.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Checks if the fadeInOutTime is greater than half of the totalTime and throws an error if true.
            if ((fadeInOutTime * 2) > totalTime)
                throw new ArgumentOutOfRangeException("The fade time must be half or below half the total time of the splash.", "Fade Time: " + fadeInOutTime + " Total Time: " + totalTime);

            // Create new fadeTexture so we can manipulate the opacity.
            fadeTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            // Countdown is equal to the totalTime, in order to create the countdown effect it has to be subtracted from.
            countdown -= gameTime.ElapsedGameTime.Milliseconds;

            // If the countdown is 0 then the splash screen is over, otherwise check for fading in and fading out.
            if (countdown <= 0)
                game.setCurrentLevel(GameLevels.GameLevels.Menu);
            else if (countdown <= fadeInOutTime)
            {
                fadeOut += gameTime.ElapsedGameTime.Milliseconds;
                fadeTexture.SetData<Color>(new Color[] { new Color(0, 0, 0, (fadeOut / 2)) });
            }
            else if (countdown >= (totalTime - fadeInOutTime))
            {
                fadeIn -= gameTime.ElapsedGameTime.Milliseconds;
                fadeTexture.SetData<Color>(new Color[] { new Color(0, 0, 0, (fadeIn / 2)) });
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the background image and the fade in and fade out.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            // Draw the background and the fading.
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 960, 620), Color.White);
            spriteBatch.Draw(fadeTexture, new Rectangle(0, 0, 960, 620), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}