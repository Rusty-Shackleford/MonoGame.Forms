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

            ScrollThumb = new ScrollThumb(this, _style);
            ScrollThumb.EnableDragging = true;
            ScrollThumb.MoveTo(new Vector2(ScrollBar.Position.X + _style.ScrollThumbOffset.X, ScrollBar.Position.Y));
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
            _input = new InputHandler(contents);

            Initialized = true;
        }
        #endregion


        #region [ Members ]
        public bool Initialized { get; private set; }
        private readonly MouseListener _mouse = ServiceProvider.GetService<MouseListener>();
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

        public bool ScrollNeeded => (_ownerContents.TotalHeight() > _owner.ContentManager.ContentArea.Height);
        public float ScrollHeight => ScrollBar.Height - ScrollThumb.Height; 
        #endregion


        #region [ ScrollThumb Moved ]
        private void ThumbMoved(object sender, PositionChangedArgs e)
        {
            
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
            //TODO: -- NEXT UP -- fix this algo
            Console.WriteLine("Move pct: " + e.DistanceChangedPct.ToString());
            float distanceToTravel = (_ownerContents.TotalHeight() * e.DistanceChangedPct);
            Console.WriteLine("Pixels To Move: " + distanceToTravel);
            _containerKeyItem.Move(new Vector2(0, distanceToTravel * -1));
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            var console = ServiceProvider.GetService<DevConsole>();
            console.Write("Scroll Height: " + ScrollHeight);
        }
        #endregion


        #region [ Draw ]
        public void Draw(SpriteBatch spriteBatch)
        {
            if (ScrollBar != null)
            {
                spriteBatch.Draw(_style.Pixel, ScrollBar.Bounds, _style.ScrollBar);
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
