using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public ControlStyle(Texture2D texture): this(texture, new Rectangle(0,0,0,0)) { }
        public ControlStyle(Texture2D texture, Rectangle offset)
        {
            Texture = texture;
            ClickAreaOffset = offset;
        }
        #endregion

        #region [ Members ]
        /// <summary>
        /// The Texture2D used for the control.
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// The starting Vector within the texture, and how wide and tall
        /// the "clickable" area of the control should be.  To account for
        /// drop-shadow transparency and other edge effects.
        /// </summary>
        Rectangle ClickAreaOffset { get; set; }

        #endregion
    }
}
