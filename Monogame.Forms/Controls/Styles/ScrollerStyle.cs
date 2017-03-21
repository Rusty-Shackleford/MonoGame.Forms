﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;


namespace MonoGame.Forms.Controls.Styles
{
    public class ScrollerStyle
    {
        #region [ Constructor ]
        public ScrollerStyle(Color bar, Color thumb)
        {
            ScrollBarColor = bar;
            ScrollThumbColor = thumb;

            Texture2D pixel = new Texture2D(ServiceProvider.Graphics, 1, 1);
            Color[] colorData = { ScrollBarColor };
            pixel.SetData(colorData);

            ScrollBarTexture = pixel;

            pixel = new Texture2D(ServiceProvider.Graphics, 1, 1);
            Color[] colorData2 = { ScrollThumbColor };
            pixel.SetData(colorData2);
            ScrollThumbTexture = pixel;
        }
        #endregion


        #region [ Members ]
        public Color ScrollBarColor { get; private set; }
        public Color ScrollThumbColor { get; private set; }
        public Texture2D ScrollBarTexture { get; private set; }
        public Texture2D ScrollThumbTexture { get; private set; }

        public int ScrollBarWidth { get; set; }
        public Vector2 ScrollThumbOffset { get; set; }
        public int ScrollThumbWidth { get; set; }

        // TODO: Implement ScrollBar Offset / buttons
        // public Vector2 ScrollBarOffset { get; set; }
        #endregion
    }
}
