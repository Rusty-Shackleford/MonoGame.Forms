﻿using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Services;


//TODO: Up Next - Make this generic to handle scrollers items.
namespace MonoGame.Forms
{
    /// <summary>
    /// An input manager for controls which utilize Content
    /// Managers and Content Area for restricted input event areas.
    /// In theory this would reduce overhead by only triggering
    /// events when input is caught within the ContentArea
    /// </summary>
    public class BoundingInputManager : InputHandler
    {
        #region [ Constructor ]
        public BoundingInputManager(ContentManager contentManager) : 
            base(contentManager.Contents)
        {
            _contentManager = contentManager;
        }
        #endregion


        #region [ Members ]
        private readonly ContentManager _contentManager;

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


        protected override void MoveCheck(object sender, MouseEventArgs e)
        {
            if (_contentManager.ContentArea.Contains(e.Position))
            {
                base.MoveCheck(sender, e);
            }

        }
        #endregion


        #region [ Hover ]
        protected override void Hover(object sender, MouseEventArgs e)
        {
            if (_contentManager.ContentArea.Contains(e.Position))
            {
                base.Hover(sender, e);
            }
        }
        #endregion


        #region [ Press ]
        protected override void Press(object sender, MouseEventArgs e)
        {
            if (_contentManager.ContentArea.Contains(e.Position))
            {
                base.Press(sender, e);
            }
        }
        #endregion


        #region [ Click ]
        protected override void Click(object sender, MouseEventArgs e)
        {
            if (_contentManager.ContentArea.Contains(e.Position))
            {
                base.Click(sender, e);
            }
        }
        #endregion


    }
}
