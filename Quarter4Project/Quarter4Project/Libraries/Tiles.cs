using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quarter4Project.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project
{
    class Tiles : SpriteAnimation
    {

        public Tiles(Texture2D TileTexture, Vector2 position, string hp, List<string> hp2, Point pos)
            : base(position)
        {
            addAnimations(TileTexture);
            if (hp == "P") hp = ".";

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

                    // Horizontal Wall End (Wall on the right, end on the left)
                    if (pos.X - 1 >= 0 && pos.Y - 1 >= 0 && pos.Y + 1 <= hp2.Count)
                    {
                        if (hp == "1" && hp2[pos.Y][pos.X + 1] == '0' && hp2[pos.Y][pos.X - 1] == '1')
                        {
                            hp = "E";
                        }
                    }

                    // Horizontal Wall End(Wall on the left, end on the right)
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
                        {
                            hp = "H";
                        }
                    }

                    if (pos.X + 1 < hp2[0].Length)
                    {
                        if (hp == "1" && hp2[pos.Y][pos.X + 1] == '1' && hp2[pos.Y][pos.X - 1] == '1')
                        {
                            hp = ",";
                        }
                    }
                }
            }
            else if (hp == ".")
            {
                if (pos.Y % 2 == 0)
                {
                    hp = "'";
                }

                if (pos.Y % 2 == 1 && pos.X % 2 == 0)
                {
                    hp = "`";
                }

                if (pos.Y % 2 == 0 && pos.X % 2 == 1)
                {
                    hp = "~";
                }
            }
            else if (hp == ";")
            {
                if (pos.X % 2 == 1 && pos.Y % 2 == 0)
                {
                    hp = ":";
                }
                else if (pos.X % 2 == 0 && pos.Y % 2 == 1)
                {
                    hp = ":";
                }
            }
            
            

            setAnimation(hp);
        }

        public override void addAnimations(Texture2D tex)
        {
            addAnimation(".", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(0, 183, 255));
            addAnimation("'", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(255, 183, 0));
            addAnimation("`", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(0, 183, 0));
            addAnimation("~", tex, new Point(30, 30), new Point(1, 1), new Point(1, 1), new Point(1, 1), 16, new Color(183, 0, 0));
            addAnimation(";", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, new Color(0, 0, 255));
            addAnimation(":", tex, new Point(30, 30), new Point(1, 1), new Point(0, 1), new Point(0, 1), 16, new Color(0, 255, 0));
            addAnimation("=", tex, new Point(30, 30), new Point(1, 1), new Point(2, 1), new Point(2, 1), 16, Color.White);
            addAnimation("1", tex, new Point(15, 15), new Point(1, 1), new Point(1, 0), new Point(0, 0), 16, Color.White);
            addAnimation("0", tex, new Point(15, 15), new Point(1, 1), new Point(5, 0), new Point(0, 0), 16, Color.Transparent);
            addAnimation("A", tex, new Point(15, 15), new Point(1, 1), new Point(2, 0), new Point(2, 0), 16, Color.White);
            addAnimation("B", tex, new Point(15, 15), new Point(1, 1), new Point(3, 0), new Point(3, 0), 16, Color.White);
            addAnimation("C", tex, new Point(15, 15), new Point(1, 1), new Point(5, 0), new Point(5, 0), 16, Color.White);
            addAnimation("D", tex, new Point(15, 15), new Point(1, 1), new Point(4, 0), new Point(4, 0), 16, Color.White);
            addAnimation("E", tex, new Point(15, 15), new Point(1, 1), new Point(6, 0), new Point(6, 0), 16, Color.White);
            addAnimation("F", tex, new Point(15, 15), new Point(1, 1), new Point(7, 0), new Point(7, 0), 16, Color.White);
            addAnimation("G", tex, new Point(15, 15), new Point(1, 1), new Point(9, 0), new Point(9, 0), 16, Color.White);
            addAnimation("H", tex, new Point(15, 15), new Point(1, 1), new Point(8, 0), new Point(8, 0), 16, Color.White);
            addAnimation(",", tex, new Point(15, 15), new Point(1, 1), new Point(0, 0), new Point(0, 0), 16, Color.White);
            setAnimation(".");
            base.addAnimations(tex);
        }

    }
}
