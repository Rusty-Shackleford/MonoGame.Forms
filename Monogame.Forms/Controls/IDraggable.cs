using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public interface IDraggable
    {
        bool Dragging { get; }
        Vector2 OriginalPosition { get; }
        void CancelDrag();
        void DragStart(MouseEventArgs e);
        void DragEnd(MouseEventArgs e);
        void Drag(MouseEventArgs e);
    }
}
