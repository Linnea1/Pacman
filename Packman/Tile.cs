using Microsoft.Xna.Framework;
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
        public bool path;

        public Tile(Vector2 pos, Texture2D texture,bool wall) : base(pos, texture)
        {
            this.wall = wall;
            
        }
        public Tile(Vector2 pos, Texture2D texture, bool wall, bool path) : base(pos, texture)
        {
            this.wall = wall;
            this.path = path;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
