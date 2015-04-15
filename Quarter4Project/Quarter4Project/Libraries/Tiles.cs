using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quarter4Project.Libraries;
using Quarter4Project.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project
{
    /// <summary>
    /// Sets all our tiles to a texture.
    /// </summary>
    class Tiles : SpriteAnimation
    {

        #region Fields

        GameManager game;

        Random r;

        Color color;

        float timer;

        #endregion

        #region Initialization

        /// <summary>
        /// Gives all the tiles a texture.
        /// </summary>
        /// <param name="TileTexture">All of the games tile textures.</param>
        /// <param name="position">Position of the tile in pixels.</param>
        /// <param name="hp">String name of the tile.</param>
        /// <param name="hp2">List of tiles(background, walls, object) to check tiles around the tile being passed through for tile awareness.</param>
        /// <param name="pos">Position of the tile on grid map.</param>
        /// <param name="g">GameManager.cs</param>
        public Tiles(Texture2D TileTexture, Vector2 position, string hp, List<string> hp2, Point pos, GameManager g)
            : base(position)
        {
            game = g;
            addAnimations(TileTexture);
            r = new Random();
            color = new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
            // Sets the player position to a regular texture.
            if (hp == "P") hp = "=";

            // Wall Awareness - Corners will have corner texture, ends of walls will have end of wall texture, 3-way 4-way intersections will have textures
            if (hp == "1") {
                if (pos.X > 0 && pos.X + 1 <= hp2[0].Length - 1 && pos.Y + 1 > 0 && pos.Y + 1 <= hp2.Count - 1)
                {
                    if (pos.Y + 1 < hp2.Count && pos.X + 1 < hp2[0].Length && pos.X > 0 && pos.Y > 0)
                    {
                        // Top Right Corners
                        if (hp == "1" && hp2[pos.Y + 1][pos.X] == '1' && hp2[pos.Y][pos.X - 1] == '1')
                        {
                            if (hp2[pos.Y][pos.X + 1] == '.' || hp2[pos.Y][pos.X + 1] == '0')
                            {
                                if (hp2[pos.Y - 1][pos.X] != '1')
                                {
                                    hp = "A";
                                }
                            }
                        }

                        // Top Left Corners
                        if (hp == "1" && hp2[pos.Y + 1][pos.X] == '1' && hp2[pos.Y][pos.X + 1] == '1')
                        {
                            if (hp2[pos.Y][pos.X - 1] == '.' || hp2[pos.Y][pos.X - 1] == '0')
                            {
                                if (hp2[pos.Y - 1][pos.X] != '1')
                                {
                                    hp = "B";
                                }
                            }
                        }

                        // Bottom Right Corners
                        if (hp == "1" && hp2[pos.Y - 1][pos.X] == '1' && hp2[pos.Y][pos.X - 1] == '1')
                        {
                            if (hp2[pos.Y][pos.X + 1] == '.' || hp2[pos.Y][pos.X + 1] == '0')
                            {
                                if (hp2[pos.Y + 1][pos.X] != '1')
                                {
                                    hp = "C";
                                }
                            }
                        }

                        // Bottom Left Corners
                        if (hp == "1" && hp2[pos.Y - 1][pos.X] == '1' && hp2[pos.Y][pos.X + 1] == '1')
                        {
                            if (hp2[pos.Y][pos.X - 1] == '0')
                            {
                                if (hp2[pos.Y + 1][pos.X] != '1')
                                {
                                    hp = "D";
                                }
                            }
                        }
                    }

                    // Horizontal Wall End(Wall on the left, end on the right)
                    if (pos.X - 1 >= 0 && pos.Y - 1 >= 0 && pos.Y + 1 <= hp2.Count)
                    {
                        if (hp == "1" && hp2[pos.Y][pos.X + 1] == '0' && hp2[pos.Y][pos.X - 1] == '1')
                        {
                            hp = "E";
                        }
                    }

                    // Horizontal Wall End (Wall on the right, end on the left)
                    if (pos.X + 1 <= hp2[0].Length && pos.Y - 1 > 0 && pos.Y + 1 < hp2.Count)
                    {
                        if (hp == "1" && hp2[pos.Y][pos.X - 1] == '0' && hp2[pos.Y][pos.X + 1] == '1')
                        {
                            hp = "F";
                        }
                    }

                    // Vertical Wall End(Wall above, end below)
                    if (pos.Y - 1 >= 0)
                    {
                        if (hp == "1" && hp2[pos.Y + 1][pos.X] == '0' && hp2[pos.Y - 1][pos.X] == '1' && hp2[pos.Y][pos.X + 1] == '0' && hp2[pos.Y][pos.X - 1] == '0')
                        {
                            hp = "G";
                        }
                    }

                    // Vertical Wall End(Wall below, end above)
                    if (pos.Y + 1 <= hp2.Count)
                    {
                        if (hp == "1" && hp2[pos.Y - 1][pos.X] == '0' && hp2[pos.Y + 1][pos.X] == '1' && hp2[pos.Y][pos.X + 1] == '0' && hp2[pos.Y][pos.X - 1] == '0')
                            hp = "H";
                    }

                    // Vertical Wall
                    if (pos.X + 1 < hp2[0].Length)
                    {
                        if (hp == "1" && hp2[pos.Y][pos.X + 1] == '1' && hp2[pos.Y][pos.X - 1] == '1')
                            hp = ",";
                    }
                }
            }

            if (game.level == 1)
            {
                // Change the background texture to have a "fun" pattern
                if (hp == ".")
                {
                    if (pos.Y % 2 == 0)
                        hp = "'";
                }

                // Another background texture morphed to have a "fun" pattern
                if (hp == ";")
                {
                    if (pos.X % 2 == 1 && pos.Y % 2 == 0)
                        hp = ":";
                    else if (pos.X % 2 == 0 && pos.Y % 2 == 1)
                        hp = ":";
                }
            }

            setAnimation(hp);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates any tiles that might've been changed.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="hp">String name of the tile.</param>
        public void Update(GameTime gameTime, string hp)
        {
            setAnimation(hp);
        }

        /// <summary>
        /// Updates stuff.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (getAnimName() == "$")
            {
                if (timer > .25f)
                {
                    setWash(new Color((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
                    timer = 0;
                }
            }
            base.Update(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add our different textures.
        /// </summary>
        /// <param name="tex">Texture passed through that contains the different tiles.</param>
        public override void addAnimations(Texture2D tex)
        {
            // Background texture 1 - Striped Pattern Blue Color
            addAnimation(".", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(0, 183, 255));
            // Background texture 1 - Striped Pattern Orange Color
            addAnimation("'", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(255, 183, 0));
            // Background texture 1 - Striped Pattern Green Color
            addAnimation("`", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(0, 183, 0));
            // Background texture 1 - Striped Pattern Red Color
            addAnimation("~", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(183, 0, 0));
            // Background texture 2 - Square Pattern Blue Color
            addAnimation(";", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, new Color(0, 0, 255));
            // Background texture 2 - Square Pattern Green Color
            addAnimation(":", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, new Color(0, 255, 0));
            // Background texture 3 - Concrete pattern No Color
            addAnimation("=", tex, new Point(30, 30), new Point(1, 1), new Point(2, 1), new Point(2, 1), 16, Color.White);
            // Background texture 4 - Concrete pattern rainbow
            addAnimation("$", tex, new Point(30, 30), new Point(1, 1), new Point(2, 1), new Point(2, 1), 16, Color.White);

            // Wall spacing - No Pattern No Color
            addAnimation("0", tex, new Point(0, 0), new Point(1, 1), new Point(5, 0), new Point(0, 0), 16, Color.Transparent);
            // Wall texture 1 - Horizontal Wall No Color
            addAnimation("1", tex, new Point(15, 15), new Point(1, 1), new Point(1, 0), new Point(0, 0), 16, Color.White);
            // Wall texture 10 - Vertical Wall No Color
            addAnimation(",", tex, new Point(15, 15), new Point(1, 1), new Point(0, 0), new Point(0, 0), 16, Color.White);
            // Wall texture 2 - Top Right Corner No Color
            addAnimation("A", tex, new Point(15, 15), new Point(1, 1), new Point(2, 0), new Point(2, 0), 16, Color.White);
            // Wall texture 3 - Top Left Corner No Color
            addAnimation("B", tex, new Point(15, 15), new Point(1, 1), new Point(3, 0), new Point(3, 0), 16, Color.White);
            // Wall texture 4 - Bottom Right Corner No Color
            addAnimation("C", tex, new Point(15, 15), new Point(1, 1), new Point(5, 0), new Point(5, 0), 16, Color.White);
            // Wall texture 5 - Bottom Left Corner No Color
            addAnimation("D", tex, new Point(15, 15), new Point(1, 1), new Point(4, 0), new Point(4, 0), 16, Color.White);
            // Wall texture 6 - Horizontal Left Wall End
            addAnimation("E", tex, new Point(15, 15), new Point(1, 1), new Point(6, 0), new Point(6, 0), 16, Color.White);
            // Wall texture 7 - Horizontal Right Wall End
            addAnimation("F", tex, new Point(15, 15), new Point(1, 1), new Point(7, 0), new Point(7, 0), 16, Color.White);
            // Wall texture 8 - Vertical Bottom Wall End
            addAnimation("G", tex, new Point(15, 15), new Point(1, 1), new Point(9, 0), new Point(9, 0), 16, Color.White);
            // Wall texture 9 - Vertical Top Wall End
            addAnimation("H", tex, new Point(15, 15), new Point(1, 1), new Point(8, 0), new Point(8, 0), 16, Color.White);

            // Object Texture 1 - Plant
            addAnimation("x", tex, new Point(30, 30), new Point(1, 1), new Point(3, 1), new Point(3, 1), 16, Color.White);
            // Object Texture 2 - Round Wooden Table
            addAnimation("y", tex, new Point(30, 30), new Point(1, 1), new Point(4, 1), new Point(4, 1), 16, Color.White);

            addAnimation("?", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, Color.Transparent);

            addAnimation("l", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, Color.Transparent);

            // To Do: Add textures for 3-way and 4-way wall intersections

            setAnimation(".");
            base.addAnimations(tex);
        }

        #endregion

    }
}
