using Microsoft.Xna.Framework;

namespace MonoGame.Forms.Anchoring
{
    //public class PositionChangedArgs
    //{
    //    public Vector2 OldPosition { get; private set; }
    //    public Vector2 NewPosition { get; private set; }
    //    public Vector2 DistanceMoved => NewPosition - OldPosition;

    //    public PositionChangedArgs(Vector2 oldPosition, Vector2 newPosition)
    //    {
    //        OldPosition = oldPosition;
    //        NewPosition = newPosition;

    //    }
    //}
    public class PositionChangedArgs
    {
        public Vector2 DistanceMoved { get; private set; }

        public PositionChangedArgs(Vector2 distanceMoved)
        {
            DistanceMoved = distanceMoved;
        }
    }
}
