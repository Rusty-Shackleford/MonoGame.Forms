using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Services;
using System.Collections.Generic;

namespace MonoGame.Forms
{
    /// <summary>
    /// Listens for events to be passed to controls within the Contents
    /// </summary>
    public class InputHandler
    {
        #region [ Constructor ]
        public InputHandler(Contents contents)
        {
            Contents = contents;
            _mouse.MouseMoved += Hover;
            _mouse.MouseDown += Press;
            _mouse.MouseUp += Click;

            _mouse.MouseDragStart += MoveStart;
            _mouse.MouseDrag += Move;
            _mouse.MouseDragEnd += MoveEnd;
        }
        #endregion


        #region [ Members ]
        public Contents Contents { get; protected set; }
        private readonly MouseListener _mouse = ServiceProvider.GetService<MouseListener>();

        private IInteractive _hoveredItem { get; set; }
        private IInteractive _pressedItem { get; set; }
        private IDraggable _movingItem { get; set; }
        /// <summary>
        /// The activated Item has focus, and further interactions should be sent
        /// to it until it loses focus.
        /// </summary>
        private IInteractive _activatedItem { get; set; }
        #endregion


        #region [ Movement ]
        protected virtual void MoveEnd(object sender, MouseEventArgs e)
        {
            if (_movingItem != null)
            {
                _movingItem.DragEnd(e);
                _movingItem = null;
            }
        }


        protected virtual void Move(object sender, MouseEventArgs e)
        {
            if (_movingItem != null)
            {
                _movingItem.Drag(e);
            }
        }


        protected virtual void MoveStart(object sender, MouseEventArgs e)
        {
            IContainable c = Contents.GetItemAtPoint(e.Position);
            if (_movingItem == null && c != null)
            {
                if (c is IDraggable)
                {
                    IDraggable movingItem = (IDraggable)c;
                    if (movingItem.DragBounds.Contains(e.Position))
                    {
                        _movingItem = (IDraggable)c;
                        _movingItem.DragStart(e);
                    }
                }
            }
        }
        #endregion


        #region [ Hover ]
        protected virtual void Hover(object sender, MouseEventArgs e)
        {
            IContainable c = Contents.GetItemAtPoint(e.Position);
            if (_hoveredItem != null)
            {
                if (_hoveredItem != (IContainable)c)
                {
                    _hoveredItem.MouseOut(e);
                }
            }
            if (c != null)
            {
                if (c is IInteractive)
                {
                    _hoveredItem = (IInteractive)c;
                    _hoveredItem.MouseOver(e);
                }
            }
        }
        #endregion


        #region [ Press ]
        protected virtual void Press(object sender, MouseEventArgs e)
        {
            IContainable c = Contents.GetItemAtPoint(e.Position);
            if (c != null)
            {
                if (c is IInteractive)
                {
                    _pressedItem = (IInteractive)c;
                    _pressedItem.Press(e);
                }
            }
        }
        #endregion


        #region [ Click ]
        protected virtual void Click(object sender, MouseEventArgs e)
        {
            IContainable c = Contents.GetItemAtPoint(e.Position);

            if (_pressedItem != null)
            {
                if (c != null && c == _pressedItem)
                {
                    _pressedItem.Click(e);
                    _pressedItem = null;
                }
            }
            // if click was not on active item's effective bounds,
            // tell it there was a click
            if (_activatedItem != null)
            {
                _activatedItem.Click(e);
            }
        }
        #endregion



    }
}
