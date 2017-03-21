using Microsoft.Xna.Framework;
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

        public static Texture2D ArrowUp { get; private set; }
        public static Texture2D ArrowDown { get; private set; }

        public static Texture2D Panel { get; private set; }
        public static Texture2D Terminal { get; private set; }
        public static Texture2D Cursor { get; private set; }
        public static Texture2D DevConsole { get; private set; }

        public static BitmapFont Crux12 { get; private set; }
        public static BitmapFont MineCraftia12 { get; private set; }
        public static BitmapFont MineCraftia10 { get; private set; }

        public static BitmapFont SteelFlight8 { get; private set; }
        public static BitmapFont SteelFlight16 { get; private set; }

        public static BitmapFont Plumbis11 { get; private set; }

        // Azure Blue Highlight:   21, 132, 221
        // Background Very Dark Grey: 30, 30, 30
        // Background Dark Grey: 40, 40, 40 
        public static Color ScrollThumb = new Color(104, 104, 104);
        public static Color ScrollBar = new Color(62, 62, 66);
        public static Color ScrollThumbHover = new Color(158, 158, 158);

        public static void LoadContent(ContentManager content)
        {
            Button = content.Load<Texture2D>(@"UI/Button");
            ButtonHover = content.Load<Texture2D>(@"UI/ButtonHover");
            ButtonPressed = content.Load<Texture2D>(@"UI/ButtonPressed");

            ArrowUp = content.Load<Texture2D>(@"UI/ArrowUp");
            ArrowDown = content.Load<Texture2D>(@"UI/ArrowDown");

            Panel = content.Load<Texture2D>(@"UI/Panel");
            Terminal = content.Load<Texture2D>(@"UI/Terminal");
            Cursor = content.Load<Texture2D>(@"UI/Cursor");
            DevConsole = content.Load<Texture2D>(@"UI/DevConsole");
            Crux12 = content.Load<BitmapFont>(@"Fonts/CoderCrux_12");
            MineCraftia12 = content.Load<BitmapFont>(@"Fonts/MineCraftia_12");
            MineCraftia10 = content.Load<BitmapFont>(@"Fonts/MineCraftia_10");

            SteelFlight8 = content.Load<BitmapFont>(@"Fonts/SteelFlight_8");
            SteelFlight16 = content.Load<BitmapFont>(@"Fonts/SteelFlight_16");

            Plumbis11 = content.Load<BitmapFont>(@"Fonts/Plumbis_11");
        }

    }
}

