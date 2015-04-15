using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Quarter4Project.EventManagers;
using Quarter4Project.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quarter4Project.Managers
{
    /// <summary>
    /// This class handles everything in the menu.
    /// </summary>
    class MenuManager : DrawableGameComponent
    {

        #region Constants

        // Where is the image located for the background?
        private const string menuBackgroundDir = "Backgrounds/starBG";

        // Where is the sprite sheet located for the menu buttons?
        private const string menuButtonsDir = "Sprites/menuTexture";

        // Where is the SpriteFont located for all the text?
        private const string consolasFontDir = "Fonts/Consolas";

        #endregion

        #region Fields

        private Game1 game;
        private SpriteBatch spriteBatch;

        private KeyboardState keyboardState, prevKeyboardState;
        private MouseState mouseState, prevMouseState;
        private Point mousePos, prevMousePos;

        private SpriteFont Consolas;
        private Texture2D backgroundImage,
                          sampleButtonTexture,
                          menuButtons,
                          gridOverlay;

        private List<ButtonFactory.Button> buttonList;
        private List<ButtonFactory.AnimatedButton> animatedButtonList;
        private List<ButtonFactory.AnimatedButton.AddAnimation> buttonAnimationsList;
        private ButtonEvents bE;

        private List<WindowFactory.Window> windowList;
        private WindowEvents wE;

        private float timer = 0;

        private Vector2[] moveMag = { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        private Vector2[] move = { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };

        private Camera cam, cam2, cam3, cam4, cam5, cam6, cam7, cam8, cam9;

        private Random r;



        #endregion

        #region Initialization

        public MenuManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        /// <summary>
        /// Initialize all of the lists and values and add items to any lists.
        /// </summary>
        public override void Initialize()
        {
            buttonList = new List<ButtonFactory.Button>();
            animatedButtonList = new List<ButtonFactory.AnimatedButton>();
            buttonAnimationsList = new List<ButtonFactory.AnimatedButton.AddAnimation>();
            bE = new ButtonEvents(game);

            windowList = new List<WindowFactory.Window>();
            wE = new WindowEvents(game);

            cam = new Camera();
            cam2 = new Camera();
            cam3 = new Camera();
            cam4 = new Camera();
            cam5 = new Camera();
            cam6 = new Camera();
            cam7 = new Camera();
            cam8 = new Camera();
            cam9 = new Camera();

            r = new Random();

            base.Initialize();

            // Add the animated buttons to the animated buttons list.
            buttonAnimationsList.Add(new ButtonFactory.AnimatedButton.AddAnimation("Start", menuButtons, new Point(660, 69), new Point(0, 0), new Point(0, 0), new Point(0, 0), 222, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(buttonAnimationsList, new Vector2(115, 150), 2000, 0));
            buttonAnimationsList.Clear();

            /* *** Note: Button animations list must be cleared before you can begin creating a new animated button using a spritesheet. *** */

            buttonAnimationsList.Add(new ButtonFactory.AnimatedButton.AddAnimation("Quit", menuButtons, new Point(660, 69), new Point(0, 0), new Point(0, 1), new Point(0, 1), 222, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(buttonAnimationsList, new Vector2(115, 300), 1000, 0));
            buttonAnimationsList.Clear();

            // Set random values for the magnitude of the camera movement.
            for (int i = 0; i < moveMag.Length; i++)
            {
                moveMag[i].X = (float)NextDouble(-.07, .07);
                moveMag[i].Y = (float)NextDouble(-.07, .07);
            }

            windowList.Add(new WindowFactory.Window(new Color(255, 255, 255), GraphicsDevice, new Point(500, 500), new Point(100, 100)));

        }

        /// <summary>
        /// Load all of the fonts/backgrounds/images/sprites.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Load all of the fonts/backgrounds/images/sprites
            Consolas = Game.Content.Load<SpriteFont>(consolasFontDir);
            backgroundImage = Game.Content.Load<Texture2D>(menuBackgroundDir);
            sampleButtonTexture = Game.Content.Load<Texture2D>(@"Images/btnTex");
            gridOverlay = Game.Content.Load<Texture2D>(@"Images/gridOverlay");
            menuButtons = Game.Content.Load<Texture2D>(menuButtonsDir);

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        /// <summary>
        /// Updates the camera movement and the users input.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot for timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Get the users input.
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X - (GraphicsDevice.Viewport.Width / 2), mouseState.Y - (GraphicsDevice.Viewport.Height / 2));

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Based on the timer change the move variable which will effect camera movement.
            for (int i = 0; i < 7; i++)
            {

                if (timer < 2)
                {
                    move[i].X += (float)moveMag[i].X;
                }
                else if (timer < 4)
                {
                    move[i].Y += (float)moveMag[i].Y;
                    move[i].X += (float)moveMag[i].X;
                }
                else if (timer < 6)
                {
                    move[i].X -= (float)moveMag[i].X;
                }
                else if (timer < 8)
                {
                    move[i].Y -= (float)moveMag[i].Y;
                    move[i].X -= (float)moveMag[i].X;
                }
                else
                {
                    if (moveMag[i].X < 0)
                    {
                        moveMag[i].X = (float)NextDouble(.01, .07);
                    }
                    else if (moveMag[i].X > 0)
                    {
                        moveMag[i].X = (float)NextDouble(-.01, -.07);
                    }
                    else if (moveMag[i].Y < 0)
                    {
                        moveMag[i].Y = (float)NextDouble(.01, .07);
                    }
                    else if (moveMag[i].Y > 0)
                    {
                        moveMag[i].Y = (float)NextDouble(-.01, -.07);
                    }
                }
            }

            // Reset the timer.
            if (timer > 8)
            {
                timer = 0;
            }

            // Update the buttons.
            bE.Update(gameTime, animatedButtonList);

            // Update the windows.
            wE.Update(gameTime, windowList);

            // Change camera positioning based on the move variable.
            cam.pos.X = mousePos.X / 25 - move[0].X;
            cam.pos.Y = mousePos.Y / 25 - move[0].Y;
            cam2.pos.X = mousePos.X / 20 - move[1].X / .1f;
            cam2.pos.Y = mousePos.Y / 20 - move[1].Y / .1f;
            cam3.pos.X = mousePos.X / 30 - move[2].X / .2f;
            cam3.pos.Y = mousePos.Y / 30 - move[2].Y / .2f;
            cam4.pos.X = mousePos.X / 27 - move[3].X / .15f;
            cam4.pos.Y = mousePos.Y / 27 - move[3].Y / .15f;
            cam5.pos.X = mousePos.X / 22 - move[4].X / .25f;
            cam5.pos.Y = mousePos.Y / 22 - move[4].Y / .25f;
            cam6.pos.X = mousePos.X / 17 - move[5].X / .33f;
            cam6.pos.Y = mousePos.Y / 17 - move[5].Y / .33f;
            cam7.pos.X = mousePos.X / 55 - move[6].X / .9f;
            cam7.pos.Y = mousePos.Y / 55 - move[6].Y / .9f;

            // Set the previous user input values.
            prevKeyboardState = keyboardState;
            prevMousePos = mousePos;
            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the background image, all buttons, and other assets within' all of the camera views.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam7.getMatrix());

            spriteBatch.Draw(backgroundImage, new Rectangle(-50, -50, 1080, 760), Color.White);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam2.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam3.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam4.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam5.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList); 

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, cam6.getMatrix());

            spriteBatch.Draw(gridOverlay, new Rectangle(-50, -50, 1080, 760), Color.White);
            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();
            spriteBatch.Begin();

            //wE.Draw(spriteBatch, gameTime, windowList);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a random double based off the parameters
        /// </summary>
        /// <param name="minimum">Minimum amount the random will return.</param>
        /// <param name="maximum">Maximum amount the random will return.</param>
        /// <returns>Returns a double between the minimum and maximum parameters</returns>
        public double NextDouble(double minimum, double maximum)
        {
            return r.NextDouble() * (maximum - minimum) + minimum;
        }

        #endregion

    }
}
