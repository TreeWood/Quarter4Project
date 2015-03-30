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
    class Player : SpriteAnimation
    {

        GameManager game;
        public Vector2 direction = Vector2.Zero;

        public Player(Texture2D texture, Vector2 pos, GameManager g)
            : base(pos)
        {
            game = g;
            addAnimations(texture);
            rotationCenter = new Vector2(15, 15);
            position = pos;
            speed = new Vector2(3, 3);
        }

        public override void Update(GameTime gameTime)
        {


            if (keyboardState.IsKeyDown(Keys.S))
            {
                if (game.wall[(int)(position.Y / 15) + 2][(int)(position.X / 15)] != '1' && game.wall[(int)(position.Y / 15) + 2][(int)(position.X / 15) + 1] != '1')
                {
                    direction.Y = 1;
                    speed.Y = 2f;
                }
                else
                {
                    direction.Y = 0;
                    speed.Y = 0;
                }
            }
            else
            {
                if (direction.Y == 1)
                {
                    if ((int)((position.Y - 1) / 30) > (int)(position.Y / 30))
                    {
                        position.Y += position.Y % 15;
                        direction.Y = 0;
                    }
                    if (position.Y % 15 == 1 || position.Y % 15 == 0)
                    {
                        direction.Y = 0;
                    }
                }
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                if (game.wall[(int)(position.Y / 15)][(int)(position.X / 15)] != '1' && game.wall[(int)(position.Y / 15)][(int)(position.X / 15) + 1] != '1')
                {
                    direction.Y = -1;
                    rotation = 270 * (float)Math.PI / 180; 
                    speed.Y = 2f;
                }
                else
                {
                    direction.Y = 0;
                    speed.Y = 0;
                }
            }
            else
            {
                if (direction.Y == -1)
                {
                    if ((int)((position.Y - 1) / 30) > (int)(position.Y / 30))
                    {
                        position.Y -= position.Y % 15;
                        direction.Y = 0;
                    }

                    if (position.Y % 15 == 1 || position.Y % 15 == 0)
                    {
                        direction.Y = 0;
                    }
                }
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                if (game.wall[(int)(position.Y / 15)][(int)(position.X / 15) + 2] != '1' && game.wall[(int)(position.Y / 15) + 1][(int)(position.X / 15) + 2] != '1')
                {
                    direction.X = 1;
                    speed.X = 2f;
                }
                else
                {
                    direction.X = 0;
                    speed.X = 0;
                }
            }
            else
            {
                if (direction.X == 1)
                {
                    if ((int)((position.X - 1) / 15) > (int)(position.X / 15))
                    {
                        position.X += position.X % 15;
                        direction.X = 0;
                    }
                    if (position.X % 15 == 1 || position.X % 15 == 0)
                    {
                        direction.X = 0;
                    }
                }
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                if (game.wall[(int)(position.Y / 15)][(int)(position.X / 15)] != '1' && game.wall[(int)(position.Y / 15) + 1][(int)(position.X / 15)] != '1')
                {
                    direction.X = -1;
                    speed.X = 2f;
                }
                else
                {
                    direction.X = 0;
                    speed.X = 0;
                }
            }
            else
            {
                if (direction.X == -1)
                {
                    if ((int)((position.X - 1) / 15) > (int)(position.X / 15))
                    {
                        position.X -= position.X % 15;
                        direction.X = 0;
                    }
                    if (position.X % 15 == 1 || position.X % 15 == 0)
                    {
                        direction.X = 0;
                    }
                }
            }

            position += direction * speed;
            base.Update(gameTime);
        }

        public override void addAnimations(Texture2D tex)
        {
            addAnimation("1", tex, new Point(30, 30), new Point(1, 1), new Point(2, 1), new Point(2, 1), 16, Color.Pink);
            setAnimation("1");
        }

    }
}
