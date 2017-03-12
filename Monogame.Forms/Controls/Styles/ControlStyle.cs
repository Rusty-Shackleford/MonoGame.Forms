using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Styles
{
    /// <summary>
    /// Generic style information used to render a Control.
    /// </summary>
    public class ControlStyle
    {
        #region [ Constructor ]
        /// <summary>
        /// Create a ControlStyle with a single look, in which the dimmensions of the
        /// image itself are the clickable area.
        /// </summary>
        /// <param name="texture">The image which represents the control</param>
        public ControlStyle(Texture2D texture): 
            this(texture, null, null, null, new Rectangle(0,0,0,0)) { }

        public ControlStyle(Texture2D texture, Rectangle offset) : 
            this(texture, null, null, null, offset){ }


        public ControlStyle(Texture2D textureDefault, 
            Texture2D textureHovered, 
            Texture2D texturePushed,
            Texture2D textureInactive,
            Rectangle clickAreaOffset)
        {
            TextureDefault = textureDefault;
            TextureHovered = textureHovered;
            TexturePressed = texturePushed;
            TextureDisabled = textureInactive;
            InteractiveOffset = clickAreaOffset;
            Color = Color.White;
        }
        #endregion

        #region [ Members ]
        /// <summary>
        /// The Texture2D used for the control by default.
        /// </summary>
        public Texture2D TextureDefault { get; set; }
        /// <summary>
        /// The Texture2D used when under the mouse cursor.
        /// </summary>
        public Texture2D TextureHovered { get; set; }
        /// <summary>
        /// The Texture2D used when the left mouse button is down, and the control is
        /// under the mouse cursor.
        /// </summary>
        public Texture2D TexturePressed { get; set; }

        /// <summary>
        /// The Texture2D
        /// </summary>
        public Texture2D TextureDisabled { get; set; }

        /// <summary>
        /// The starting Vector within the texture, and how wide and tall
        /// the "clickable" area of the control should be.  To account for
        /// drop-shadow transparency and other edge effects.
        /// </summary>
        public Rectangle InteractiveOffset { get; set; }

        /// <summary>
        /// Offset of where to place the label for this control.
        /// </summary>
        public Rectangle LabelOffset { get; set; }

        /// <summary>
        /// When a texture has active focus, like a textbox
        /// </summary>
        public Texture2D TextureActive { get; set; }


        public Color Color { get; set; }

        public FontStyle FontStyle { get; set; }

        #endregion
    }
}
