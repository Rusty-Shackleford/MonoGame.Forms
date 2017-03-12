using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Renderers
{
    public class ControlRenderer : IRender
    {
        #region [ Constructor ]
        public ControlRenderer(Control control)
        {
            owner = control;
            if (owner.Style == null)
            {
                throw new NotSupportedException("This control is missing a style.");
            }
            style = owner.Style;
        }
        #endregion


        #region [ Members ]
        ControlStyle style;
        Control owner;

        Texture2D UseTexture
        {
            get
            {
                switch (owner.State)
                {
                    case ControlState.Default:
                        return style.TextureDefault;

                    case ControlState.Hovered:
                        if (style.TextureHovered != null)
                        {
                            return style.TextureHovered;
                        }
                        return style.TextureDefault;

                    case ControlState.Pressed:
                        if (style.TexturePressed != null)
                        {
                            return style.TexturePressed;
                        }
                        return style.TextureDefault;

                    case ControlState.Disabled:
                        if (style.TextureDisabled != null)
                        {
                            return style.TextureDisabled;
                        }
                        return style.TextureDefault;

                    case ControlState.Activated:
                        if (style.TextureActive != null)
                        {
                            return style.TextureActive;
                        }
                        return style.TextureDefault;
                        

                    default:
                        return style.TextureDefault;
                }
            }
        }
        #endregion


        #region [ Render ]
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UseTexture, owner.Position, style.Color);
        }
        #endregion

    }
}
