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

        /// <summary>
        /// 
        /// </summary>
        public class Button
        {

            #region Initialization

            /// <summary>
            /// Saves a button with the parameters below.
            /// </summary>
            /// <param name="buttonTexture">Texture of the button.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, int eventID, int animationID)
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
            
            /// <summary>
            /// Saves a button with the parameters below.
            /// </summary>
            /// <param name="buttonTexture">Texture of the button.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="textShadow">Shadow color of the text.</param>
            /// <param name="textShadowPosition">Position of the shadow in relation to the text.</param>
            /// <param name="textShadowSize">Size of the text shadow.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, int eventID, int animationID)
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

            /// <summary>
            /// Saves a button with the parameters below.
            /// </summary>
            /// <param name="buttonTexture">Texture of the button.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="textShadow">Color of the shadow of the text.</param>
            /// <param name="textShadowPosition">Position of the shadow in relation to the text.</param>
            /// <param name="textShadowSize">Size of the shadow of the text.</param>
            /// <param name="buttonBorderTexture">Texture of the border of the button.</param>
            /// <param name="buttonBorderSize">Size of the border.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Texture2D buttonTexture, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, Texture2D buttonBorderTexture, Point buttonBorderSize, int eventID, int animationID)
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

            /// <summary>
            /// Creates a button with the following parameters.
            /// </summary>
            /// <param name="buttonTexture">Color of the button.</param>
            /// <param name="gD">GraphicsDevice to convert the color to a texture.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, int eventID, int animationID)
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

            /// <summary>
            /// Creates a button with the following parameters.
            /// </summary>
            /// <param name="buttonTexture">Color of the button.</param>
            /// <param name="gD">GraphicsDevice to convert the color to a texture.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="textShadow">Color of the shadow of the text.</param>
            /// <param name="textShadowPosition">Position of the shadow in relation to the text.</param>
            /// <param name="textShadowSize">Size of the shadow.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, int eventID, int animationID)
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

            /// <summary>
            /// Creates a button with the following parameters.
            /// </summary>
            /// <param name="buttonTexture">Color of the button.</param>
            /// <param name="gD">GraphicsDevice to convert the color to a texture.</param>
            /// <param name="buttonPosition">Position of the button on the map.</param>
            /// <param name="buttonSize">Size of the button.</param>
            /// <param name="buttonFont">Font used on the button.</param>
            /// <param name="buttonText">Text that appears on the button.</param>
            /// <param name="textColor">Color of the text.</param>
            /// <param name="textShadow">Color of the text shadow.</param>
            /// <param name="textShadowPosition">Position of the shadow in relation of the text.</param>
            /// <param name="textShadowSize">Size of the shadow.</param>
            /// <param name="buttonBorderTexture">Color of the border.</param>
            /// <param name="buttonBorderSize">Size of the border.</param>
            /// <param name="eventID">Action that is completed when the button is clicked.</param>
            /// <param name="animationID">Action that is completed when the mouse is hovered over the button.</param>
            public Button(Color buttonTexture, GraphicsDevice gD, Vector2 buttonPosition, Point buttonSize, SpriteFont buttonFont, string buttonText, Color textColor, Color textShadow, Vector2 textShadowPosition, float textShadowSize, Color buttonBorderTexture, Point buttonBorderSize, int eventID, int animationID)
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

            #region Fields

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

            #region Methods

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

        /// <summary>
        /// 
        /// </summary>
        public class AnimatedButton : SpriteAnimation
        {

            #region Initialization

            /// <summary>
            /// Creates an animated button using the following parameters.
            /// </summary>
            /// <param name="animList">Animation list of the button.</param>
            /// <param name="pos">Position of the button.</param>
            /// <param name="eventID">Event that is completed when the button is clicked.</param>
            /// <param name="animationID">Animation that is completed when the button is hovered.</param>
            public AnimatedButton(List<AnimatedButton.AddAnimation> animList, Vector2 pos, int eventID, int animationID)
                : base(pos)
            {
                this.animList = animList;
                position = pos;
                this.eventID = eventID;
                this.animID = animationID;
                addAnimations();
            }

            #endregion

            #region Fields

            List<AddAnimation> animList;
            public int eventID { get; private set; }
            public int animID { get; private set; }

            #endregion

            #region Methods

            /// <summary>
            /// Adds all the animations from the animation list.
            /// </summary>
            public void addAnimations()
            {
                for (int i = 0; i < animList.Count; i++)
                {
                    addAnimation(animList[i].name, animList[i].texture, animList[i].frameSize, animList[i].sheetSize, animList[i].startPos, animList[i].endPos, animList[i].milliPerFrame, animList[i].wash);
                    setAnimation(animList[0].name);
                }
            }

            #endregion

            #region Sub-Classes

            /// <summary>
            /// 
            /// </summary>
            public class AddAnimation
            {

                #region Initialization

                /// <summary>
                /// Adds an animation based on the following parameters.
                /// </summary>
                /// <param name="name">Name of the animation</param>
                /// <param name="tex">Sprite sheet used in the animation</param>
                /// <param name="frameSize">Frame size of the animation in pixels.</param>
                /// <param name="sheetSize">Sheet size of the total frames used</param>
                /// <param name="startPos">Starting position of the animation.</param>
                /// <param name="endPos">Ending position of the animation.</param>
                /// <param name="milliPerFrame">Frame change rate.</param>
                /// <param name="wash">Color wash.</param>
                public AddAnimation(string name, Texture2D tex, Point frameSize, Point sheetSize, Point startPos, Point endPos, int milliPerFrame, Color wash)
                {
                    this.name = name;
                    this.texture = tex;
                    this.frameSize = frameSize;
                    this.sheetSize = sheetSize;
                    this.startPos = startPos;
                    this.endPos = endPos;
                    this.milliPerFrame = milliPerFrame;
                    this.wash = wash;
                    this.eventID = eventID;
                    this.animID = animID;
                }

                #endregion

                #region Fields

                public string name { get; private set; }
                public Texture2D texture { get; private set; }
                public Point frameSize { get; private set; }
                public Point sheetSize { get; private set; }
                public Point startPos { get; private set; }
                public Point endPos {get; private set;}
                public int milliPerFrame { get; private set; }
                public Color wash { get; private set; }
                public int eventID { get; private set; }
                public int animID { get; private set; }

                #endregion

            }

            #endregion

        }

    }
}