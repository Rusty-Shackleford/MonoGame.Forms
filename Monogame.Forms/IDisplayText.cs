using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public interface IDisplayText
    {
        Vector2 Position { get; }
        FontStyle FontStyle { get; set; }
        string Text { get; set; }
    }
}
