using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Styles;
using System;


namespace MonoGame.Forms.Controls
{
    public class Label : Control, IDisplayText
    {
        #region [ Constructor ]
        public Label(FontStyle style) : this("", style) { }
        public Label(string text, FontStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this Label.");
            }
            Text = text;
            Style = style;
            _render = new FontRenderer(this);
        }
        #endregion


        #region [ Members ]
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                }
            }
        }

        public FontStyle Style { get; set; }
        private FontRenderer _render;
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && !string.IsNullOrEmpty(Text))
            {
                _render.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
