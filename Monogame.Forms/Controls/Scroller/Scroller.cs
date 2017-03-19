using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
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
        }


        #region [ Initialize ]
        public void Initialize()
        {
            ScrollBar = new ScrollBar();
            ScrollBar.Width = 18;
            ScrollBar.Height = _owner.Bounds.Height - _owner.DragBounds.Height;
            ScrollBar.AnchorTo(_owner, Anchoring.PositionType.Inside_Top_Right, 0, _owner.DragBounds.Height, Anchoring.AnchorType.Bounds);

            ScrollThumb = new ScrollThumb(ScrollBar);
            ScrollThumb.Position = new Vector2(ScrollBar.Position.X + 4, ScrollBar.Position.Y);
            ScrollThumb.Width = 9;

            // USE CASES FOR UPDATING THE SCROLL THUMB HEIGHT:
            ScrollBar.OnPositionChanged += UpdateThumbHeight;
            _owner.ContentManager.OnItemAdded += UpdateThumbHeight;
            _owner.ContentManager.OnItemRemoved += UpdateThumbHeight;
            Initialized = true;


        }
        #endregion


        private void UpdateThumbHeight(object sender, EventArgs e)
        {
            if (ScrollNeeded)
            {
                ScrollThumb.UpdateHeight(sender as ScrollBar, _content);
            }
        }
        #endregion


        #region [ Members ]
        public bool Initialized { get; private set; }
        private readonly MouseListener _mouse = ServiceProvider.GetService<MouseListener>();
        private IScroll _owner;
        private Contents _content;

        private ScrollerStyle _style;
        private InputHandler _input;

        public ScrollBar ScrollBar { get; private set; }
        public ScrollThumb ScrollThumb { get; private set; }

        public bool ScrollNeeded
        {
            get { return (_content.TotalHeight() > _owner.ContentManager.ContentArea.Height); }
        }
        #endregion


        #region [ SCROLL ]
        public void Scroll(float distance)
        {

        }
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
            if (ScrollThumb != null)
            {
                spriteBatch.Draw(_style.ScrollThumbTexture, ScrollThumb.Bounds, Color.White);
            }
        }
        #endregion
    }
}
