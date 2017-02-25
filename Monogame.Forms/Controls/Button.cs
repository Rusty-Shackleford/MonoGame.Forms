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
        public Button(ControlStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this button.");
            }
            Style = style;
            _render = new ControlRenderer(this);
        }
        #endregion


        #region [ Members ]
        private ControlRenderer _render;

        #endregion


        #region [ Update ]
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                _render.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
