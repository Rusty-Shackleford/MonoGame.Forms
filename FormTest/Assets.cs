using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTest
{
    public static class Assets
    {

        public static Texture2D Button { get; private set; }
        public static Texture2D ButtonHover { get; private set; }
        public static Texture2D ButtonPressed { get; private set; }

        public static Texture2D Panel { get; private set; }
        public static Texture2D Cursor { get; private set; }

        public static BitmapFont Crux12 { get; private set; }
        public static BitmapFont MineCraftia12 { get; private set; }
        public static BitmapFont MineCraftia10 { get; private set; }
        // Azure Blue Highlight:   21, 132, 221
        // Background Very Dark Grey: 30, 30, 30
        // Background Dark Grey: 40, 40, 40 

        public static void LoadContent(ContentManager content)
        {
            Button = content.Load<Texture2D>(@"UI/Button");
            ButtonHover = content.Load<Texture2D>(@"UI/ButtonHover");
            ButtonPressed = content.Load<Texture2D>(@"UI/ButtonPressed");

            Panel = content.Load<Texture2D>(@"UI/Panel");
            Cursor = content.Load<Texture2D>(@"UI/Cursor");

            Crux12 = content.Load<BitmapFont>(@"Fonts/CoderCrux_12");
            MineCraftia12 = content.Load<BitmapFont>(@"Fonts/MineCraftia_12");
            MineCraftia10 = content.Load<BitmapFont>(@"Fonts/MineCraftia_10");
        }

    }
}
