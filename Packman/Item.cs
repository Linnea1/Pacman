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
    class Item : GameObject
    {
        Texture2D texture;
        public Rectangle itemRect;
        public bool itemActive;
        public bool itemCollected;

        public Item(Vector2 pos, Texture2D texture) : base(pos, texture)
        {
            this.texture = texture;
            itemActive = true;
            itemCollected = false;
        }

        public override void Update(GameTime gameTime)
        {
            itemRect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (itemActive == true)
            {
                spriteBatch.Draw(texture, pos, Color.White);
            }

        }
    }
}
