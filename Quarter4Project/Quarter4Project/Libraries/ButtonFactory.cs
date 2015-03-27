using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.EventManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Libraries
{
    /// <summary>
    /// This class is used to create buttons.
    /// </summary>
    class ButtonFactory
    {

        #region Structs

        public struct Button
        {

            #region Struct Initialization

            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, int eventID, int animationID)
                : this()
            {
                this.texture = buttonTexture;
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSizeint, int eventID, int animationID)
                : this()
            {
                this.texture = buttonTexture;
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.textShadow = textShadow;
                this.textShadowPos = textShadowPosition;
                this.textShadowSize = textShadowSize;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, Texture2D buttonBorderTexture, Point buttonBorderSize, int eventID, int animationID)
                : this()
            {
                this.texture = buttonTexture;
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.textShadow = textShadow;
                this.textShadowPos = textShadowPosition;
                this.textShadowSize = textShadowSize;
                this.borderTexture = buttonBorderTexture;
                this.borderSize = buttonBorderSize;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, int eventID, int animationID)
                : this()
            {
                this.texture = this.convertColorToTexture2D(buttonTexture, gD);
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, int eventID, int animationID)
                : this()
            {
                this.texture = this.convertColorToTexture2D(buttonTexture, gD);
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.textShadow = textShadow;
                this.textShadowPos = textShadowPosition;
                this.textShadowSize = textShadowSize;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, Color buttonBorderTexture, Point buttonBorderSize, int eventID, int animationID)
                : this()
            {
                this.texture = this.convertColorToTexture2D(buttonTexture, gD);
                this.position = buttonPosition;
                this.size = buttonSize;
                this.font = buttonFont;
                this.text = buttonText;
                this.textColor = textColor;
                this.textShadow = textShadow;
                this.textShadowPos = textShadowPosition;
                this.textShadowSize = textShadowSize;
                this.borderTexture = this.convertColorToTexture2D(buttonBorderTexture, gD);
                this.borderSize = buttonBorderSize;
                this.eventID = eventID;
                this.animationID = animationID;
            }

            #endregion

            #region Struct Fields

            public Texture2D texture { get; private set; }
            public Vector2 position { get; private set; }
            public Point size { get; private set; }
            public SpriteFont font { get; private set; }
            public string text { get; private set; }
            public Color textColor { get; private set; }
            public Color textShadow { get; private set; }
            public Vector2 textShadowPos { get; private set; }
            public float textShadowSize { get; private set; }
            public Texture2D borderTexture { get; private set; }
            public Point borderSize { get; private set; }
            public int eventID { get; private set; }
            public int animationID { get; private set; }

            #endregion

            #region Struct Methods

            /// <summary>
            /// Finds the center of the button.
            /// </summary>
            /// <returns>The Vector2 of the center of the button.</returns>
            public Vector2 getButtonCenter()
            {
                return new Vector2(position.X + (size.X / 2), position.Y + (size.Y / 2));
            }

            /// <summary>
            /// Gets the positioning of the border.
            /// </summary>
            /// <returns>Vector2 format of the position of the border.</returns>
            public Vector2 getBorderPosition()
            {
                return new Vector2(position.X - (borderSize.X / 2), position.Y - (borderSize.Y / 2));
            }

            /// <summary>
            /// Gets the size of the button.
            /// </summary>
            /// <returns>Rectangle size of the button</returns>
            public Rectangle getButtonSize()
            {
                return new Rectangle(0, 0, size.X, size.Y);
            }

            /// <summary>
            /// Gets the position of the font.
            /// </summary>
            /// <returns>Vector2 format of the font position.</returns>
            public Vector2 getFontPos()
            {
                return new Vector2(getButtonCenter().X - (font.MeasureString(text).X / 2), getButtonCenter().Y - (font.MeasureString(text).Y / 2));
            }

            /// <summary>
            /// Gets the position of the shadow position.
            /// </summary>
            /// <returns>Vector2 format of the shadow positioning.</returns>
            public Vector2 getFontShadowPos()
            {
                return new Vector2(getButtonCenter().X - (font.MeasureString(text).X / 2) + textShadowPos.X, getButtonCenter().Y - (font.MeasureString(text).Y / 2) + textShadowPos.Y);
            }

            /// <summary>
            /// Gets the position of the border.
            /// </summary>
            /// <returns>Rectangle format of the border positioning.</returns>
            public Rectangle getBorderPos()
            {
                return new Rectangle(0, 0, size.X + borderSize.X, size.Y + borderSize.Y);
            }

            /// <summary>
            /// Gets the rectangle of the button.
            /// </summary>
            /// <returns>Rectangle format of the position and size of the rectangle.</returns>
            public Rectangle getCollisionRect()
            {
                return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
            }

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

            #endregion

        }

        #endregion

    }
}