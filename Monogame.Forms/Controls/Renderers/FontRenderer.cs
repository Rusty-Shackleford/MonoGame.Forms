using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Extended.BitmapFonts;

namespace MonoGame.Forms.Controls.Renderers
{
    public class FontRenderer : IRenderer
    {
        #region [ Constructor ]
        public FontRenderer(IDisplayText textControl)
        {
            owner = textControl;
            style = owner.Style;
        }
        #endregion


        #region [ Members ]
        ControlStyle style;
        IDisplayText owner;
        #endregion


        #region [ Render ]
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(style.Font, owner.Text, owner.Position, style.Color);
        }
        #endregion
    }
}
