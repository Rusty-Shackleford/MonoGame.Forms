using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Anchoring
{
    public class PositionChangedArgs
    {
        public Vector2 OldPosition { get; private set; }
        public Vector2 NewPosition { get; private set; }
        public Rectangle Bounds { get; private set; }
        public Vector2 DistanceMoved
        {
            get { return NewPosition - OldPosition; }
        }

        public PositionChangedArgs(Vector2 oldPosition, Vector2 newPosition, Rectangle newBounds)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Bounds = newBounds;
        }
    }
}
