using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Packman;

namespace Pacman
{
    class Ghost:GameObject
    {
        Vector2 destination;
        Vector2 direction;

        Texture2D texture;

        float speed = 100.0f;
        bool moving = false;
        bool isMoving = true;

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
            this.sheetSize = new Point(3, 1);
            this.frameSize = new Point(40, 40);
            this.currentFrame = new Point(0, 5);
            this.texture = texture;
        }
    }
}
