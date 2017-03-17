using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Scrollers
{
    public class Scroller
    {
        #region [ Constructor ]
        public Scroller(Contents content, ScrollerStyle style)
        {
            _content = content;
            _style = style;
        }
        #endregion


        #region [ Members ]
        private Contents _content;

        private ScrollerStyle _style;

        public ScrollBar ScrollBar { get; private set; }
        public ScrollThumb ScrollThumb { get; private set; }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region [ Draw ]
        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
