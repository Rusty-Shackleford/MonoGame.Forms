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
            if (_owner.HasButtons)
            {
                _owner.ScrollUpBtn.OnClicked += ScrollUpBtnClick;
                _owner.ScrollDownBtn.OnClicked += ScrollDownBtnClick;

            }
            DistanceFromTop = 0;
            ApplyMoveCheck = MoveCheck;
        }
        #endregion


        #region [ Members ]
        private readonly Scroller _owner;
        private readonly ScrollerStyle _style;

        public event EventHandler<ScrollArgs> OnScrolled;

        private float _distanceFromTop;
        public float DistanceFromTop
        {
            get { return _distanceFromTop; }
            private set
            {
                if (value != _distanceFromTop)
                {
                    var previousDistance = _distanceFromTop;
                    _distanceFromTop = value;
                    var scrollArgs = new ScrollArgs(previousDistance, _distanceFromTop, _owner.ScrollHeight);
                    OnScrolled?.Invoke(this, scrollArgs);
                }
            }
        }
        public float Top
        {
            get
            {
                if (_owner.HasButtons)
                    return _owner.ScrollDownBtn.Bounds.Bottom;
                else
                    return _owner.ScrollBar.Position.Y;
            }
        }
        #endregion


        #region [ Update Height ]
        public void UpdateHeight(ScrollBar bar, Contents contents)
        {
            float virtualHeight = bar.Height;
            float ratio = virtualHeight / contents.TotalHeight();
            var height = virtualHeight * ratio;
            if (height != _height)
            {
                _height = (int)height;
                DragAreaOffset = new Rectangle(DragAreaOffset.X, DragAreaOffset.Y, Width, Height);
                //TODO:  Need to recalculate where we should be based on the new height
            }
        }
        #endregion


        #region [ Movement ]
        private void ScrollBarMoved(object sender, PositionChangedArgs e)
        {
            MoveTo(new Vector2(_owner.ScrollBar.Position.X + _style.ScrollThumbOffset.X, _owner.ScrollBar.Position.Y + DistanceFromTop));
        }


        protected void ScrollUpBtnClick(object sender, EventArgs e)
        {
            Move(new Vector2(0, (_owner.ScrollBarArea.Height / 20) * -1));
        }
        protected void ScrollDownBtnClick(object sender, EventArgs e)
        {
            Move(new Vector2(0, (_owner.ScrollBarArea.Height / 20)));
        }

        protected Vector2 MoveCheck(Vector2 proposedPosition)
        {
            Rectangle myProposedBounds = new Rectangle((int)proposedPosition.X, (int)proposedPosition.Y, Width, Height);
            Vector2 myCounterOffer = proposedPosition;

            if (myProposedBounds.Bottom > _owner.ScrollBarArea.Bottom)
            {
                myCounterOffer = new Vector2(myCounterOffer.X, (_owner.ScrollBarArea.Bottom - Height));
            }
            if (myProposedBounds.Top < _owner.ScrollBarArea.Top)
            {
                myCounterOffer = new Vector2(myCounterOffer.X, _owner.ScrollBarArea.Top);
            }
            if (myProposedBounds.Left != _owner.ScrollBarArea.X)
            {
                myCounterOffer = new Vector2(_owner.ScrollBarArea.X, myCounterOffer.Y);
            }

            if (myCounterOffer != Position)
            {
                DistanceFromTop = myCounterOffer.Y - _owner.ScrollBar.Position.Y;
                return myCounterOffer;
            }
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
