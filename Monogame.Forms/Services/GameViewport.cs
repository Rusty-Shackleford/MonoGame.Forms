using Microsoft.Xna.Framework;
using System;
using MonoGame.Forms.Anchoring;

namespace MonoGame.Forms.Services
{
    public class GameViewport : IAnchorable, MonoGame.Extended.IUpdate
    {
        #region [ Constructor ]
        public GameViewport()
        {
            Width = ServiceProvider.Graphics.Viewport.Width;
            Height = ServiceProvider.Graphics.Viewport.Height;
        }
        #endregion


        #region [ Members ]
        public Vector2 Position
        {
            get { return Vector2.Zero; }
            set { Position = Vector2.Zero; }
        }

        public event EventHandler OnPositionChanged;

        #region [ Field: Height ]
        private int _previousHeight;
        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _previousHeight = _height;
                    _height = value;
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        #region [ Field: Width ]
        private int _previousWidth;
        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _previousWidth = _width;
                    _width = value;
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        public Rectangle InteractiveBounds
        {
            get { return Bounds; }
        }

        public BoxProperty Margin { get; set; }

        public BoxProperty Padding { get; set; }
        #endregion


        #region [ Method: Update ]
        public void Update(GameTime gameTime)
        {
            Width = ServiceProvider.Graphics.Viewport.Width;
            Height = ServiceProvider.Graphics.Viewport.Height;
        }

        public void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds)
        {
            throw new NotSupportedException("Can't anchor this viewport to anything.");
        }


        private AnchorComponent _anchor = null;
        public AnchorComponent Anchor { get { return _anchor; } }
        #endregion
    }
}
