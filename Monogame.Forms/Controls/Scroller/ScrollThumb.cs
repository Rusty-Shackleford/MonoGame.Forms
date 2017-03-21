using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
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
        public ScrollThumb(Scroller owner, ScrollerStyle style) : base()
        {
            _owner = owner;
            _style = style;
            _owner.ScrollBar.OnPositionChanged += ScrollBarMoved;
            _distanceFromTop = 0;
            ApplyMoveCheck = MoveCheck;
        }
        #endregion


        #region [ Members ]
        private readonly Scroller _owner;
        private readonly ScrollerStyle _style;
        private float _distanceFromTop;
        #endregion


        #region [ Update Height ]
        public void UpdateHeight(ScrollBar bar, Contents contents)
        {
            float virtualHeight = bar.Height;
            float ratio = virtualHeight / contents.TotalHeight();
            var height = virtualHeight * ratio;
            _height = (int)height;

            DragAreaOffset = new Rectangle(DragAreaOffset.X, DragAreaOffset.Y, Width, Height);
        }
        private void ScrollBarMoved(object sender, PositionChangedArgs e)
        {
            //Console.WriteLine("Told by bar to move: " + e.DistanceMoved.ToString());
            //Console.WriteLine("Thumb Moved submitted for approval: " + (Position - OriginalPosition).ToString());
            MoveTo(new Vector2(_owner.ScrollBar.Position.X, _owner.ScrollBar.Position.Y + _distanceFromTop));
        }
        #endregion


        #region [ Move Check ]
        protected Vector2 MoveCheck(Vector2 proposedPosition)
        {
            Rectangle myProposedBounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, Width, Height);
            Vector2 myCounterOffer = proposedPosition;

            var scrollBarBounds = _owner.ScrollBar.Bounds;

            if (myProposedBounds.Bottom > scrollBarBounds.Bottom)
            {
                myCounterOffer = new Vector2(myCounterOffer.X, (scrollBarBounds.Bottom - Height));
            }
            if (myProposedBounds.Top < scrollBarBounds.Top)
            {
                myCounterOffer = new Vector2(myCounterOffer.X, scrollBarBounds.Top);
            }
            if (myProposedBounds.Left != scrollBarBounds.Left + _style.ScrollThumbOffset.X)
            {
                myCounterOffer = new Vector2(scrollBarBounds.X + _style.ScrollThumbOffset.X, myCounterOffer.Y);
            }

            if (myCounterOffer != Position)
            {
                _distanceFromTop = myCounterOffer.Y - _owner.ScrollBar.Position.Y;
               // Console.WriteLine("Move Check - move to: " + myCounterOffer.ToString());
                return myCounterOffer;
            }
          //  Console.WriteLine("Move Check - denied");
            return Position;
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
