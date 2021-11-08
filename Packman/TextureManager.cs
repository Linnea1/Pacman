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
    public static class TextureManager
    {
        public static Texture2D emptyTex;
        public static Texture2D wallTex;
        public static Texture2D pacmanTex;
        public static Texture2D foodTex;
        public static Texture2D ghostTex;
        public static void Load(ContentManager content)
        {
            wallTex = content.Load<Texture2D>(@"empty");
            emptyTex = content.Load<Texture2D>(@"empty(1)");
            pacmanTex = content.Load<Texture2D>(@"pacman");
            foodTex = content.Load<Texture2D>(@"empty(2)");
            ghostTex = content.Load<Texture2D>(@"Ghost");
        }
    }
    
}
