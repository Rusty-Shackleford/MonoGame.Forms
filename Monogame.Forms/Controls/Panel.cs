﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Renderers;
using MonoGame.Forms.Controls.Scrollers;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Renderers;
using MonoGame.Forms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls
{
    public class Panel : Control, IContainer, IScroll
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
            Width = Style.TextureDefault.Width;
            Height = Style.TextureDefault.Height;

            if (!string.IsNullOrEmpty(title))
            {
                label = new Label(title, Style.FontStyle);
                label.AnchorTo(this, Anchoring.PositionType.Inside_Top_Left, 3, 8, Anchoring.AnchorType.Bounds);
            }

            render = new ControlRenderer(this);

            AnchorToArgs arg = new AnchorToArgs(this, PositionType.Inside_Top_Left, 0, 0, AnchorType.Bounds);
            ContentManager = new ContentManager(ServiceProvider.Graphics, new AnchoredRectangle(Rectangle.Empty, arg));


            if (scrolls)
            {
                if (Style.ScrollerStyle == null)
                {
                    throw new NotSupportedException("ControlStyle must contain ScrollerStyle to enable scrolling.");
                }
                Scrolls = true;
                scroller = new Scroller(this);
                _contentsRenderer = new RenderInScissor(ServiceProvider.Graphics);
            }

        }
        #endregion


        #region [ Members ]
        protected ControlRenderer render;
        protected Label label;
        public bool Initialized { get; protected set; }

        public void SetContentArea(int offsetX, int offsetY, int width, int height)
        {
            ContentManager.ContentArea.Width = width;
            ContentManager.ContentArea.Height = height;
            ContentManager.ContentArea.Position = new Vector2(Position.X + offsetX, Position.Y + offsetY);
        }

        public bool Scrolls { get; set; }
        public ContentManager ContentManager { get; protected set; }
        protected Scroller scroller;
        protected RenderInScissor _contentsRenderer;
        #endregion


        #region [ Initialize ]
        public virtual void Initialize()
        {
            Initialized = true;
        }
        #endregion


        #region [ Event Handling ]
        private void WheelMove(object sender, MouseEventArgs e)
        {
            //if (Bounds.Contains(e.Position) && e.ScrollWheelDelta != 0)
            //{
            //    if (CanScroll)
            //    {
            //        if (Math.Abs(e.ScrollWheelDelta) <= 25)
            //        {
            //            _layout.Move(e.ScrollWheelDelta);
            //        }
            //        else
            //        {
            //            // One "click" scroll of MY mouse returns a huge amount.
            //            // Normalize it a bit to keep it from going crazy.
            //            if (e.ScrollWheelDelta > 0)
            //            {
            //                _layout.Move(25);
            //            }
            //            else
            //            {
            //                _layout.Move(-25);
            //            }
            //        }
            //    }
            //}
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
                if (Scrolls)
                {
                    
                    scroller.Draw(spriteBatch);
                    spriteBatch.End();
                    _contentsRenderer.Draw(ContentManager);
                    spriteBatch.Begin();
                }
                else
                {
                    // Draw my contents:
                    ContentManager.Draw();
                }
            }
        }
        #endregion

    }
}
