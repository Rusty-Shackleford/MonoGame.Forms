using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public class FormObjectList
    {
        public FormObjectList()
        {
            _children = new List<IFormObject>();
        }


        private List<IFormObject> _children;


        public IEnumerator<IFormObject> GetEnumerator()
        {
            return ((IEnumerable<IFormObject>)_children).GetEnumerator();
        }



        public IFormObject this[int index]
        {
            get { return _children[index]; }
        }

        public int Count
        {
            get { return _children.Count; }
        }

        public void AddControl(IFormObject control)
        {
            _children.Add(control);
        }


        public void RemoveControl(IFormObject control)
        {
            _children.Remove(control);
        }


        public IFormObject GetControlAtPoint(Point point)
        {
            return _children.LastOrDefault(e => e.InteractiveBounds.Contains(point));
        }


        public List<IFormObject> GetControlsAtPoint(Point point)
        {
            return _children.FindAll(e => e.InteractiveBounds.Contains(point));
        }


        private IFormObject Find(Func<IFormObject, IFormObject, bool> compare)
        {
            if (_children.Count == 0)
                return default(IFormObject);

            if (_children.Count == 1)
                return _children[0];

            IFormObject found = _children[0];
            for (int i = 1; i < _children.Count; i++)
            {
                if (compare(_children[i], found))
                {
                    found = _children[i];
                }
            }
            return found;
        }


        public IFormObject RightmostControl()
        {
            return Find((a, b) => a.Bounds.Right > b.Bounds.Right);
        }


        public IFormObject LeftmostControl()
        {
            return Find((a, b) => a.Bounds.Left < b.Bounds.Left);
        }


        public IFormObject HighestControl()
        {
            return Find((a, b) => a.Bounds.Top < b.Bounds.Top);
        }


        public IFormObject LowestControl()
        {
            return Find((a, b) => a.Bounds.Bottom > b.Bounds.Bottom);
        }
    }
}
