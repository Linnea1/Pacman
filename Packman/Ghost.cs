using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Packman;

namespace Packman
{
    class Ghost:GameObject
    {
        Vector2 destination;
        Vector2 direction;

        Texture2D texture;

        float speed;
        bool moving = false;

        double timeSinceLastFrames;
        double timeBetweenFrames;
        Point sheetSize;
        Point frameSize;
        Point currentFrame;

        public Rectangle pacmanRect;
        public Ghost(Vector2 pos, Texture2D texture) : base(pos, texture)
        {
            this.timeSinceLastFrames = 0;
            this.timeBetweenFrames = 0.1;
            this.sheetSize = new Point(1, 1);
            this.frameSize = new Point(38, 40);
            this.currentFrame = new Point(0, 5);
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (!moving)
            {
                direction = new Vector2(-1, 0);
            }
            else
            {
                pos += direction * speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;


            }
            if (Vector2.Distance(pos, destination) <= 1)
            {
                pos = destination;
                moving = false;

            }
            timeSinceLastFrames += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrames >= timeBetweenFrames)
            {
                timeSinceLastFrames -= timeBetweenFrames;
                currentFrame.X++;
                if (currentFrame.X > sheetSize.X)
                {
                    currentFrame.X = 0;


                    if (currentFrame.Y > sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle frame = new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, pos, frame, Color.White);
            
        }
        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = pos + direction * 40;
            destination = newDestination;

            if (Game1.GetPathAtPos(newDestination))
            {
                moving = true;
                speed = 40 * 2;

            }
            if (Game1.GetTileAtPos(newDestination))
            {
                moving = false;
            }
        }
    }
}
