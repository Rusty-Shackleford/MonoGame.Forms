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
        }
        #endregion


        #region [ Members ]
        protected Control owner;

        Texture2D UseTexture
        {
            get
            {
                switch (owner.State)
                {
                    case ControlState.Default:
                        return owner.Style.TextureDefault;

                    case ControlState.Hovered:
                        if (owner.Style.TextureHovered != null)
                        {
                            return owner.Style.TextureHovered;
                        }
                        return owner.Style.TextureDefault;

                    case ControlState.Pressed:
                        if (owner.Style.TexturePressed != null)
                        {
                            return owner.Style.TexturePressed;
                        }
                        return owner.Style.TextureDefault;

                    case ControlState.Disabled:
                        if (owner.Style.TextureDisabled != null)
                        {
                            return owner.Style.TextureDisabled;
                        }
                        return owner.Style.TextureDefault;

                    case ControlState.Activated:
                        if (owner.Style.TextureActive != null)
                        {
                            return owner.Style.TextureActive;
                        }
                        return owner.Style.TextureDefault;
                        

                    default:
                        return owner.Style.TextureDefault;
                }
            }
        }
        #endregion


        #region [ Render ]
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UseTexture, owner.Position, owner.Style.Color);
        }
        #endregion

    }
}
