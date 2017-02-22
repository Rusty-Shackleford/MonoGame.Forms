using System;
using MonoGame.Extended.InputListeners;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls.Styles;

namespace MonoGame.Forms.Controls
{
    public abstract class Control
    {

        protected Control()
        {
            Visible = true;
            Enabled = true;
        } 

        #region [ Members ]
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool UnderMouse { get; protected set; }

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

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnDimmensionChange?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                if (value != _width)
                {
                    _width = value;
                    OnDimmensionChange?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public Rectangle Bounds
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
        public Rectangle AlphaBorderBounds
        {
            get { return new Rectangle(); }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    var args = new PositionChangedArgs(_position, value, Bounds);
                    _position = value;
                    OnPositionChanged?.Invoke(this, args);
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

        public event EventHandler<PositionChangedArgs> OnPositionChanged;

        public virtual void MouseOver(MouseEventArgs e)
        {
            if (Enabled && !UnderMouse)
            {
                if (Bounds.Contains(e.Position))
                {
                    UnderMouse = true;
                    OnMouseOver?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        public virtual void MouseOut(MouseEventArgs e)
        {
            if (Enabled && UnderMouse)
            {
                if (!Bounds.Contains(e.Position))
                {
                    UnderMouse = false;
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
            if (Enabled && UnderMouse)
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
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
