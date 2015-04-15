using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.EventManagers
{
    /// <summary>
    /// Updates, Draws and completes specified events related to the buttons.
    /// </summary>
    class ButtonEvents
    {

        #region Fields

        Game1 game;
        
        MouseState mouseState, mouseState2, prevMouseState, prevMouseState2;
        Point mousePos, mousePos2;

        GlobalEvents gE;

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize variables.
        /// </summary>
        /// <param name="g">Instance of game.</param>
        public ButtonEvents(Game1 g)
        {
            game = g;
            gE = new GlobalEvents(game);
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates non-animating buttons and checks and completes events.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="buttonList">non-animating button list that is to be updated.</param>
        public void Update(GameTime gameTime, List<ButtonFactory.Button> buttonList)
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
                        case 1001:
                            game.setCurrentLevel(GameLevels.GameLevels.Menu);
                            break;
                        case 2000:
                            game.setCurrentLevel(GameLevels.GameLevels.Game);
                            break;
                    }
                }
            }

            prevMouseState = mouseState;
        }

        /// <summary>
        /// Updates animating buttons and checks and completes events.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="buttonList">Animating button list that is to be updated.</param>
        public void Update(GameTime gameTime, List<ButtonFactory.AnimatedButton> buttonList)
        {
            mouseState2 = Mouse.GetState();
            mousePos2 = new Point(mouseState2.X, mouseState2.Y);

            //Updates all the buttons that are added to our buttonList.
            for (int i = 0; i < buttonList.Count; i++)
                buttonList[i].Update(gameTime);

            //Updates all the buttons that are added to our buttonList.
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].collisionRect().Intersects(getMousePos2()))
                {
                    if (buttonList[i].sprites.Count(p => p.name == "Hover") >= 1)
                    {
                        buttonList[i].setAnimation("Hover");
                    }

                    if (mouseState2.LeftButton == ButtonState.Pressed && prevMouseState2.LeftButton == ButtonState.Released)
                    {
                        switch (buttonList[i].eventID)
                        {
                            default:
                            case 0:
                                break;
                            case 1000:
                                gE.quitGame();
                                break;
                            case 1001:
                                game.setCurrentLevel(GameLevels.GameLevels.Menu);
                                break;
                            case 2000:
                                game.setCurrentLevel(GameLevels.GameLevels.Game);
                                break;
                        }
                    }
                }
                else
                {
                    if (buttonList[i].sprites.Count(p => p.name == "Hello") >= 1)
                    {
                        buttonList[i].setAnimation("Hello");
                    }
                }

            }

            prevMouseState2 = mouseState2;
        }

        /// <summary>
        /// Draws non-animating buttons.
        /// </summary>
        /// <param name="spriteBatch">Enabled drawing for textures.</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="buttonList">non-animating button list that is to be drawn.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, List<ButtonFactory.Button> buttonList)
        {

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

        }

        /// <summary>
        /// Draws animating buttons.
        /// </summary>
        /// <param name="spriteBatch">Used to draw the textures.</param>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="buttonList">Animating button list that is to be drawn.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, List<ButtonFactory.AnimatedButton> buttonList)
        {

            for (int i = 0; i < buttonList.Count; i++)
                buttonList[i].Draw(gameTime, spriteBatch);

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

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns>Returns rectangle at mouse position.</returns>
        public Rectangle getMousePos2()
        {
            return new Rectangle(mousePos2.X, mousePos2.Y, 1, 1);
        }

        #endregion

    }
}