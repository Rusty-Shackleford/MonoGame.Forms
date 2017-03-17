using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Styles
{
    public class ScrollerStyle
    {
        #region [ Constructor ]
        public ScrollerStyle(Color background, Color foreground)
        {
            Background = background;
            Foreground = foreground;

            Texture2D pixel = new Texture2D(ServiceProvider.Graphics, 1, 1);
            Color[] colorData = { background };
            pixel.SetData(colorData);

            ScrollBarTexture = pixel;

            pixel = new Texture2D(ServiceProvider.Graphics, 1, 1);
            Color[] colorData2 = { background };
            pixel.SetData(colorData2);
            ScrollThumbTexture = pixel;
        }
        #endregion


        #region [ Members ]
        public Color Background { get; private set; }
        public Color Foreground { get; private set; }
        public Texture2D ScrollBarTexture { get; private set; }
        public Texture2D ScrollThumbTexture { get; private set; }
        #endregion
    }
}
