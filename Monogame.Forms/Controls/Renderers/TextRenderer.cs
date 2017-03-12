using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Extended.BitmapFonts;

namespace MonoGame.Forms.Controls.Renderers
{
    public class TextRenderer : IRender
    {
        #region [ Constructor ]
        public TextRenderer(IDisplayText textControl)
        {
            owner = textControl;
        }
        #endregion


        #region [ Members ]
        IDisplayText owner;
        #endregion


        #region [ Render ]
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(owner.FontStyle.Font, owner.Text, owner.Position, owner.FontStyle.Color);
        }
        #endregion
    }
}
