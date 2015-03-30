using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Libraries
{
    class SpriteAnimation
    {

        #region Sprite Properties Struct

        public struct spriteProperties
        {

            public string name;
            public Texture2D texture;
            public Point frameSize;
            public Point sheetSize;
            public Point startPos;
            public Point endPos;
            public int millisecondsPerFrame;
            public Color color;

        }

        #endregion

        #region Fields

        public spriteProperties currentSprite;
        public List<spriteProperties> sprites = new List<spriteProperties>();
        protected Vector2 velocity,
                          acceleration,
                          position;
        protected Point currentFrame;
        protected int timeSinceLastFrame;
        protected KeyboardState keyboardState;
        public Collision.Circle collisionCircle;
        protected float rotation = 0;
        public Vector2 rotationCenter = Vector2.Zero;
        public Vector2 speed;

        #endregion

        #region Initialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public SpriteAnimation(Vector2 pos)
        {
            timeSinceLastFrame = 0;
            position = pos;
            currentFrame = currentSprite.startPos;
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame >= currentSprite.millisecondsPerFrame)
            {
                currentFrame.X++;
                if (currentFrame.X > currentSprite.sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if (currentFrame.Y > currentSprite.sheetSize.Y)
                    {
                        currentFrame = currentSprite.startPos;
                    }
                }
                timeSinceLastFrame = 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(currentSprite.texture, position, new Rectangle(currentSprite.frameSize.X * currentFrame.X, currentSprite.frameSize.Y * currentFrame.Y, currentSprite.frameSize.X, currentSprite.frameSize.Y), currentSprite.color);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        public void setWash(Color color)
        {
            if (currentSprite.color != color)
            {
                currentSprite.color = color;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tex"></param>
        /// <param name="frameSize"></param>
        /// <param name="sheetSize"></param>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        /// <param name="msPF"></param>
        /// <param name="color"></param>
        public void addAnimation(string name, Texture2D tex, Point frameSize, Point sheetSize, Point startP, Point endP, int msPF, Color color)
        {
            spriteProperties tmpProps;
            tmpProps.name = name;
            tmpProps.texture = tex;
            tmpProps.frameSize = frameSize;
            tmpProps.sheetSize = sheetSize;
            tmpProps.startPos = startP;
            tmpProps.endPos = endP;
            tmpProps.millisecondsPerFrame = msPF;
            tmpProps.color = color;

            sprites.Add(tmpProps);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void setAnimation(string name)
        {
            if (currentSprite.name != name)
            {
                foreach (spriteProperties a in sprites)
                {
                    if (a.name == name)
                    {
                        currentSprite = a;
                        currentFrame = a.startPos;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tex"></param>
        public virtual void addAnimations(Texture2D tex) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Rectangle collisionRect()
        {
            return new Rectangle((int)position.X, (int)position.Y, currentSprite.frameSize.X, currentSprite.frameSize.Y);
        }

        public void setPos(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 getPos()
        {
            return position;
        }

        #endregion
    
    }
}
