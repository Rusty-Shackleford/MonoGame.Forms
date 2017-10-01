using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public interface IDraggable
    {
        Rectangle DragBounds { get; }
        bool Dragging { get; }
        Vector2 OriginalPosition { get; }
        void CancelDrag();
        void DragStart(MouseEventArgs e);
        void DragEnd(MouseEventArgs e);
        void Drag(MouseEventArgs e);
    }
}
