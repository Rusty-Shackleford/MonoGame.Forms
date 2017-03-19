using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections;
using System.Collections.ObjectModel;


namespace MonoGame.Forms.Controls
{
    public class DropDown : Control
    {
        #region [ Constructor ]
        public DropDown(IList items, ControlStyle dropDownStyle, ControlStyle buttonStyle) : base()
        {
            if (dropDownStyle == null || buttonStyle == null)
            {
                throw new NotSupportedException("You must provide a style for this DropDown.");
            }
            Style = dropDownStyle;
            _dropDownButton = new Button(buttonStyle);
            _dropDownButton.AnchorTo(this, Anchoring.PositionType.Outside_Left_Top, 0, 0, Anchoring.AnchorType.Bounds);

            Items = new ObservableCollection<string>();
            Items = (ObservableCollection<string>)items;

            _render = new DropDownRenderer(this);
        }
        #endregion


        #region [ Members ]
        public ObservableCollection<string> Items;
        private DropDownRenderer _render;
        private Button _dropDownButton;
        public bool Open { get; private set; }

        public string FirstItemText { get; set; }

        /// <summary>
        /// The maximum number of entries to display in the dropdown before scrolling.
        /// </summary>
        public int MaxDropDownItems { get; set; }
        #endregion


        #region [ Events ]
        public override void Click(MouseEventArgs e)
        {
            // If we were already active, check for a click on one of
            // our items

            // If an item was clicked, change index to that, send event
            // Close the drawer
            // Hide FirstItem


            // Will set pressed to false, initiate event
            base.Click(e);


        }
        #endregion


        #region [ Item Click ]

        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
