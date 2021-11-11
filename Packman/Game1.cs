using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Packman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        static Tile[,] tileArray;

        public static Random rand = new Random();

        public int foodCollected;
        public int score;
        public int lives=3;
        public float timer;

        List<string> strings;
        List<Food> foodList;
        List<Life> livesList;
        List<Item> itemList;

        Gamestate currentGameState=Gamestate.Start;
        KeyboardState kbd;

        Pacman pacman;
        Ghost ghost;

        enum Gamestate
        {
            Start,
            Play,
            GameOver,
            Won,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 520;
            graphics.PreferredBackBufferHeight = 550;
            graphics.ApplyChanges();
            base.Initialize();
        }
        //------------------------------------------------LoadContent-----------------------------------------------------------//
        protected override void LoadContent()
        {
            Load();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ReadFromFile("pacman.txt");
            StreamReader file = new StreamReader("pacman.txt");
            strings = new List<string>();
            foodList = new List<Food>();
            livesList = new List<Life>();
            itemList = new List<Item>();

            while (!file.EndOfStream)
            {
                strings.Add(file.ReadLine());
            }
            file.Close();

            for (int i = 0; i < 3; i++)
            {
                Life life = new Life( new Vector2(50 * i + 63, 480),TextureManager.livesTex);
                livesList.Add(life);
                if (lives <= 2)
                {
                    livesList.RemoveAt(i);
                }
            }

            tileArray = new Tile[strings[0].Length, strings.Count];
            for (int l = 0; l < tileArray.GetLength(0); l++)
            {
                for (int c = 0; c < tileArray.GetLength(1); c++)
                {
                    if (strings[c][l] == 'W')
                    {
                        tileArray[l, c] = new Tile( new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.wallTex, true);
                    }
                    if (strings[c][l] == '-')
                    {
                        tileArray[l, c] = new Tile( new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, false,true);
                        foodList.Add(new Food(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.foodTex));
                    }
                    if (strings[c][l] == 'P')
                    {
                        tileArray[l, c] = new Tile(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, false, true);
                         pacman = new Pacman(new Vector2(TextureManager.wallTex.Width * l + 20, TextureManager.wallTex.Height * c + 20), TextureManager.pacmanTex);
                    }
                    if (strings[c][l] == 'G')
                    {
                        tileArray[l, c] = new Tile(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, false,true);
                        foodList.Add(new Food(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.foodTex));
                        ghost = new Ghost(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.ghostTex);
                    }
                    if (strings[c][l] == 'I')
                    {
                        tileArray[l, c] = new Tile(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, false, true);
                        itemList.Add(new Item(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.itemTex));
                    }
                }
            }
        }
        public void ReadFromFile(string filename)
        {
        }
        public void Load()
        {
            TextureManager.Load(Content);
        }
        //---------------------------------------------------UPDATE--------------------------------------------------------------//
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            kbd = Keyboard.GetState();
            switch (currentGameState)
            {
                case Gamestate.Start:
                    if (kbd.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.Play;
                    }
                    break;
                case Gamestate.Play:
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    pacman.Update(gameTime);
                    ghost.Update(gameTime);
                    if (pacman.pacmanRect.Intersects(ghost.ghostRect) == true && timer > 3)
                    {
                        lives--;
                        livesList.RemoveAt(livesList.Count - 1);
                        timer = 0;

                    }
                    foreach (Food f in foodList)
                    {
                        f.Update(gameTime);
                        if (pacman.pacmanRect.Intersects(f.foodRect) == true)
                        {
                            if (f.isActive == true)
                            {
                                f.isActive = false;
                                f.isCollected = true;
                            }
                        }
                        if (f.isCollected == true)
                        {
                            foodCollected++;
                            f.isCollected = false;
                            break;
                        }
                        if (lives <= 0)
                        {
                            currentGameState = Gamestate.GameOver;
                            lives = 3;
                            score = 0;
                            foodCollected=0;
                            foreach (Food food in foodList)
                            {
                                food.isActive = true;
                            }
                            foreach (Item item in itemList)
                            {
                                item.itemActive = true;
                            }
                        }
                        if (foodCollected >= 59)
                        {
                            currentGameState = Gamestate.Won;
                            lives = 3;
                            score = 0;
                            foodCollected = 0;
                            foreach (Food food in foodList)
                            {
                                food.isActive = true;
                            }
                            foreach (Item item in itemList)
                            {
                                item.itemActive = true;
                            }
                        }
                    }
                    foreach (Item i in itemList)
                    {
                        i.Update(gameTime);
                        if (pacman.pacmanRect.Intersects(i.itemRect) == true)
                        {
                            if (i.itemActive == true)
                            {
                                i.itemActive = false;
                                i.itemCollected = true;
                            }
                        }
                        if (i.itemCollected == true)
                        {
                            score=score+10;
                            i.itemCollected = false;
                            break;
                        }
                    }
                    break;
                case Gamestate.GameOver:
                    if (kbd.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.Play;
                    }
                    break;
                case Gamestate.Won:
                    if (kbd.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = Gamestate.Play;
                    }
                    break;
            }
            
                base.Update(gameTime);
        }
        //-----------------------------------------------------DRAW----------------------------------------------------------------//
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MistyRose);

            spriteBatch.Begin();
            if (currentGameState == Gamestate.Start)
            {
                spriteBatch.DrawString(TextureManager.spriteFont, "Press 'ENTER' to start" , new Vector2(100, 100), new Color(215, 86, 98));
            }

            if (currentGameState == Gamestate.Play)
            {
                foreach (Tile tile in tileArray)
                {
                    tile.Draw(spriteBatch);
                }
                spriteBatch.Draw(TextureManager.livesBarTex, new Vector2(-40, 410), Color.White);
                spriteBatch.Draw(TextureManager.scoreBarTex, new Vector2(195, 410), Color.White);
                foreach (Life b in livesList)
                {
                    b.Draw(spriteBatch);
                }
                for (int i = 0; i < foodList.Count; i++)
                {
                    foodList[i].Draw(spriteBatch);
                }
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemList[i].Draw(spriteBatch);
                }
                ghost.Draw(spriteBatch);
                pacman.Draw(spriteBatch);
                spriteBatch.DrawString(TextureManager.spriteFont, "" + score, new Vector2(365, 490), new Color(215, 86, 98));
            }
            if (currentGameState == Gamestate.GameOver)
            {
                spriteBatch.DrawString(TextureManager.spriteFont, "Game Over! Press 'ENTER' to start again", new Vector2(10, 100), new Color(215, 86, 98));
            }
            if (currentGameState == Gamestate.Won)
            {
                spriteBatch.DrawString(TextureManager.spriteFont, "You won! Press 'ENTER' to start again", new Vector2(10, 100), new Color(215, 86, 98));
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public static bool GetTileAtPos(Vector2 tilePosition)
        {
            return tileArray[(int)tilePosition.X / TextureManager.wallTex.Width, (int)tilePosition.Y / TextureManager.wallTex.Height].wall;
        }
        public static bool GetPathAtPos(Vector2 tilePosition)
        {
            return tileArray[(int)tilePosition.X / TextureManager.wallTex.Width, (int)tilePosition.Y / TextureManager.wallTex.Height].path;
        }
    }
}
