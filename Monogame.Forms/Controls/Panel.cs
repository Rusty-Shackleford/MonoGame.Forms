using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class Panel : Control, IDraggable
    {

        #region [ Constructor ]
        public Panel(ControlStyle style) : this("", style) { }
        public Panel(string title, ControlStyle style): base()
        {
            EnableDragging = true;
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this panel.");
            }
            Style = style;
            if (!string.IsNullOrEmpty(title))
            {
                _label = new Label(title, Style.FontStyle);
                _label.AnchorTo(this, Anchoring.PositionType.Inside_Top_Left, 3, 8, Anchoring.AnchorType.Bounds);
            }
            _render = new ControlRenderer(this);
        }
        #endregion


        #region [ Members ]
        private GameViewport _window = ServiceProvider.GetService<GameViewport>();


        private ControlRenderer _render;

        private Label _label;

        #endregion




        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            _render.Draw(spriteBatch);
            if (_label != null)
            {
                _label.Draw(spriteBatch);
            }
        }
        #endregion


    }
}
