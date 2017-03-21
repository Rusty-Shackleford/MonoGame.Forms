using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Forms.Anchoring;

namespace MonoGame.Forms.Controls.Scrollers
{
    public class ScrollBar : Control
    {
        #region [ Constructor ]
        public ScrollBar() : base()
        {
           // OnPositionChanged += testme;
        }

        private void testme(object sender, PositionChangedArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("ScrollBar OnPositionChanged: " + e.DistanceMoved.ToString());
        }
        #endregion


        #region [ Members ]

        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
