namespace MonoGame.Forms
{
    public struct BoxProperty
    {
        #region [ Constructors ]
        public BoxProperty(int top, int right, int bottom, int left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
        public BoxProperty(int top, int rightleft, int bottom)
        {
            Top = top;
            Right = rightleft;
            Bottom = bottom;
            Left = rightleft;
        }
        public BoxProperty(int topbottom, int leftright)
        {
            Top = topbottom;
            Right = leftright;
            Bottom = topbottom;
            Left = leftright;
        }
        public BoxProperty(int allsides)
        {
            Top = allsides;
            Right = allsides;
            Bottom = allsides;
            Left = allsides;
        }
        #endregion

        #region [ Members ]
        public int Top;
        public int Right;
        public int Bottom;
        public int Left;
        #endregion

        #region [ Operators ]
        public override bool Equals(object obj)
        {
            if (obj is BoxProperty)
            {
                return Equals((BoxProperty)obj);
            }
            return false;
        }

        public bool Equals(BoxProperty other)
        {
            return this == other;
        }

        public static bool operator ==(BoxProperty value1, BoxProperty value2)
        {
            return
                value1.Top == value2.Top &&
                value1.Right == value2.Right &&
                value1.Bottom == value2.Bottom &&
                value1.Left == value2.Left;
        }

        public static bool operator !=(BoxProperty value1, BoxProperty value2)
        {
            return
                value1.Top != value2.Top ||
                value1.Right != value2.Right ||
                value1.Bottom != value2.Bottom ||
                value1.Left != value2.Left;
        }

        public override int GetHashCode()
        {
            return Top.GetHashCode() +
                Right.GetHashCode() +
                Bottom.GetHashCode() +
                Left.GetHashCode();
        }
        #endregion

        public static BoxProperty Zero { get { return new BoxProperty(); } }
    }
}
