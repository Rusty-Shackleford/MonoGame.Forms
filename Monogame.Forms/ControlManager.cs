using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        #endregion


        #region [ Members ]
        private ControlSet controls;
        public FontStyle DefaultFontStyle { get; set; }
        public ControlStyle DefaultControlStyle { get; set; }
        private SpriteBatch sb;
        #endregion


        #region [ Add / Remove Controls ]
        public void AddControl(Control control)
        {
            //TODO: positioning
            // for right now I'm just going to pass this through for testing
            controls.AddControl(control);
        }
        #endregion

        //TODO: Dispose of Controls when dead?

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
