using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    /// 
    /// </summary>
    class GameManager : DrawableGameComponent
    {

        #region Fields

        Game1 game;
        SpriteBatch spriteBatch;

        private List<string> map = new List<string>(),
                             wall = new List<string>();
        List<Tiles> mapTiles = new List<Tiles>();
        private int level = 1, prevlevel = 0;

        SpriteFont Consolas;

        private List<ButtonFactory.Button> buttonList;
        private List<ButtonFactory.AnimatedButton.AddAnimation> animatedButtonListAnims;
        private List<ButtonFactory.AnimatedButton> animatedButtonList;

        Texture2D animatedButton;

        ButtonEvents bE;

        KeyboardState keyBoardState, prevKeyBoardState;

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

            wall = wallGen();

            map = mapGen();

            buttonList = new List<ButtonFactory.Button>();

            animatedButtonListAnims = new List<ButtonFactory.AnimatedButton.AddAnimation>();

            animatedButtonList = new List<ButtonFactory.AnimatedButton>();

            bE = new ButtonEvents(game);
            
            base.Initialize();

            buttonList.Add(new ButtonFactory.Button(new Color(209, 48, 4), GraphicsDevice, new Vector2(5, 500), new Point(75, 50), Consolas, "Menu", new Color(250, 158, 130), new Color(146, 38, 0), new Vector2(2, 2), 1.0f, new Color(135, 29, 0), new Point(4, 4), 1001, 0));
            buttonList.Add(new ButtonFactory.Button(new Color(0, 50, 150), GraphicsDevice, new Vector2(200, 500), new Point(100, 100), Consolas, "Hello", new Color(40, 170, 250), new Color(0, 0, 0), new Vector2(1, 1), 1.0f, new Color(0, 0, 50), new Point(6, 6), 1001, 0));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(0, 3), new Point(1, 3), 180, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(400, 450), 1001, 0));
            animatedButtonListAnims.Clear();
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hello", animatedButton, new Point(100, 25), new Point(0, 3), new Point(0, 3), new Point(0, 3), 16, Color.White));
            animatedButtonListAnims.Add(new ButtonFactory.AnimatedButton.AddAnimation("Hover", animatedButton, new Point(100, 25), new Point(1, 3), new Point(1, 3), new Point(1, 3), 16, Color.White));
            animatedButtonList.Add(new ButtonFactory.AnimatedButton(animatedButtonListAnims, new Vector2(100, 450), 1001, 0));

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
            keyBoardState = Keyboard.GetState();

            bE.Update(gameTime, buttonList);

            bE.Update(gameTime, animatedButtonList);

            if (keyBoardState.IsKeyDown(Keys.D1) && prevKeyBoardState.IsKeyUp(Keys.D1))
            {
                mapTiles.Clear();
                level = 1;
                map = mapGen();
                wall = wallGen();
            }
            else if (keyBoardState.IsKeyDown(Keys.D2) && prevKeyBoardState.IsKeyUp(Keys.D2))
            {
                mapTiles.Clear();
                level = 2;
                map = mapGen();
                wall = wallGen();
            }

            if (level != prevlevel)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    for (int j = 0; j < map[0].Length; j++)
                    {
                        mapTiles.Add(new Tiles(Game.Content.Load<Texture2D>(@"Images/mapTiles"), new Vector2(j * 30, i * 30), map[i][j].ToString(), map, new Point(j, i)));
                    }
                }

                for (int i = 0; i < wall.Count; i++)
                {
                    for (int j = 0; j < wall[0].Length; j++)
                    {
                        mapTiles.Add(new Tiles(Game.Content.Load<Texture2D>(@"Images/mapTiles"), new Vector2(j * 15, i * 15), wall[i][j].ToString(), wall, new Point(j, i)));
                    }
                }
                prevlevel = level;
            }

            prevKeyBoardState = keyBoardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            
            foreach (Tiles t in mapTiles)
                t.Draw(gameTime, spriteBatch);
            
            bE.Draw(spriteBatch, gameTime, buttonList);

            bE.Draw(spriteBatch, gameTime, animatedButtonList);            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region Map Generation

        List<string> mapGen()
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

        List<string> wallGen()
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

        #endregion

    }
}