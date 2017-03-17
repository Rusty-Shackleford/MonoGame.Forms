using Microsoft.Xna.Framework;
using MonoGame.Forms.Anchoring;
using System;

namespace MonoGame.Forms
{
    public class AnchoredRectangle : IAnchorable, IEquatable<AnchoredRectangle>
    {
        #region [ Constructors ]
        public AnchoredRectangle() : this(0) { }
        public AnchoredRectangle(Rectangle bounds, AnchorToArgs args): this(new Vector2(bounds.X, bounds.Y), bounds.Width, bounds.Height, args) { }
        public AnchoredRectangle(int x, int y, int height, int width) : this(new Vector2(x,y), height, width, null) { }
        public AnchoredRectangle(int square) : this(Vector2.Zero, square, square, null) { }
        public AnchoredRectangle(Rectangle rect) : this(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, null) { }
        public AnchoredRectangle(Vector2 position, int width, int height, AnchorToArgs anchoring)
        {
            Width = width;
            Height = height;
            Position = position;
            if (anchoring != null)
            {
                AnchorTo(anchoring);
            }
        }
        #endregion


        #region [ Members ]
        public static AnchoredRectangle Empty { get { return new AnchoredRectangle(); } }

        protected int _height;
        public virtual int Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected int _width;
        public virtual int Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Rectangle EffectiveBounds { get { return Bounds; } }
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    _position = value;
                    OnPositionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
        }
        #endregion


        #region [ Anchoring ]
        public event EventHandler OnPositionChanged;
        private AnchorComponent _anchor;
        public AnchorComponent Anchor
        {
            get { return _anchor; }
        }
        public void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds)
        {
            if (target != (IAnchorable)this)
            {
                RemoveAnchor();
                _anchor = new AnchorComponent(target, this, anchorType, style, new MonoGame.Extended.Size(offsetX, offsetY));
                return;
            }
            Console.WriteLine("WARNING: Attempted to anchor this object to itself.");
        }
        public void AnchorTo(AnchorToArgs args)
        {
            AnchorTo(args.AnchorTo, args.PositionType, args.OffsetX, args.OffsetY, args.AnchorType);
        }

        public void RemoveAnchor()
        {
            if (_anchor != null)
            {
                _anchor.RemoveAnchor();
                _anchor = null;
            }
        }
        #endregion


        #region [ Operators ]
        public override bool Equals(object obj)
        {
            if (obj is AnchoredRectangle)
            {
                return Equals((AnchoredRectangle)obj);
            }
            return false;
        }

        public bool Equals(AnchoredRectangle other)
        {
            return this == other;
        }

        public static bool operator ==(AnchoredRectangle value1, AnchoredRectangle value2)
        {
            return
                value1.Bounds == value1.Bounds;
        }

        public static bool operator !=(AnchoredRectangle value1, AnchoredRectangle value2)
        {
            return
                value1.Bounds != value1.Bounds;
        }

        public override int GetHashCode()
        {
            return Bounds.GetHashCode();
        }
        #endregion


        #region [ Rectangle Functions ]
        public int Bottom { get { return Bounds.Bottom; } }
        public Point Center { get { return Bounds.Center; } }
        public bool IsEmpty { get { return Bounds.IsEmpty; } }
        public int Left { get { return Bounds.Left; } }
        public Point Location { get { return Bounds.Location; } }
        public int Right { get { return Bounds.Right; } }
        public Point Size { get { return Bounds.Size; } }
        public int Top { get { return Bounds.Top; } }
        public bool Intersects(Rectangle value) { return Bounds.Intersects(value); }
        public bool Contains(Point value) { return Bounds.Contains(value); }
        public bool Contains(Vector2 value) { return Bounds.Contains(value); }
        public bool Contains(Rectangle value) { return Bounds.Contains(value); }
        public bool Contains(int x, int y) { return Bounds.Contains(x, y); }
        public bool Contains(float x, float y) { return Bounds.Contains(x, y); }
        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }

        #endregion



    }
}
