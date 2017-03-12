using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public class ControlSet
    {
        public ControlSet()
        {
            _children = new List<Control>();
        }


        private List<Control> _children;


        public IEnumerator<Control> GetEnumerator()
        {
            return ((IEnumerable<Control>)_children).GetEnumerator();
        }



        public Control this[int index]
        {
            get { return _children[index]; }
        }

        public int Count
        {
            get { return _children.Count; }
        }

        public void AddControl(Control control)
        {
            _children.Add(control);
        }


        public void RemoveControl(Control control)
        {
            _children.Remove(control);
        }


        public Control GetControlAtPoint(Point point)
        {
            return _children.LastOrDefault(e => e.InteractiveBounds.Contains(point));
        }


        public List<Control> GetControlsAtPoint(Point point)
        {
            return _children.FindAll(e => e.InteractiveBounds.Contains(point));
        }


        private Control Find(Func<Control, Control, bool> compare)
        {
            if (_children.Count == 0)
                return default(Control);

            if (_children.Count == 1)
                return _children[0];

            Control found = _children[0];
            for (int i = 1; i < _children.Count; i++)
            {
                if (compare(_children[i], found))
                {
                    found = _children[i];
                }
            }
            return found;
        }


        public Control RightmostControl()
        {
            return Find((a, b) => a.Bounds.Right > b.Bounds.Right);
        }


        public Control LeftmostControl()
        {
            return Find((a, b) => a.Bounds.Left < b.Bounds.Left);
        }


        public Control HighestControl()
        {
            return Find((a, b) => a.Bounds.Top < b.Bounds.Top);
        }


        public Control LowestControl()
        {
            return Find((a, b) => a.Bounds.Bottom > b.Bounds.Bottom);
        }
    }
}
