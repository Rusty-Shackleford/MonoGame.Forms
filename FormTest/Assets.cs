using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTest
{
    public static class Assets
    {

        #region [ Button Style ]
        public static Texture2D Button { get; private set; }
        public static Texture2D ButtonHover { get; private set; }
        public static Texture2D ButtonPressed { get; private set; }
        #endregion


        #region [ Scroller Style ]
        public static ScrollerStyle ScrollerStyle { get; private set; }
        public static Texture2D ScrollUp { get; private set; }
        public static Texture2D ScrollDown { get; private set; }
        public static Texture2D ScrollUpHover { get; private set; }
        public static Texture2D ScrollDownHover { get; private set; }
        public static Texture2D ScrollUpPressed { get; private set; }
        public static Texture2D ScrollDownPressed { get; private set; }
        // Azure Blue Highlight:   21, 132, 221
        // Background Very Dark Grey: 30, 30, 30
        // Background Dark Grey: 40, 40, 40 
        public static Color ScrollThumb = new Color(104, 104, 104);
        public static Color ScrollBar = new Color(62, 62, 66);
        public static Color ScrollThumbHover = new Color(158, 158, 158);
        #endregion


        #region [ Misc. ]
        public static Texture2D Panel_Bg { get; private set; }
        public static Texture2D Terminal_Bg { get; private set; }
        public static Texture2D Cursor { get; private set; }
        public static Texture2D DevConsole_Bg { get; private set; }
        #endregion


        #region [ Fonts ]
        public static BitmapFont Font_MineCraftia11 { get; private set; } // appears to be the correct one
        #endregion


        public static void LoadContent(ContentManager content)
        {
            Button = content.Load<Texture2D>(@"UI/Button");
            ButtonHover = content.Load<Texture2D>(@"UI/ButtonHover");
            ButtonPressed = content.Load<Texture2D>(@"UI/ButtonPressed");

            ScrollUp = content.Load<Texture2D>(@"UI/ScrollUp");
            ScrollDown = content.Load<Texture2D>(@"UI/ScrollDown");
            ScrollUpHover = content.Load<Texture2D>(@"UI/ScrollUpHover");
            ScrollDownHover = content.Load<Texture2D>(@"UI/ScrollDownHover");
            ScrollUpPressed = content.Load<Texture2D>(@"UI/ScrollUpPressed");
            ScrollDownPressed = content.Load<Texture2D>(@"UI/ScrollDownPressed");
            ScrollerStyle = new ScrollerStyle(ScrollBar, ScrollThumb, ScrollThumbHover)
            {
                ScrollThumbOffset = new Vector2(4, 0),
                ScrollThumbWidth = 9,
                ScrollBarWidth = 18,
                ScrollUpButtonStyle = new ControlStyle(
                    ScrollUp,
                    ScrollUpHover,
                    ScrollUpPressed,
                    null,
                    Rectangle.Empty
                    ),
                ScrollDownButtonStyle = new ControlStyle(
                    ScrollDown,
                    ScrollDownHover,
                    ScrollDownPressed,
                    null,
                    Rectangle.Empty
                    )
            };

            // BGs
            Panel_Bg = content.Load<Texture2D>(@"UI/Panel");
            Terminal_Bg = content.Load<Texture2D>(@"UI/Terminal");
            Cursor = content.Load<Texture2D>(@"UI/Cursor");
            DevConsole_Bg = content.Load<Texture2D>(@"UI/DevConsole");

            // Fonts
            Font_MineCraftia11 = content.Load<BitmapFont>(@"Fonts/MineCraftia_11");
        }

    }
}

