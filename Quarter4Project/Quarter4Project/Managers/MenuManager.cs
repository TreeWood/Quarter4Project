using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.EventManagers;
using Quarter4Project.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Managers
{
    /// <summary>
    /// This class will display the menu options that the user may navigate through.
    /// </summary>
    class MenuManager : DrawableGameComponent
    {

        #region Fields

        Game1 game;
        SpriteBatch spriteBatch;
        SpriteFont Consolas;

        MouseState mouseState, prevMouseState;
        Point mousePos;

        Texture2D backgroundImage,
                  sampleButtonTexture;

        List<ButtonFactory.Button> buttonList;

        GlobalEvents gE;

        #endregion

        #region Initialization

        public MenuManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            buttonList = new List<ButtonFactory.Button>();

            gE = new GlobalEvents(game);

            base.Initialize();

            buttonList.Add(new ButtonFactory.Button(sampleButtonTexture, new Vector2(50, 50), new Point(50, 25), Consolas, "Quit", new Color(20, 9, 0), 1000, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(200, 100, 100), GraphicsDevice, new Vector2(150, 150), new Point(50, 75), Consolas, "Start", new Color(100, 100, 100), new Color(0, 0, 0), new Vector2(1, 1), 1.05f, 2000, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(200, 100, 100), GraphicsDevice, new Vector2(250, 250), new Point(100, 255), Consolas, "Quit", new Color(100, 100, 100), new Color(0, 0, 0), new Vector2(1, 1), 1.05f, new Color(0, 123, 231), new Point(8, 4), 1000, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(135, 14, 0), GraphicsDevice, new Vector2(400, 200), new Point(75, 50), Consolas, "Hello", new Color(0, 0, 0), new Color(255, 28, 3), new Vector2(1, 1), 1.0f, new Color(190, 66, 54), new Point(2, 2), 0, 0));
        }

        protected override void LoadContent()
        {
            //backgroundImage = Game.Content.Load<Texture2D>(@"Backgrounds/MENUBACKGROUND");

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            sampleButtonTexture = Game.Content.Load<Texture2D>(@"Images/btnTex");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            //Updates all the buttons that are added to our buttonList.
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].getCollisionRect().Intersects(getMousePos()) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    switch (buttonList[i].eventID)
                    {
                        default:
                        case 0:
                            break;
                        case 1000:
                            gE.quitGame();
                            break;
                        case 2000:
                            game.setCurrentLevel(GameLevels.GameLevels.Game);
                            break;
                    }
                    
                }
            }

            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 640, 480), Color.White);

            // Draws all the buttons that are added to our buttonList.
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].borderTexture != null)
                {
                    spriteBatch.Draw(buttonList[i].borderTexture, buttonList[i].getBorderPosition(), buttonList[i].getBorderPos(), Color.White);
                }
                spriteBatch.Draw(buttonList[i].texture, buttonList[i].position, buttonList[i].getButtonSize(), Color.White);
                if (buttonList[i].textShadow != null)
                {
                    spriteBatch.DrawString(buttonList[i].font, buttonList[i].text, buttonList[i].getFontShadowPos(), buttonList[i].textShadow, 0.0f, Vector2.Zero, buttonList[i].textShadowSize, SpriteEffects.None, 0.0f);
                }
                    spriteBatch.DrawString(buttonList[i].font, buttonList[i].text, buttonList[i].getFontPos(), buttonList[i].textColor);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns>Rectangle format of the mouse position.</returns>
        public Rectangle getMousePos()
        {
            return new Rectangle(mousePos.X, mousePos.Y, 1, 1);
        }

        #endregion

    }
}
