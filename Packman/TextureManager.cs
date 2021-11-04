﻿using Microsoft.Xna.Framework;
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
        public static void Load(ContentManager content)
        {
            wallTex = content.Load<Texture2D>(@"empty");
            emptyTex = content.Load<Texture2D>(@"empty(1)");
        }
    }
    
}
