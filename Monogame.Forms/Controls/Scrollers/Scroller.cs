using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
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
            _style = _owner.Style.ScrollerStyle;
        }
        #endregion


        #region [ Initialize ]
        public void Initialize()
        {
            ScrollBar = new ScrollBar();
            ScrollBar.Width = _style.ScrollBarWidth;
            ScrollBar.Height = _owner.Bounds.Height - _owner.DragBounds.Height;
            ScrollBar.AnchorTo(_owner, PositionType.Inside_Top_Right, 0, _owner.DragBounds.Height, AnchorType.Bounds);

            if (_style.ScrollUpButtonStyle != null && _style.ScrollDownButtonStyle != null)
            {
                ScrollUpBtn = new Button(_style.ScrollUpButtonStyle);
                ScrollUpBtn.AnchorTo(
                    ScrollBar, PositionType.Inside_Top_Left, 0, 0, AnchorType.Bounds
                );

                ScrollDownBtn = new Button(_style.ScrollDownButtonStyle);
                ScrollDownBtn.AnchorTo(
                    ScrollBar, PositionType.Inside_Bottom_Left, 0, 0, AnchorType.Bounds
                );
                HasButtons = true;
            }

            ScrollThumb = new ScrollThumb(this, _style);
            ScrollThumb.EnableDragging = true;
            ScrollThumb.MoveTo(new Vector2(ScrollBarArea.X, ScrollBarArea.Y));
            ScrollThumb.Width = _style.ScrollThumbWidth;
            ScrollThumb.UpdateHeight(ScrollBar, _ownerContents);
            ScrollThumb.OnScrolled += Scroll;
            // USE CASES FOR UPDATING THE SCROLL THUMB HEIGHT HERE:
            _owner.ContentManager.OnItemAdded += UpdateThumbHeight;
            _owner.ContentManager.OnItemRemoved += UpdateThumbHeight;

            if (_owner.ContentManager.Contents[0] != null)
            {
                _containerKeyItem = _owner.ContentManager.Contents[0];
                _containerStartPosition = _containerKeyItem.Position;
            }
            // Input Handlers
            var contents = new Contents();
            contents.Add(ScrollBar);
            contents.Add(ScrollThumb);
            if (HasButtons)
            {
                contents.Add(ScrollUpBtn);
                contents.Add(ScrollDownBtn);
            }
            _input = new InputHandler(contents);

            Initialized = true;
        }
        #endregion


        #region [ Members ]
        public bool Initialized { get; private set; }
        private readonly MouseListener _mouse = KVM.Mouse;
        private readonly IScroll _owner;
        private Contents _ownerContents
        {
            get { return _owner.ContentManager.Contents; }
        }
        private IContainable _containerKeyItem;
        private Vector2 _containerStartPosition;

        private readonly ScrollerStyle _style;
        private InputHandler _input;

        public ScrollBar ScrollBar { get; private set; }
        public ScrollThumb ScrollThumb { get; private set; }
        public Button ScrollDownBtn { get; private set; }
        public Button ScrollUpBtn { get; set; }
        public bool HasButtons { get; private set; }

        public bool ScrollNeeded => (_ownerContents.TotalHeight() > _owner.ContentManager.ContentArea.Height);

        public float ScrollHeight
        {
            get
            {
                if (HasButtons)
                {
                    return ((ScrollBar.Height - ScrollDownBtn.Height - ScrollUpBtn.Height) - ScrollThumb.Height);
                }
                else
                {
                    return (ScrollBar.Height - ScrollThumb.Height);
                }
            }
        }

        public Rectangle ScrollBarArea
        {
            get
            {
                if (HasButtons)
                {
                    return new Rectangle(
                        (int)ScrollBar.Position.X + (int)_style.ScrollThumbOffset.X,
                        (int)ScrollBar.Position.Y + ScrollUpBtn.Height,
                        ScrollBar.Width,
                        (ScrollBar.Height - ScrollDownBtn.Height - ScrollUpBtn.Height)
                    );
                    // ((ScrollBar.Height - ScrollDownBtn.Height - ScrollUpBtn.Height) - ScrollThumb.Height)
                }
                else
                {
                    return new Rectangle(
                        (int)ScrollBar.Position.X + (int)_style.ScrollThumbOffset.X,
                        (int)ScrollBar.Position.Y,
                        ScrollBar.Width,
                        (ScrollBar.Height - ScrollThumb.Height)
                    );
                }

            }
        }
        #endregion


        #region [ Update Thumb ]
        private void UpdateThumbHeight(object sender, EventArgs e)
        {
            if (ScrollNeeded)
            {
                ScrollThumb.UpdateHeight(sender as ScrollBar, _ownerContents);
            }
        }
        #endregion


        #region [ SCROLL ]
        public void Scroll(object sender, ScrollArgs e)
        {
            float distanceToTravel = (_ownerContents.TotalHeight() * e.DistanceChangedPct);
            _containerKeyItem.Move(new Vector2(0, distanceToTravel * -1));
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
           // var console = ServiceProvider.GetService<DevConsole>();
        }
        #endregion


        #region [ Draw ]
        //TODO: Move to Renderer
        public void Draw(SpriteBatch spriteBatch)
        {
            if (ScrollBar != null)
            {
                spriteBatch.Draw(_style.Pixel, ScrollBar.Bounds, _style.ScrollBar);
            }
            if (HasButtons)
            {
                ScrollUpBtn.Draw(spriteBatch);
                ScrollDownBtn.Draw(spriteBatch);
            }
            if (ScrollThumb != null)
            {
                Color scrollThumb = _style.ScrollThumb;
                if (ScrollThumb.Hovered || ScrollThumb.Pressed)
                    scrollThumb = _style.ScrollThumbHover;

                spriteBatch.Draw(_style.Pixel, ScrollThumb.Bounds, scrollThumb);
            }
        }
        #endregion
    }
}
