using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public interface IFormObject : IAnchorable
    {
        ControlStyle Style { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
