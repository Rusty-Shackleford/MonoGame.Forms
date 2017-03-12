using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Controls.Renderers;

namespace MonoGame.Forms.Controls
{
    public class Button : Control
    {
        #region [ Constructor ]
        public Button(ControlStyle style) : this("", style) { }

        public Button(string label, ControlStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this button.");
            }

            Style = style;

            _render = new ControlRenderer(this);
            if (!string.IsNullOrEmpty(label) && style.FontStyle != null)
            {
                Label = new Label(label, style.FontStyle);
                Label.AnchorTo(this, Anchoring.PositionType.Inside_Middle_Center, 0, 0, Anchoring.AnchorType.Bounds);
            }
        }
        #endregion


        #region [ Members ]
        private ControlRenderer _render;
        public Label Label { get; set; }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                _render.Draw(spriteBatch);
                if (Label != null)
                {
                    Label.Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}
