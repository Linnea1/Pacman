﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Packman
{
    class Tile:GameObject
    {
        

        Texture2D mapTex;

        public bool wall;

        public Tile(Vector2 pos, Texture2D texture) : base(pos, texture)
        {
            this.wall = wall;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapTex, pos, Color.White);
        }
    }
}
