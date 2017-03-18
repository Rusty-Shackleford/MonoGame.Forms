using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Renderers
{
    public class RenderInScissor
    {
        #region [ Constructor ]
        public RenderInScissor(GraphicsDevice graphics)
        {
            _sb = new SpriteBatch(graphics);
        }
        #endregion


        #region [ Members ]
        private RasterizerState _rasterState = new RasterizerState() { ScissorTestEnable = true };
        private SpriteBatch _sb;
        #endregion


        #region [ Render ]
        public void Draw(ContentManager contents)
        {
            _sb.Begin(blendState: BlendState.AlphaBlend, rasterizerState: _rasterState);
            _sb.GraphicsDevice.ScissorRectangle = contents.ContentArea.Bounds;
            for (int i = 0; i < contents.Contents.Count; i++)
            {
                contents.Contents[i].Draw(_sb);
            }
            _sb.End();
        }
        #endregion
    }
}
