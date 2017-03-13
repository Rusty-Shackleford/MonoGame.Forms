using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Styles;
using System;


namespace MonoGame.Forms.Controls
{
    public abstract class Control : IAnchorable, IContainable, IInteractive, IDraggable
    {
        protected Control()
        {
            Visible = true;
            Enabled = true;
        }


        #region [ Members ]

        #region [ Anchoring ]
        public event EventHandler OnPositionChanged;
        private AnchorComponent _anchor;
        public AnchorComponent Anchor
        {
            get { return _anchor; }
        }


        public void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds)
        {
            RemoveAnchor();
            _anchor = new AnchorComponent(target, this, anchorType, style, new MonoGame.Extended.Size(offsetX, offsetY));
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

        #region [ Dragging ]
        public bool EnableDragging { get; set; }

        private Vector2 _originalPosition;
        public Vector2 OriginalPosition
        {
            get { return _originalPosition; }
            private set { _originalPosition = value; }
        }

        public void CancelDrag() { }

        public void DragStart(MouseEventArgs e)
        {
            if (EnableDragging)
            {
                Dragging = true;
                Move(e);
            }
        }


        public void DragEnd(MouseEventArgs e)
        {
            if (EnableDragging)
            {
                Dragging = false;
                Move(e);
            }
        }


        public void Drag(MouseEventArgs e)
        {
            if (EnableDragging)
            {
                Move(e);
            }
        }

        public virtual Vector2 Move(MouseEventArgs e)
        {
            Position += e.DistanceMoved;
            return Position;
        }
        public virtual Vector2 Move(Vector2 distance)
        {
            Position += distance;
            return Position;
        }
        #endregion

        private ControlStyle _style;
        public virtual ControlStyle Style
        {
            get { return _style; }
            set
            {
                if (value != null)
                {
                    _style = value;
                    Width = Style.TextureDefault.Width;
                    Height = Style.TextureDefault.Height;
                }
            }
        }

        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool Hovered { get; protected set; }
        public bool Dragging { get; protected set; }


        private bool _hasFocus;
        public bool HasFocus
        {
            get { return _hasFocus; }
            set
            {
                if (value != _hasFocus)
                {
                    _hasFocus = value;
                    if (_hasFocus)
                    {
                        OnGainedFocus?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        OnLostFocus?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public ControlState State
        {
            get
            {
                if (!Enabled)
                {
                    return ControlState.Disabled;
                }
                if (Hovered)
                {
                    if (Pressed)
                    {
                        return ControlState.Pressed;
                    }
                    return ControlState.Hovered;
                }
                if (HasFocus)
                {
                    return ControlState.Activated;
                }
                return ControlState.Default;
            }
        }


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

        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Width,
                    Height
                );
            }
        }

        //TODO: Implement Alpha
        /// <summary>
        /// Some controls may use images that contain a drop-shadow, or other effects near the edges which will
        /// be in the image,but should not count for clicks, hover, etc.  This would be a rectangle drawn within the
        /// Bounding Rectangle of the control.
        /// </summary>
        public Rectangle InteractiveBounds
        {
            get
            {
                if (Style != null)
                {
                    if (Style.InteractiveOffset != Rectangle.Empty)
                    {
                        return new Rectangle(
                            (int)Position.X + Style.InteractiveOffset.X,
                            (int)Position.Y + Style.InteractiveOffset.Y,
                            Style.InteractiveOffset.Width,
                            Style.InteractiveOffset.Height
                        );
                    }
                }
                return Bounds;
            }
        }

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
        #endregion


        #region [ Events ]
        public event EventHandler OnGainedFocus;
        public event EventHandler OnLostFocus;
        public event EventHandler OnClicked;
        public event EventHandler OnDimmensionChange;
        public event EventHandler OnMouseOver;
        public event EventHandler OnMouseOut;

        public virtual void MouseOver(MouseEventArgs e)
        {
            if (Enabled && !Hovered)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    Hovered = true;
                    OnMouseOver?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public virtual void MouseOut(MouseEventArgs e)
        {
            if (Enabled && Hovered)
            {
                if (!InteractiveBounds.Contains(e.Position))
                {
                    Hovered = false;
                    OnMouseOut?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public virtual void Press(MouseEventArgs e)
        {
            if (Enabled)
            {
                Pressed = true;
            }
        }


        public virtual void Click(MouseEventArgs e)
        {
            if (Enabled && Hovered)
            {
                Pressed = false;
                OnClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                HasFocus = false;
            }
        }
        #endregion


        #region [ Update ]
        public virtual void Update(GameTime gameTime) { }
        #endregion

        #region [ Draw ]
        public virtual void Draw(SpriteBatch spriteBatch) { }
        #endregion
    }
}
