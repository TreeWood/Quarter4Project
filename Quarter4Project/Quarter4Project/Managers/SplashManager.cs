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
    /// This class will display the group logo and then continue to the menu.
    /// </summary>
    class SplashManager : DrawableGameComponent
    {

        #region Constants

        // How long will we see the splash screen? (in milliseconds)
        private const int totalTime = 4000;

        // How long does the fade in and out take? (in milliseconds)
        private const int fadeInOutTime = 1000;

        #endregion

        #region Fields

        Game1 game;
        SpriteBatch spriteBatch;

        Texture2D backgroundImage,
                  fadeTexture;

        int countdown = totalTime, fadeIn = fadeInOutTime, fadeOut = 0;

        #endregion

        #region Initialization

        public SplashManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {

            backgroundImage = Game.Content.Load<Texture2D>(@"Backgrounds/Splash");

            fadeTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            fadeTexture.SetData<Color>(new Color[] { new Color(0, 0, 0, 1.0f) });

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            if ((fadeInOutTime * 2) > totalTime) 
            {
                throw new ArgumentOutOfRangeException("The fade time must be half or below half the total time of the splash.", "Fade Time: " + fadeInOutTime + " Total Time: " + totalTime);
            }

            fadeTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            countdown -= gameTime.ElapsedGameTime.Milliseconds;
            if (countdown <= 0)
            {
                game.setCurrentLevel(GameLevels.GameLevels.Menu);
            }
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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 960, 620), Color.White);
            spriteBatch.Draw(fadeTexture, new Rectangle(0, 0, 960, 620), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

    }
}
