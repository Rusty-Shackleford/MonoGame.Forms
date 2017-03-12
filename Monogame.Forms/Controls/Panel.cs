using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class Panel : Control, IDraggable
    {

        #region [ Constructor ]
        public Panel(ControlStyle style): base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this panel.");
            }
            _render = new ControlRenderer(this);
        }
        #endregion


        #region [ Members ]
        public bool Dragging { get; private set; }
        private GameViewport _window = ServiceProvider.GetService<GameViewport>();

        private Vector2 _originalPosition;
        public Vector2 OriginalPosition
        {
            get { return _originalPosition; }
            private set { _originalPosition = value; }
        }
        private ControlRenderer _render;
        #endregion




        #region [ Drag ]
        public void CancelDrag() { }

        public void DragStart(MouseEventArgs e)
        {
            Dragging = true;
            Move(e);
        }

        public void DragEnd(MouseEventArgs e)
        {
            Dragging = false;
            Move(e);
        }
         
        public void Drag(MouseEventArgs e)
        {
            Move(e);
        }

        public Vector2 Move(MouseEventArgs e)
        {
            var newPosition = Position + e.DistanceMoved;

            if (newPosition.X + Width > _window.Width)
            {
                newPosition.X = _window.Width - Width;
            }
            if (newPosition.Y + Height > _window.Height)
            {
                newPosition.Y = _window.Height - Height;
            }
            if (Position.X < 0)
            {
                newPosition.X = 0;
            }
            if (Position.Y < 0)
            {
                newPosition.Y = 0;
            }

            Position = newPosition;
            return Position;
        }
        #endregion



        #region [ Update ]
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
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
