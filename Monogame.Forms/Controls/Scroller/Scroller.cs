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
        public Scroller(IScroll owner)
        {
            _owner = owner;
            if (_owner.Style.ScrollerStyle == null)
            {
                throw new NotSupportedException("Scrolling controls require a Scroller Style.");
            }

            _content = _owner.ContentManager.Contents;
            _style = _owner.Style.ScrollerStyle;

            ScrollBar = new ScrollBar();
            ScrollBar.Width = 18;
            ScrollBar.Height = _owner.Bounds.Height;
            ScrollBar.AnchorTo(_owner, Anchoring.PositionType.Outside_Right_Top, 0, 0, Anchoring.AnchorType.Bounds);

        }
        #endregion


        #region [ Members ]
        private IScroll _owner;
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
            if (ScrollBar != null)
            {
                spriteBatch.Draw(_style.ScrollBarTexture, ScrollBar.Bounds, Color.White);
            }
        }
        #endregion
    }
}
