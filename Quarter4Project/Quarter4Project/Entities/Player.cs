using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.Libraries;
using Quarter4Project.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Entities
{
    /// <summary>
    /// 
    /// </summary>
    class Player : SpriteAnimation
    {

        #region Fields

        GameManager game;
        public Vector2 direction = Vector2.Zero;

        private char pQuad1w,
                     pQuad2w,
                     pQuad3s,
                     pQuad4s,
                     pQuad2d,
                     pQuad2da,
                     pQuad4d,
                     pQuad1a,
                     pQuad2a,
                     opQuad1w,
                     opQuad2w,
                     opQuad3s,
                     opQuad4s,
                     opQuad2d,
                     opQuad4d,
                     opQuad1a,
                     opQuad2a;

        #endregion

        #region Initialization

        public Player(Texture2D texture, Vector2 pos, GameManager g)
            : base(pos)
        {
            game = g;
            addAnimations(texture);
            rotationCenter = new Vector2(15, 15);
            position = pos;
            speed = new Vector2(3, 3);
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime)
        {
            if (game.level == game.prevLevel)
            {

                pQuad1w = game.getWall()[(int)((position.Y) / 15) - 1][(int)((position.X) / 15)];
                pQuad2w = game.getWall()[(int)((position.Y) / 15) - 1][(int)((position.X) / 15) + 1];
                pQuad3s = game.getWall()[(int)((position.Y + 1) / 15) + 2][(int)((position.X) / 15)];
                pQuad4s = game.getWall()[(int)((position.Y + 1) / 15) + 2][(int)((position.X) / 15) + 1];
                pQuad2d = game.getWall()[(int)((position.Y) / 15)][(int)((position.X) / 15) + 2];
                pQuad4d = game.getWall()[(int)((position.Y) / 15) + 1][(int)((position.X) / 15) + 1];
                pQuad2da = game.getWall()[(int)((position.Y) / 15) + 1][(int)((position.X) / 15) + 2];
                pQuad2a = game.getWall()[(int)((position.Y) / 15) + 1][(int)((position.X) / 15) - 1];
                pQuad1a = game.getWall()[(int)((position.Y) / 15)][(int)((position.X) / 15) - 1];

                opQuad1w = game.getObjects()[(int)((position.Y - 15) / 30)][(int)((position.X) / 30)];
                opQuad2w = game.getObjects()[(int)((position.Y - 15) / 30)][(int)((position.X + 15) / 30)];
                opQuad3s = game.getObjects()[(int)((position.Y) / 30) + 1][(int)((position.X) / 30)];
                opQuad4s = game.getObjects()[(int)((position.Y) / 30) + 1][(int)((position.X + 15) / 30)];
                opQuad2d = game.getObjects()[(int)((position.Y) / 30)][(int)((position.X) / 30) + 1];
                opQuad4d = game.getObjects()[(int)((position.Y + 15) / 30)][(int)((position.X) / 30) + 1];
                opQuad1a = game.getObjects()[(int)((position.Y) / 30)][(int)((position.X - 15) / 30)];
                opQuad2a = game.getObjects()[(int)((position.Y + 15) / 30)][(int)(position.X - 15) / 30];

                if (direction.Y == 1)
                {
                    if ((int)((position.Y - speed.Y) / 15) < (int)(position.Y / 15))
                        position.Y -= position.Y % 15;
                    if (position.Y % 15 == 0)
                        direction.Y = 0;
                }

                if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (position.X % 15 == 0 && position.Y % 15 == 0)
                    {
                        if (pQuad3s != '1' && pQuad4s != '1' && opQuad3s != 'x' && opQuad3s != 'y' && opQuad4s != 'x' && opQuad4s != 'y')
                        {
                            direction.Y = 1;
                        }
                    }
                }

                if (direction.Y == -1)
                {
                    if ((int)((position.Y - speed.Y) / 15) < (int)(position.Y / 15))
                        position.Y -= position.Y % 15;
                    if (position.Y % 15 == 0)
                        direction.Y = 0;
                }
                
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (position.X % 15 == 0 && position.Y % 15 == 0)
                    {
                        if (pQuad1w != '1' && pQuad2w != '1' && opQuad1w != 'x' && opQuad1w != 'y' && opQuad2w != 'x' && opQuad2w != 'y')
                        {
                            direction.Y = -1;
                        }
                    }
                }

                if (direction.X == 1)
                {
                    if ((int)((position.X - speed.X) / 15) < (int)(position.X / 15))
                        position.X -= position.X % 15;

                    if (position.X % 15 == 0)
                        direction.X = 0;
                }

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (position.X % 15 == 0 && position.Y % 15 == 0)
                    {
                        if (pQuad2d != '1' && pQuad4d != '1' && pQuad2da != '1' && opQuad2d != 'x' && opQuad2d != 'y' && opQuad4d != 'x' && opQuad4d != 'y')
                        {
                            direction.X = 1;
                        }
                    }
                }

                if (direction.X == -1)
                {
                    if ((int)((position.X - speed.X) / 15) < (int)(position.X / 15))
                        position.X -= position.X % 15;

                    if (position.X % 15 == 0)
                        direction.X = 0;
                }
 
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (position.X % 15 == 0 && position.Y % 15 == 0)
                    {
                        if (pQuad1a != '1' && pQuad2a != '1' && opQuad2a != 'x' && opQuad2a != 'y' && opQuad1a != 'x' && opQuad1a != 'y')
                        {
                            direction.X = -1;
                        }
                    }
                }

                position += direction * speed;

                
                if (game.getBackground()[(int)(position.Y / 30)][(int)(position.X / 30)] == '=')
                {
                    //game.setTile(game.getBackground(), (int)(position.Y / 30), (int)(position.X / 30), '$');
                    //game.updateMapTiles(gameTime, game.getBackground(), (int)(position.Y / 30), (int)(position.X / 30), "=", 30);
                }
                 

            }
            base.Update(gameTime);
        }

        #endregion

        #region Methods

        public override void addAnimations(Texture2D tex)
        {
            addAnimation("1", tex, new Point(30, 30), new Point(1, 1), new Point(2, 1), new Point(2, 1), 16, Color.Pink);
            setAnimation("1");
        }

        #endregion

    }
}