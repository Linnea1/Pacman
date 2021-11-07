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
    class Food : GameObject
    {
        Texture2D texture;
        public Rectangle foodRect;

        public Food(Vector2 pos, Texture2D texture) : base(pos, texture)
        {
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            foodRect = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
            
        }
    }
}
