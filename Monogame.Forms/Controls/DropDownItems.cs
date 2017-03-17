using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class DropDownItems
    {
        #region [ Constructor ]
        public DropDownItems(DropDown owner, Color backcolor)
        {
            _owner = owner;

            // Determine backcolor on image:

            var gfx = ServiceProvider.Graphics;
            _px = new Texture2D(gfx, 1, 1);
            Color[] colorData = { Color.White };
            _px.SetData<Color>(colorData);

        }
        #endregion


        #region [ Members ]
        private DropDown _owner;
        private Texture2D _px;
        public Color BackColor { get; set; }

        public int ItemHeight { get; private set; }
        #endregion


        public void Refresh()
        {
            ItemHeight = CalculateItemHeight();
        }

        private int CalculateItemHeight()
        {
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return (int)Math.Ceiling(_owner.Style.FontStyle.Font.MeasureString(testString).Y);
        }


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region [ Draw ]
        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
