using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;

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
            Vector2 test = new Vector2((int)owner.Position.X, (int)owner.Position.Y);
            spriteBatch.DrawString(owner.FontStyle.Font, owner.Text, test, owner.FontStyle.Color);
        }
        #endregion
    }
}
