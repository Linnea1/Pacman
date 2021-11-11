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

        Random rnd = new Random();
        Texture2D texture;

        int num;
        float speed;
        bool moving = false;

        double timeSinceLastFrames;
        double timeBetweenFrames;
        Point sheetSize;
        Point frameSize;
        Point currentFrame;

        enum GhostDir
        {
            TurnLeft,
            TurnRight,
            GoUp,
            GoDown,
        }
        GhostDir currentState = GhostDir.TurnLeft;

        public Rectangle ghostRect;
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
            ghostRect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width/2, texture.Height);
            switch (currentState)
            {
                case GhostDir.TurnLeft:
                    
                    if (!moving)
                    {
                        ChangeDirection(new Vector2(-1, 0));

                        if (!moving)
                        {
                            currentState = GhostDir.GoDown;
                        }
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
                    break;
                    
                case GhostDir.GoDown:
                    if (!moving)
                    {
                        ChangeDirection(new Vector2(0, 1));
                        if (!moving)
                        {
                            currentState = GhostDir.TurnRight;
                        }
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
                    break;

                case GhostDir.TurnRight:
                    if (!moving)
                    {
                        ChangeDirection(new Vector2(1, 0));
                        if (!moving)
                        {
                            currentState = GhostDir.GoUp;
                        }
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
                    break;
                case GhostDir.GoUp:
                    if (!moving)
                    {
                        ChangeDirection(new Vector2(0, -1));
                        if (!moving)
                        {
                            currentState = GhostDir.TurnLeft;
                        }
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
                    break;


                   
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
