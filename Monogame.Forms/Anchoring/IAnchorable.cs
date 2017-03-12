using Microsoft.Xna.Framework;
using System;

namespace MonoGame.Forms.Anchoring
{
    public interface IAnchorable
    {
        AnchorComponent Anchor { get; }

        Vector2 Position { get; set; }

        Rectangle Bounds { get; }

        //Rectangle VirtualBounds { get; }

        event EventHandler OnPositionChanged;

        void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds);
    }
}