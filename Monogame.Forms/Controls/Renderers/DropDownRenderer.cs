using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using MonoGame.Extended.InputListeners;

namespace MonoGame.Forms.Controls.Renderers
{
    public class DropDownRenderer : ControlRenderer
    {
        #region [ Constructor ]
        public DropDownRenderer(DropDown control) : base(control)
        {
            _items = new List<Button>();
            _owner = (DropDown)owner;
            _owner.Items.CollectionChanged += UpdateItemDisplay;
        }
        #endregion


        #region [ Members ]
        private List<Button> _items;
        private Button _currentSelection;

        DropDown _owner;
        #endregion

        #region [ Update Item Display ]
        private void UpdateItemDisplay(object sender, NotifyCollectionChangedEventArgs e)
        {
            _items.Clear();
            for (int i = 0; i < _owner.Items.Count; i++)
            {
                var btn = new Button(_owner.Items[i], _owner.Style);
                btn.AnchorTo(_owner, Anchoring.PositionType.Below_Left, 0, 0, Anchoring.AnchorType.Bounds);

            }
        }
        #endregion

        #region [ Show / Hide ]
        public void Show()
        {

        }

        public void Hide()
        {

        }

        public Button GetItemClicked(MouseEventArgs e)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].EffectiveBounds.Contains(e.Position))
                {
                    _currentSelection = _items[i];
                    return _currentSelection;
                }
            }
            return null;
        }


        private void ClearPreviousSelection()
        {

        }
        #endregion

        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
        #endregion


    }
}
