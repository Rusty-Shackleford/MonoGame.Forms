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
            controls = new ControlSet();
            sb = new SpriteBatch(gd);

            mouse.MouseMoved += Hover;
            mouse.MouseDown += Press;
            mouse.MouseUp += Click;
        }
        #endregion


        #region [ Members ]
        private ControlSet controls;
        public FontStyle DefaultFontStyle { get; set; }
        public ControlStyle DefaultControlStyle { get; set; }
        private SpriteBatch sb;

        private readonly MouseListener mouse = ServiceProvider.GetService<MouseListener>();
        private Control hoveredControl { get; set; }
        private Control pressedControl { get; set; }

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


        #region [ Hover ]
        private void Hover(object sender, MouseEventArgs e)
        {
            Control c = controls.GetControlAtPoint(e.Position);

            if (hoveredControl != null)
            {
                if (hoveredControl != c)
                {
                    hoveredControl.MouseOut(e);
                }
            }
            if (c != null)
            {
                hoveredControl = c;
                hoveredControl.MouseOver(e);
            }
        }
        #endregion


        #region [ Press ]
        private void Press(object sender, MouseEventArgs e)
        {
            Control c = controls.GetControlAtPoint(e.Position);
            if (c != null)
            {
                pressedControl = c;
                pressedControl.Press(e);
            }
        }
        #endregion


        #region [ Click ]
        private void Click(object sender, MouseEventArgs e)
        {
            Control c = controls.GetControlAtPoint(e.Position);

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
