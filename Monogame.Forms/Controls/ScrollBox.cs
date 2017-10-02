using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class ScrollBox : Control
    {

        #region [ Constructor ]
        /// <summary>
        /// [ CURRENTLY UNUSED ] ScrollBox is the virtual window mask in which the contents of a 
        /// container are displayed via a Scissor Rectangle.
        /// </summary>
        /// <param name="style"></param>
        public ScrollBox(ControlStyle style) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("Provide a style for this ScrollBox");
            }
            Style = style;
        }
        #endregion


        #region [ Members ]
        private RasterizerState _rasterState = new RasterizerState() { ScissorTestEnable = true };
        
        #endregion


        #region [ Update ]
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
