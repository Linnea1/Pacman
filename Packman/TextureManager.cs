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
    class TextureManager
    {
        public static Texture2D pacmanTex;
        public TextureManager(ContentManager content)
        {
            pacmanTex = content.Load<Texture2D>(@"");
        }
    }
    
}
