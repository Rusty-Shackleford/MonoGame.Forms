using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Renderers
{
    public interface IRender
    {
        // NOTE: at the moment this is the same as IRender
        void Draw(SpriteBatch spriteBatch);
    }
}
