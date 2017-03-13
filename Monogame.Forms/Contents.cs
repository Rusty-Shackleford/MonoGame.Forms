using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MonoGame.Forms
{
    public class Contents
    {
        #region [ Constructor ]
        public Contents()
        {
            _contents = new List<IContainable>();
        }
        #endregion


        #region [ Members ]
        private List<IContainable> _contents;
        #endregion


        #region [ List ]
        public IEnumerator<IContainable> GetEnumerator()
        {
            return ((IEnumerable<IContainable>)_contents).GetEnumerator();
        }

        public IContainable this[int index]
        {
            get { return _contents[index]; }
        }

        public int Count
        {
            get { return _contents.Count; }
        }
        #endregion
        

        #region [ Add / Remove ]
        public void Add(IContainable control)
        {
            _contents.Add(control);
        }

        public void Remove(IContainable control)
        {
            _contents.Remove(control);
        }
        #endregion


        #region [ Queries ]
        public IContainable GetControlAtPoint(Point point)
        {
            return _contents.LastOrDefault(e => e.InteractiveBounds.Contains(point));
        }


        public List<IContainable> GetControlsAtPoint(Point point)
        {
            return _contents.FindAll(e => e.InteractiveBounds.Contains(point));
        }


        private IContainable Find(Func<IContainable, IContainable, bool> compare)
        {
            if (_contents.Count == 0)
                return default(IContainable);

            if (_contents.Count == 1)
                return _contents[0];

            IContainable found = _contents[0];
            for (int i = 1; i < _contents.Count; i++)
            {
                if (compare(_contents[i], found))
                {
                    found = _contents[i];
                }
            }
            return found;
        }


        public IContainable RightmostControl()
        {
            return Find((a, b) => a.Bounds.Right > b.Bounds.Right);
        }


        public IContainable LeftmostControl()
        {
            return Find((a, b) => a.Bounds.Left < b.Bounds.Left);
        }


        public IContainable HighestControl()
        {
            return Find((a, b) => a.Bounds.Top < b.Bounds.Top);
        }


        public IContainable LowestControl()
        {
            return Find((a, b) => a.Bounds.Bottom > b.Bounds.Bottom);
        }
        #endregion
    }
}
