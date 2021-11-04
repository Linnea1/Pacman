using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Packman
{
    class Pacman : GameObject
    {
        Vector2 destination;
        Vector2 direction;

        Texture2D walkingTex;

        float speed = 100.0f;
        bool moving = false;

        public Rectangle pacmanRect;
        public Pacman(Vector2 pos, Texture2D texture) : base(pos, texture)
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    ChangeDirection(new Vector2(1, 0));

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    ChangeDirection(new Vector2(0, 1));
                }
            }
            else
            {
                pos += direction * speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Vector2.Distance(pos, destination) < 1)
                {
                    pos = destination;
                    moving = false;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            base.Draw(spriteBatch);
        }

        
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = pos + direction * 40;


            if (!Game1.GetTileAtPos(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }
    }
}
