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
        public static Texture2D livesTex;
        public static Texture2D livesBarTex;
        public static Texture2D scoreBarTex;
        public static Texture2D itemTex;

        public static SpriteFont spriteFont;
        
        public static void Load(ContentManager content)
        {
            wallTex = content.Load<Texture2D>(@"empty");
            emptyTex = content.Load<Texture2D>(@"empty(1)");
            pacmanTex = content.Load<Texture2D>(@"pacman");
            foodTex = content.Load<Texture2D>(@"empty(2)");
            ghostTex = content.Load<Texture2D>(@"Ghost");
            livesTex = content.Load<Texture2D>(@"heart");
            livesBarTex = content.Load<Texture2D>(@"Drawing");
            scoreBarTex = content.Load<Texture2D>(@"Score");
            itemTex = content.Load<Texture2D>(@"Itemm");
            spriteFont = content.Load<SpriteFont>(@"Font");
            
        }
    }
    
}
