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
            _input = new BoundingInputHandler(this);
        }
        #endregion


        #region [ Members ]
        public Contents Contents { get; protected set; }
        public event EventHandler OnItemAdded;
        public event EventHandler OnItemRemoved;

        private BoundingInputHandler _input;

        private SpriteBatch _sb;

        public AnchoredRectangle ContentArea { get; set; }
        #endregion


        #region [ Add / Remove Controls ]
        public void Add(IContainable item)
        {
            if (item != null)
            {
                Contents.Add(item);
                OnItemAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool Remove(IContainable item)
        {
            if (Contents.Remove(item))
            {
                OnItemRemoved?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
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
