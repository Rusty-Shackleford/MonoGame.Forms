using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms
{
    public interface IScroll : IAnchorable
    {
        ContentManager ContentManager { get; }
        ControlStyle Style { get; }
        bool Scrolls { get; }
    }
}
