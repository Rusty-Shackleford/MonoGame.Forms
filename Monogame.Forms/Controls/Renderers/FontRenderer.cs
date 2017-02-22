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
        FontStyle style;
        IDisplayText owner;
        #endregion


        #region [ Render ]
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(owner.Style.Font, owner.Text, owner.Position, owner.Style.FontColor);
        }
        #endregion
    }
}
