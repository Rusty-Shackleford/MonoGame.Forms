using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;


namespace MonoGame.Forms.Controls.Styles
{
    public class ScrollerStyle
    {
        #region [ Constructor ]
        public ScrollerStyle(Color bar, Color thumb, Color hover)
        {
            ScrollBar = bar;
            ScrollThumb = thumb;
            ScrollThumbHover = hover;
            Pixel = Util.MakePixel(Color.White);
        }
        #endregion


        #region [ Members ]
        public Color ScrollBar { get; private set; }
        public Color ScrollThumb { get; private set; }
        public Color ScrollThumbHover { get; private set; }
        public Texture2D Pixel { get; private set; }

        public int ScrollBarWidth { get; set; }
        public Vector2 ScrollThumbOffset { get; set; }
        public int ScrollThumbWidth { get; set; }

        public ControlStyle ScrollUpButtonStyle { get; set; }
        public ControlStyle ScrollDownButtonStyle { get; set; }
        // TODO: Implement ScrollBar Offset / buttons
        // public Vector2 ScrollBarOffset { get; set; }
        #endregion
    }
}
