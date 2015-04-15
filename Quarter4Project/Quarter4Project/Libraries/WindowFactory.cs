using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Libraries
{
    class WindowFactory
    {

        #region Sub-Classes

        /// <summary>
        /// Creates a new window.
        /// </summary>
        public class Window
        {

            #region Initialization

            /// <summary>
            /// Creates a new window with the following parameters.
            /// </summary>
            /// <param name="windowTexture">Texture of the window.</param>
            /// <param name="windowSize">Size of the window.</param>
            /// <param name="windowPosition">Position of the window.</param>
            public Window(Texture2D windowTexture, Point windowSize, Point windowPosition)
            {
                this.windowTexture = windowTexture;
                this.windowSize = windowSize;
                this.windowPosition = windowPosition;
            }

            /// <summary>
            /// Creates a new window with the following parameters.
            /// </summary>
            /// <param name="windowColor">Color of the window.</param>
            /// <param name="gD">GraphicsDevice to convert color to texture.</param>
            /// <param name="windowSize">Size of the window.</param>
            /// <param name="windowPosition">Position of the window.</param>
            public Window(Color windowColor, GraphicsDevice gD, Point windowSize, Point windowPosition)
            {
                this.windowTexture = this.convertColorToTexture2D(windowColor, gD);
                this.windowSize = windowSize;
                this.windowPosition = windowPosition;
            }

            #endregion

            #region Fields

            public Texture2D windowTexture { get; private set; }
            public Point windowSize { get; private set; }
            public Point windowPosition { get; private set; }

            #endregion

            #region Methods

            /// <summary>
            /// Converts a color into a Texture2D.
            /// </summary>
            /// <param name="color">The color that is being turned into a Texture2D.</param>
            /// <param name="gD">GraphicsDevice used to create our Texture2D.</param>
            /// <returns>Texture2D format of our color we passed through.</returns>
            public Texture2D convertColorToTexture2D(Color color, GraphicsDevice gD)
            {
                Texture2D tex = new Texture2D(gD, 1, 1, false, SurfaceFormat.Color);
                tex.SetData<Color>(new Color[] { color });

                return tex;
            }

            /// <summary>
            /// Rectangle of the window
            /// </summary>
            /// <returns>Returns rectangle of window.</returns>
            public Rectangle collisionRect()
            {
                return new Rectangle(windowPosition.X, windowPosition.Y, windowSize.X, windowSize.Y);
            }

            public void setPosition(Point pos)
            {
                windowPosition = pos;
            }

            #endregion

        }

        #endregion

    }
}
