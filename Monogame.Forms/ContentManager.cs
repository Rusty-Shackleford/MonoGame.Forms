using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
using System;

namespace MonoGame.Forms
{
    /// <summary>
    /// Updates, Draws, etc.  >> Has its own SpriteBatch
    /// </summary>
    public class ContentManager : IUpdate
    {
        #region [ Constructor ]
        public ContentManager(GraphicsDevice gd, AnchoredRectangle contentArea)
        {
            if (contentArea != AnchoredRectangle.Empty)
            {
                throw new NotSupportedException("ContentArea cannot be empty.");
            }
            ContentArea = contentArea;

            Contents = new Contents();
            _sb = new SpriteBatch(gd);

            _mouse.MouseMoved += Hover;
            _mouse.MouseDown += Press;
            _mouse.MouseUp += Click;

            _mouse.MouseDragStart += moveCheck;
            _mouse.MouseDrag += move;
            _mouse.MouseDragEnd += moveEnd;
        }
        #endregion


        #region [ Members ]
        public Contents Contents { get; protected set; }
        private SpriteBatch _sb;

        public AnchoredRectangle ContentArea { get; set; }

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


        #region [ Add / Remove Controls ]
        public void Add(IContainable item)
        {
            if (item != null)
            {
                if (!ContentArea.Contains(item.Bounds))
                {
                    throw new NotSupportedException("Could not add " + item.GetType().ToString() + " as it does not fit within this ContentManager's viewport.");
                }
                Contents.Add(item);
            }
        }
        #endregion


        #region [ Movement ]
        private void moveEnd(object sender, MouseEventArgs e)
        {
            if (_movingItem != null)
            {
                _movingItem.DragEnd(e);
                _movingItem = null;
            }
        }


        private void move(object sender, MouseEventArgs e)
        {
            if (_movingItem != null)
            {
                _movingItem.Drag(e);
            }
        }


        private void moveCheck(object sender, MouseEventArgs e)
        {
            if (ContentArea.Contains(e.Position))
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

        }
        #endregion


        #region [ Hover ]
        private void Hover(object sender, MouseEventArgs e)
        {
            if (ContentArea.Contains(e.Position))
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
        }
        #endregion


        #region [ Press ]
        private void Press(object sender, MouseEventArgs e)
        {
            if (ContentArea.Contains(e.Position))
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
        }
        #endregion


        #region [ Click ]
        private void Click(object sender, MouseEventArgs e)
        {
            if (ContentArea.Contains(e.Position))
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
        }
        #endregion


        #region [ Unload ]
        public void UnloadContent()
        {
            //TODO: Dispose of Controls when dead?
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gt)
        {
            for (int i = 0; i < Contents.Count; i++)
            {
                Contents[i].Update(gt);
            }
        }
        #endregion


        #region [ Draw ]
        public void Draw()
        {
            _sb.Begin();
            for (int i = 0; i < Contents.Count; i++)
            {
                Contents[i].Draw(_sb);
            }
            _sb.End();
        }
        #endregion
    }
}
