using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;


namespace MonoGame.Forms
{
    /// <summary>
    /// Updates, Draws, etc.  >> Has its own SpriteBatch
    /// </summary>
    public class ControlManager : IUpdate, IDraw
    {
        #region [ Constructor ]
        public ControlManager(GraphicsDevice gd)
        {
            controls = new FormObjectList();
            sb = new SpriteBatch(gd);

            mouse.MouseMoved += Hover;
            mouse.MouseDown += Press;
            mouse.MouseUp += Click;

            mouse.MouseDragStart += moveCheck;
            mouse.MouseDrag += move;
            mouse.MouseDragEnd += moveEnd;
        }
        #endregion


        #region [ Members ]
        private FormObjectList controls;
        public FontStyle DefaultFontStyle { get; set; }
        public ControlStyle DefaultControlStyle { get; set; }
        private SpriteBatch sb;

        private readonly MouseListener mouse = ServiceProvider.GetService<MouseListener>();
        private IInteractive hoveredControl { get; set; }
        private IInteractive pressedControl { get; set; }
        private IDraggable movingControl { get; set; }

        #endregion


        #region [ Add / Remove Controls ]
        public void AddControl(Control control)
        {
            //TODO: positioning
            // for right now I'm just going to pass this through for testing
            if (control != null)
            {
                controls.AddControl(control);
            }
        }
        #endregion

        #region [ Movement ]
        private void moveEnd(object sender, MouseEventArgs e)
        {
            if (movingControl != null)
            {
                movingControl.DragEnd(e);
                movingControl = null;
            }
        }

        private void move(object sender, MouseEventArgs e)
        {
            if (movingControl != null)
            {
                movingControl.Drag(e);
            }
        }

        private void moveCheck(object sender, MouseEventArgs e)
        {
            IFormObject c = controls.GetControlAtPoint(e.Position);
            if (movingControl == null && c != null)
            {
                if (c is IDraggable)
                {
                    movingControl = (IDraggable)c;
                    movingControl.DragStart(e);
                }
            }
        }
        #endregion


        #region [ Hover ]
        private void Hover(object sender, MouseEventArgs e)
        {
            IFormObject c = controls.GetControlAtPoint(e.Position);
            if (c != null)
            {
                ServiceProvider.GetService<DevConsole>().Write("Have a thing");
            }
            if (hoveredControl != null)
            {
                if (hoveredControl != (IFormObject)c)
                {
                    hoveredControl.MouseOut(e);
                }
            }
            if (c != null)
            {
                if (c is IInteractive)
                {
                    hoveredControl = (IInteractive)c;
                    hoveredControl.MouseOver(e);
                }
            }
        }
        #endregion


        #region [ Press ]
        private void Press(object sender, MouseEventArgs e)
        {
            IFormObject c = controls.GetControlAtPoint(e.Position);
            if (c != null)
            {
                if (c is IInteractive)
                {
                    pressedControl = (IInteractive)c;
                    pressedControl.Press(e);
                }
            }
        }
        #endregion


        #region [ Click ]
        private void Click(object sender, MouseEventArgs e)
        {
            IFormObject c = controls.GetControlAtPoint(e.Position);

            if (pressedControl != null)
            {
                if (c != null && c == pressedControl)
                {
                    pressedControl.Click(e);
                    pressedControl = null;
                }
            }
          //  pressedControl = null;
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
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Update(gt);
            }
        }
        #endregion


        #region [ Draw ]
        public void Draw(GameTime gt)
        {
            sb.Begin();
            for (int i = 0; i < controls.Count; i++)
            {
                controls[i].Draw(sb);
            }
            sb.End();
        }
        #endregion
    }
}
