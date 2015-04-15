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
    class WindowEvents
    {

        #region Fields

        Game1 game;

        GlobalEvents gE;

        MouseState mouseState;
        Point mousePos;

        #endregion

        #region Initialization

        public WindowEvents(Game1 g)
        {
            game = g;
            gE = new GlobalEvents(game);
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime, List<WindowFactory.Window> windowList)
        {
            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            for (int i = 0; i < windowList.Count; i++)
            {
                if (windowList[i].collisionRect().Intersects(getMousePos()))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        windowList[i].setPosition(new Point(mousePos.X - 50, mousePos.Y - 50));
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, List<WindowFactory.Window> windowList)
        {
            foreach (WindowFactory.Window w in windowList)
                spriteBatch.Draw(w.windowTexture, new Rectangle(w.windowPosition.X, w.windowPosition.Y, w.windowSize.X, w.windowSize.Y), Color.White);
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Rectangle position.
        /// </summary>
        /// <returns>Returns rectangle position of the mouse.</returns>
        public Rectangle getMousePos()
        {
            return new Rectangle(mousePos.X, mousePos.Y, 1, 1);
        }

        #endregion

    }
}
