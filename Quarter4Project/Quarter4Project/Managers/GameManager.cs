using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.Entities;
using Quarter4Project.EventManagers;
using Quarter4Project.Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Quarter4Project.Managers
{
    /// <summary>
    /// This class handles everything in the game.
    /// </summary>
    class GameManager : DrawableGameComponent
    {

        #region Constants

        // Where is the mapTiles sprite sheet?
        private const string mapTilesDir = "Sprites/mapTiles";

        // Where is the animatedButtons sprite sheet?
        private const string animatedButtonsDir = "Sprites/menus";

        // Where is the health user interface?
        private const string healthUIDir = "Images/health";

        // Where is the consolas sprite font?
        private const string consolasDir = "Fonts/Consolas";

        #endregion

        #region Fields

        private Game1 game;
        private SpriteBatch spriteBatch;

        private KeyboardState keyboardState, prevKeyboardState;
        private MouseState mouseState, prevMouseState;

        private SpriteFont Consolas;

        private float health,
                      energy,
                      mana;
        private Texture2D circle,
                          healthUI,
                          black,
                          red,
                          light;

        RenderTarget2D nonlights;
        RenderTarget2D lights;
        Effect lighting;

        private List<string> background = new List<string>(),
                             wall = new List<string>(),
                             objects = new List<string>();

        private Texture2D mapTilesTexture;
        private List<Tiles> mapTiles = new List<Tiles>();

        public int level { get; private set; }
        public int prevLevel { get; private set; }

        private Player player;
        public Point playerGridPos { get; private set; }
        public Vector2 playerPosCenter { get; private set; }

        private List<ButtonFactory.Button> buttonList;
        private List<ButtonFactory.AnimatedButton.AddAnimation> animatedButtonListAnims;
        private List<ButtonFactory.AnimatedButton> animatedButtonList;
        private Texture2D animatedButton;
        private ButtonEvents bE;

        private Camera cam;

        #endregion

        #region Initialization

        public GameManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        /// <summary>
        /// Initialize all lists and values.
        /// </summary>
        public override void Initialize()
        {
            buttonList = new List<ButtonFactory.Button>();
            animatedButtonListAnims = new List<ButtonFactory.AnimatedButton.AddAnimation>();
            animatedButtonList = new List<ButtonFactory.AnimatedButton>();
            bE = new ButtonEvents(game);

            cam = new Camera();
            
            base.Initialize();

            // Sets the current level.
            level = 1;
            prevLevel = 0;

            // Generates the map.
            wall = wallGen();
            background = backgroundGen();
            objects = objectGen();

            // Adds a new player.
            player = new Player(Game.Content.Load<Texture2D>(@"Sprites/mapTiles"), new Vector2(0, 0), this);

            // Sets the camera position to the player position.
            cam.pos = new Vector2(player.getPos().X, player.getPos().Y);

            // Add the regular buttons to the buttons list.
            buttonList.Add(new ButtonFactory.Button(new Color(209, 48, 4), GraphicsDevice, new Vector2(5, 500), new Point(75, 50), Consolas, "Menu", new Color(250, 158, 130), new Color(146, 38, 0), new Vector2(2, 2), 1.0f, new Color(135, 29, 0), new Point(4, 4), 1001, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(0, 50, 150), GraphicsDevice, new Vector2(200, 500), new Point(100, 100), Consolas, "Hello", new Color(40, 170, 250), new Color(0, 0, 0), new Vector2(1, 1), 1.0f, new Color(0, 0, 50), new Point(6, 6), 1001, 0));

            // Add the animated buttons to the animated buttons list.
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(0, 3), new Point(1, 3), 180, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(400, 450), 1001, 0));
            animatedButtonListAnims.Clear();

            /* *** Note: Button animations list must be cleared before you can begin creating a new animated button using a spritesheet. *** */

            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(1, 3), new Point(1, 3), 16, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(100, 450), 1001, 0));
            animatedButtonListAnims.Clear();
            GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;
        }
        
        /// <summary>
        ///  Load all Fonts/Images/Textures/Sprites
        /// </summary>
        protected override void LoadContent()
        {
            // Creates a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Load all Fonts/Images/Textures/Sprites
            Consolas = Game.Content.Load<SpriteFont>(consolasDir);
            healthUI = Game.Content.Load<Texture2D>(healthUIDir);
            circle = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            circle.SetData<Color>(new Color[] { new Color(255, 255, 255) });
            black = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            black.SetData<Color>(new Color[] { new Color(0, 0, 0) });
            red = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            red.SetData<Color>(new Color[] { new Color(255, 0, 0) });
            animatedButton = Game.Content.Load<Texture2D>(animatedButtonsDir);
            mapTilesTexture = Game.Content.Load<Texture2D>(mapTilesDir);
            light = Game.Content.Load<Texture2D>(@"Images/light");
            lighting = Game.Content.Load<Effect>(@"Effects/lighting");
            nonlights = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            lights = new RenderTarget2D(GraphicsDevice, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Gets the users input and changes the values associated with that input.
        /// </summary>
        public void updateUserInput()
        {
            // Changes the user interface based on the - and + buttons.
            if (keyboardState.IsKeyDown(Keys.OemMinus))
            {
                mana -= .0001f;
                energy -= .00005f;
                health -= 1.0f;

                if (mana <= 0.0f)
                    mana = 0.0f;
                if (energy <= 0.0f)
                    energy = 0.0f;
                if (health <= 0.0f)
                    health = 0.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.OemPlus))
            {
                mana += .0001f;
                energy += .00005f;
                health += 1.0f;

                if (mana >= .0063f)
                    mana = .0063f;
                if (energy >= .0063f)
                    energy = .0063f;
                if (health >= 50)
                    health = 50;
            }

            // Enables the levels to change by pressing 1 & 2.
            if (keyboardState.IsKeyDown(Keys.D1) && prevKeyboardState.IsKeyUp(Keys.D1))
            {
                mapTiles.Clear();
                level = 1;
                background = backgroundGen();
                wall = wallGen();
                objects = objectGen();
            }
            else if (keyboardState.IsKeyDown(Keys.D2) && prevKeyboardState.IsKeyUp(Keys.D2))
            {
                mapTiles.Clear();
                level = 2;
                background = backgroundGen();
                wall = wallGen();
                objects = objectGen();
            }

            // Allows the player to look around the map using LeftShift.
            if (keyboardState.IsKeyDown(Keys.LeftShift))
            {
                if (prevKeyboardState.IsKeyUp(Keys.LeftShift))
                {
                    Mouse.SetPosition((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2));
                    game.IsMouseVisible = false;
                }
                cam.pos = new Vector2((player.getPos().X - (GraphicsDevice.Viewport.Width / 2)) - (mouseState.X / 5), (player.getPos().Y - (GraphicsDevice.Viewport.Height / 2)) + (mouseState.Y / 5));
            }
            else
            {
                cam.pos = new Vector2((player.getPos().X - (GraphicsDevice.Viewport.Width / 2)), (player.getPos().Y - (GraphicsDevice.Viewport.Height / 2)));
                game.IsMouseVisible = true;
            }
        }

        /// <summary>
        /// Updates the level if it has changed.
        /// </summary>
        public void updateLevel()
        {
            // Update the level if it has changed.
            if (level != prevLevel)
            {
                // Load and add all of our background tiles to mapTiles.
                for (int i = 0; i < background.Count; i++)
                {
                    for (int j = 0; j < background[0].Length; j++)
                    {
                        if (background[i][j] == 'P')
                        {
                            player.setPos(new Vector2(j * 30, i * 30));
                            background[i] = background[i].Substring(0, j) + '=' + background[i].Substring(j + 1);
                        }

                        if (background[i][j] != '0')
                        {
                            mapTiles.Add(new Tiles(mapTilesTexture, new Vector2(j * 30, i * 30), background[i][j].ToString(), background, new Point(j, i), this));
                        }
                    }
                }

                // Load and add all of our wall tiles to mapTiles.
                for (int i = 0; i < wall.Count; i++)
                {
                    for (int j = 0; j < wall[0].Length; j++)
                    {
                        if (wall[i][j] != '0')
                        {
                            mapTiles.Add(new Tiles(mapTilesTexture, new Vector2(j * 15, i * 15), wall[i][j].ToString(), wall, new Point(j, i), this));
                        }
                    }
                }

                // Load and add all of our wall tiles to mapTiles.
                for (int i = 0; i < objects.Count; i++)
                {
                    for (int j = 0; j < objects[0].Length; j++)
                    {
                        if (objects[i][j] != '0')
                        {
                            mapTiles.Add(new Tiles(mapTilesTexture, new Vector2(j * 30, i * 30), objects[i][j].ToString(), objects, new Point(j, i), this));
                        }
                    }
                }

                // Set the previous level.
                prevLevel = level;
            }
        }

        /// <summary>
        /// Updates the player, map, user inputs, and buttons.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Get and set the users input.
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // Creates an rotation angle from mana and energy.
            mana = mana % (MathHelper.Pi * 2);
            energy = energy % (MathHelper.Pi * 2);

            // Update all of our buttons/animated buttons.
            bE.Update(gameTime, buttonList);
            bE.Update(gameTime, animatedButtonList);

            // Update the player, player position.
            player.Update(gameTime);
            playerPosCenter = player.getPos() + player.rotationCenter;
            playerGridPos = new Point((int)(player.getPos().X / 30), (int)(player.getPos().Y / 30));

            // Updates all variables based on the users input.
            updateUserInput();

            // Updates the level if it has changed.
            updateLevel();

            //Updates Tiles
            for (int i = 0; i < mapTiles.Count; i++)
            {
                if (mapTiles[i].getAnimName() == "$")
                {
                    mapTiles[i].Update(gameTime);
                }
            }

            // Sets previous user input.
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the map, player, and user interface.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of all timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(nonlights);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.getMatrix());

            // Draw all tiles.
            foreach (Tiles t in mapTiles)
                t.Draw(gameTime, spriteBatch);
            // Draw player.
            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.SetRenderTarget(lights);
            GraphicsDevice.Clear(new Color(30, 30, 30));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, null, null, null, cam.getMatrix());

            // Lighting for wall tiles.
            for (int i = 0; i < wall.Count; i++)
            {
                for (int j = 0; j < wall[0].Length; j++)
                {
                    if (wall[i][j] == '1')
                    {
                        //spriteBatch.Draw(light, new Rectangle((j * 15) - 12, (i * 15) - 12, 40, 40), Color.White);
                    }
                }
            }

            // Lighting for background tiles.
            for (int i = 0; i < background.Count; i++)
            {
                for (int j = 0; j < background[0].Length; j++)
                {
                    if (background[i][j] == '$')
                    {
                        for (int x = 0; x < mapTiles.Count; x++)
                        {
                            if (mapTiles[x].getAnimName() == background[i][j].ToString())
                            {
                                if ((int)(mapTiles[x].getPos().X / 30) == j && (int)(mapTiles[x].getPos().Y / 30) == i)
                                {
                                    spriteBatch.Draw(light, new Rectangle((j * 30) - 85, (i * 30) - 85, 200, 200), mapTiles[x].currentSprite.color);
                                }
                            }
                        }
                    }
                }
            }

            // Lighting for object tiles
            for (int i = 0; i < objects.Count; i++)
            {
                for (int j = 0; j < objects[0].Length; j++)
                {
                    if (objects[i][j] == 'l')
                    {
                        for (int x = 0; x < mapTiles.Count; x++)
                        {
                            if (mapTiles[x].getAnimName() == objects[i][j].ToString())
                            {
                                if ((int)(mapTiles[x].getPos().X / 30) == j && (int)(mapTiles[x].getPos().Y / 30) == i)
                                {
                                    spriteBatch.Draw(light, new Rectangle((j * 30) - 235, (i * 30) - 235, 500, 500), Color.White);
                                }
                            }
                        }
                    }

                    if (objects[i][j] == '?')
                    {
                        for (int x = 0; x < mapTiles.Count; x++)
                        {
                            if (mapTiles[x].getAnimName() == objects[i][j].ToString())
                            {
                                if ((int)(mapTiles[x].getPos().X / 30) == j && (int)(mapTiles[x].getPos().Y / 30) == i)
                                {
                                    spriteBatch.Draw(light, new Rectangle((j * 30) - 235, (i * 30) - 235, 500, 500), Color.White);
                                }
                            }
                        }
                    }
                }
            }

            // Lighting for the player.
            //spriteBatch.Draw(light, new Rectangle((int)player.getPos().X - 85, (int)player.getPos().Y - 85, 200, 200), Color.White);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(new Color(0, 0, 0, .1f));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            lighting.Parameters["lights"].SetValue(lights);
            lighting.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(nonlights, new Vector2(0, 0), new Color(0, 0, 0, .8f));

            spriteBatch.End();
            spriteBatch.Begin();

            // Draw buttons.
            bE.Draw(spriteBatch, gameTime, buttonList);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            // Draw energy circle if energy is greater than 0.
            if (energy > 0.0f)
            {
                for (int i = 0; i < 1000; i++)
                {
                    spriteBatch.Draw(circle, new Rectangle(60, 60, 1, 47), new Rectangle(0, 0, 1, 1), new Color(0, 100, 0, .8f), energy * (i), Vector2.Zero, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(circle, new Rectangle(60, 60, 1, 45), new Rectangle(0, 0, 1, 1), new Color(200, 255, 0, .8f), energy * (i), Vector2.Zero, SpriteEffects.None, 0.0f);
                }
            }

            // Draw mana circle if mana is greater than 0.
            if (mana > 0.0f)
            {
                for (int i = 0; i < 1000; i++)
                {
                    spriteBatch.Draw(circle, new Rectangle(60, 60, 1, 42), new Rectangle(0, 0, 1, 1), new Color(0, 0, 100, .8f), mana * (i), Vector2.Zero, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(circle, new Rectangle(60, 60, 1, 40), new Rectangle(0, 0, 1, 1), new Color(0, 100, 255, .8f), mana * (i), Vector2.Zero, SpriteEffects.None, 0.0f);
                }
            }

            // Draw health and visual user interface additives.
            spriteBatch.Draw(black, new Rectangle(35, 35, 50, 50), Color.White);
            spriteBatch.Draw(red, new Rectangle(85, 85, 50, (int)health), new Rectangle(0, 0, 1, 1), Color.White, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            spriteBatch.Draw(healthUI, new Rectangle(20, 20, 80, 80), Color.White);

            // Debug
            spriteBatch.DrawString(Consolas, (player.getPos().Y % 15).ToString(), new Vector2(10, 20), Color.Cyan);
            spriteBatch.DrawString(Consolas, (player.getPos().X % 15).ToString(), new Vector2(10, 40), Color.Cyan);
            spriteBatch.DrawString(Consolas, (player.direction.X % 15).ToString(), new Vector2(10, 60), Color.Cyan);
            spriteBatch.DrawString(Consolas, (player.direction.Y % 15).ToString(), new Vector2(10, 80), Color.Cyan);
            spriteBatch.DrawString(Consolas, "FPS: " + (Math.Round(1 / (float)gameTime.ElapsedGameTime.TotalSeconds).ToString()), new Vector2(10, 100), Color.Cyan);
            spriteBatch.DrawString(Consolas, "X: " + (player.getPos().X % 15).ToString(), new Vector2(10, 120), Color.Cyan);
            spriteBatch.DrawString(Consolas, "Y: " + (player.getPos().Y % 15).ToString(), new Vector2(10, 140), Color.Cyan);
            spriteBatch.DrawString(Consolas, "X: " + (player.getPos().X / 30).ToString(), new Vector2(10, 160), Color.Cyan);
            spriteBatch.DrawString(Consolas, "Y: " + (player.getPos().Y / 30).ToString(), new Vector2(10, 180), Color.Cyan);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Map Generation

        /// <summary>
        /// Gets the map background.
        /// </summary>
        /// <returns>Returns a string list of background tile information from the background file.</returns>
        private List<string> backgroundGen()
        {
            List<string> tmpList = new List<string>();
            using (StreamReader r = new StreamReader(@"Content/Maps/" + level + "/Background/" + level + ".txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    tmpList.Add(line);
                }
            }
            return tmpList;
        }

        /// <summary>
        /// Gets the map walls.
        /// </summary>
        /// <returns>Returns a string list of wall tile information from the wall file.</returns>
        private List<string> wallGen()
        {
            List<string> tmpList = new List<string>();
            using (StreamReader r = new StreamReader(@"Content/Maps/" + level + "/Walls/" + level + ".txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    tmpList.Add(line);
                }
            }

            return tmpList;
        }

        /// <summary>
        /// Gets the map objects.
        /// </summary>
        /// <returns>Returns a string list of object tile information from the object file.</returns>
        private List<string> objectGen()
        {
            List<string> tmpList = new List<string>();
            using (StreamReader r = new StreamReader(@"Content/Maps/" + level + "/Objects/" + level + ".txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    tmpList.Add(line);
                }
            }

            return tmpList;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets a new tile.
        /// </summary>
        /// <param name="tileList">Specified tile list such as background, wall or objects.</param>
        /// <param name="y">Y position of the tile to be changed.</param>
        /// <param name="x">X position of the tile to be changed.</param>
        /// <param name="replace">Character that is to replace other tile.</param>
        /// <returns>Returns the changed list.</returns>
        public List<string> setTile(List<string> tileList, int y, int x, char replace)
        {
            List<string> tmpList = new List<string>();

            tmpList = tileList;
            for (int i = 0; i < tmpList.Count; i++)
            {
                for (int j = 0; j < tmpList[0].Length; j++)
                {
                    if (i == y && j == x)
                    {
                        tmpList[i] = tmpList[i].Substring(0, j) + replace + tmpList[i].Substring(j + 1);
                    }
                }
            }

            return tmpList;
        }

        /// <summary>
        /// Updates the mapTiles.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <param name="y">Y position of the changed tile.</param>
        /// <param name="x">X position of the changed tile.</param>
        /// <param name="toChange">Character that is to be changed.</param>
        public void updateMapTiles(GameTime gameTime, List<string> layer, int y, int x, string toChange, int tileSize)
        {
            for (int i = 0; i < mapTiles.Count; i++)
            {
                if (mapTiles[i].getAnimName() == toChange && (mapTiles[i].getPos().X / tileSize) == x && (mapTiles[i].getPos().Y / tileSize) == y)
                {
                    mapTiles[i].Update(gameTime, layer[y][x].ToString());
                }
            }
        }

        /// <summary>
        /// Gets the wall list.
        /// </summary>
        /// <returns>Returns the string list of the walls.</returns>
        public List<string> getWall()
        {
            return wall;
        }

        /// <summary>
        /// Gets the objects list.
        /// </summary>
        /// <returns>Returns the string list of the objects.</returns>
        public List<string> getObjects()
        {
            return objects;
        }

        /// <summary>
        /// Gets the background list.
        /// </summary>
        /// <returns>Returns the string list of the background.</returns>
        public List<string> getBackground()
        {
            return background;
        }

        /// <summary>
        /// Gets a random double based off the parameters
        /// </summary>
        /// <param name="minimum">Minimum amount the random will return.</param>
        /// <param name="maximum">Maximum amount the random will return.</param>
        /// <returns>Returns a double between the minimum and maximum parameters</returns>
        public double NextDouble(double minimum, double maximum, Random r)
        {
            return r.NextDouble() * (maximum - minimum) + minimum;
        }


        #endregion

    }
}