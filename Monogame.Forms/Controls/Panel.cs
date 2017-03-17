using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Scrollers;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class Panel : Control, IContainer
    {

        #region [ Constructor ]
        public Panel(ControlStyle style) : this("", style) { }
        public Panel(string title, ControlStyle style, bool scrolls = false, bool moves = false) : base()
        {
            if (style == null)
            {
                throw new NotSupportedException("A style must be provided for this panel.");
            }

            if (moves)
            {
                EnableDragging = true;
            }

            Style = style;
            if (!string.IsNullOrEmpty(title))
            {
                label = new Label(title, Style.FontStyle);
                label.AnchorTo(this, Anchoring.PositionType.Inside_Top_Left, 3, 8, Anchoring.AnchorType.Bounds);
            }

            render = new ControlRenderer(this);

            ContentManager = new ContentManager(ServiceProvider.Graphics, new Viewport(ContentBounds));

            if (scrolls)
            {
                if (Style.ScrollerStyle == null)
                {
                    throw new NotSupportedException("ControlStyle must contain ScrollerStyle to enable scrolling.");
                }
                scroller = new Scroller(ContentManager.Contents, Style.ScrollerStyle);
            }

        }
        #endregion


        #region [ Members ]
        protected ControlRenderer render;
        protected Label label;
        public bool Initialized { get; protected set; }

        public Rectangle ContentBounds { get; set; }

        public ContentManager ContentManager { get; protected set; }
        protected Scroller scroller;
        #endregion


        #region [ Initialize ]
        public virtual void Initialize()
        {
            Initialized = true;
        }
        #endregion


        #region [ Update ]
        public override void Update(GameTime gameTime)
        {
            ContentManager.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                render.Draw(spriteBatch);
                if (label != null)
                {
                    label.Draw(spriteBatch);
                }

                // Draw my contents:
                ContentManager.Draw();
            }
        }
        #endregion

    }
}
