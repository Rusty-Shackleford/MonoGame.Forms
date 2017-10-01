using Microsoft.Xna.Framework;
using System;
using MonoGame.Forms.Anchoring;
using MonoGame.Extended;

namespace MonoGame.Forms.Services
{
    public class GameViewport : GameComponent, IAnchorable, IUpdate
    {
        #region [ Constructor ]
        public GameViewport(Game game) : base(game)
        {
            Width = game.GraphicsDevice.Viewport.Width;
            Height = game.GraphicsDevice.Viewport.Height;
        }
        #endregion


        #region [ Members ]
        public Vector2 DistanceMoved { get; set; }
        public event EventHandler<PositionChangedArgs> OnPositionChanged;
        public event EventHandler OnDimmensionChanged;

        public Vector2 OriginalPosition { get; protected set; }
        public Vector2 Position
        {
            get { return Vector2.Zero; }
            set { Position = Vector2.Zero; }
        }
        public Vector2 Move(Vector2 distance)
        {
            return Position;
        }
        public Vector2 MoveTo(Vector2 newPosition)
        {
            return Position;
        }
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
                    OnDimmensionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
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
                    OnDimmensionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public Rectangle Bounds
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        public Rectangle EffectiveBounds
        {
            get { return Bounds; }
        }

        public BoxProperty Margin { get; set; }

        public BoxProperty Padding { get; set; }
        #endregion


        #region [ Method: Update ]
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Width = KVM.Graphics.Viewport.Width;
            Height = KVM.Graphics.Viewport.Height;
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
