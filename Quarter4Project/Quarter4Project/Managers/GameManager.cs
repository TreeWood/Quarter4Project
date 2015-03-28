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
    /// 
    /// </summary>
    class GameManager : DrawableGameComponent
    {

        #region Fields

        Game1 game;
        SpriteBatch spriteBatch;

        SpriteFont Consolas;

        private List<ButtonFactory.Button> buttonList;
        private List<ButtonFactory.AnimatedButton.AddAnimation> animatedButtonListAnims;
        private List<ButtonFactory.AnimatedButton> animatedButtonList;

        Texture2D animatedButton;

        ButtonEvents bE;

        #endregion

        #region Initialization

        public GameManager(Game1 g)
            : base(g)
        {
            game = g;
        }

        public override void Initialize()
        {

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            buttonList = new List<ButtonFactory.Button>();

            animatedButtonListAnims = new List<ButtonFactory.AnimatedButton.AddAnimation>();

            animatedButtonList = new List<ButtonFactory.AnimatedButton>();

            bE = new ButtonEvents(game);
            
            base.Initialize();

            buttonList.Add(new ButtonFactory.Button(new Color(209, 48, 4), GraphicsDevice, new Vector2(5, 5), new Point(75, 50), Consolas, "Menu", new Color(250, 158, 130), new Color(146, 38, 0), new Vector2(2, 2), 1.0f, new Color(135, 29, 0), new Point(4, 4), 1001, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(0, 50, 150), GraphicsDevice, new Vector2(200, 200), new Point(100, 100), Consolas, "Hello", new Color(40, 170, 250), new Color(0, 0, 0), new Vector2(1, 1), 1.0f, new Color(0, 0, 50), new Point(6, 6), 1001, 0));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(1, 3), new Point(1, 3), 16, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(400, 350), 1001, 0));
            animatedButtonListAnims.Clear();
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(1, 3), new Point(1, 3), 16, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(100, 350), 1001, 0));

        }

        protected override void LoadContent()
        {

            Consolas = Game.Content.Load<SpriteFont>(@"Fonts/Consolas");

            animatedButton = Game.Content.Load<Texture2D>(@"Sprites/menus");

            base.LoadContent();
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {

            bE.Update(gameTime, buttonList);

            bE.Update(gameTime, animatedButtonList);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            bE.Draw(spriteBatch, gameTime, buttonList);

            bE.Draw(spriteBatch, gameTime, animatedButtonList);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}