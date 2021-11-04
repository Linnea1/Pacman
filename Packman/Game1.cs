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

        List<string> strings;

        Pacman pacman;

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
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 680;
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
            while (!file.EndOfStream)
            {
                strings.Add(file.ReadLine());
            }
            file.Close();

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
                        tileArray[l, c] = new Tile( new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, true);
                    }
                    if (strings[c][l] == 'P')
                    {
                        tileArray[l, c] = new Tile(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex, true);
                        pacman = new Pacman(new Vector2(TextureManager.wallTex.Width * l, TextureManager.wallTex.Height * c), TextureManager.emptyTex);
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

            pacman.Update(gameTime);

            base.Update(gameTime);
        }

        //-----------------------------------------------------DRAW----------------------------------------------------------------//
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MistyRose);

            spriteBatch.Begin();
            foreach (Tile tile in tileArray)
            {
                tile.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public static bool GetTileAtPos(Vector2 tilePosition)
        {
            return tileArray[(int)tilePosition.X / TextureManager.wallTex.Width, (int)tilePosition.Y / TextureManager.wallTex.Height].wall;
        }
    }
}
