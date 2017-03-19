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


        #region [ Update ]
        public void UpdateHeight(ScrollBar bar, Contents contents)
        {
            float virtualHeight = (float)bar.Height;
            float ratio = virtualHeight / contents.TotalHeight();
            var height = virtualHeight * ratio;
            _height = (int)height;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void MoveCheck()
        {
            if (true)
            {

            }
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
