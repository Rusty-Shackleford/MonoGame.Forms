using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Scrollers
{
    public class ScrollThumb : Control
    {
        #region [ Constructor ]
        public ScrollThumb(ScrollBar bar) : base()
        {
            _scrollBar = bar;
        }
        #endregion


        #region [ Members ]
        private readonly ScrollBar _scrollBar;
        #endregion


        #region [ Update Height ]
        public void UpdateHeight(ScrollBar bar, Contents contents)
        {
            float virtualHeight = (float)bar.Height;
            float ratio = virtualHeight / contents.TotalHeight();
            var height = virtualHeight * ratio;
            _height = (int)height;
        }
        #endregion


        #region [ Move Check ]
        protected override void MoveCheck()
        {
            if (Bounds.Bottom > _scrollBar.Bounds.Top)
            {
                Position = new Vector2(Position.X, (_scrollBar.Bounds.Top - Height));
            }
            if (Bounds.Top < _scrollBar.Bounds.Top)
            {
                Position = new Vector2(Position.X, _scrollBar.Bounds.Top);
            }
            //TODO: Left Off Here----- LEFT / RIGHT

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
