using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Forms;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using MonoGame.Forms.Services;
using MonoGame.Forms.Anchoring;
using MonoGame.Extended;

namespace FormTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region [ Members ]
        public GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;
        ContentManager manager;
        public readonly int ScreenWidthStart = 1280;
        public readonly int ScreenHeightStart = 720;

        Panel myPanel;

        #endregion


        #region [ Constructor ]
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion


        #region [ Init ]
        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = ScreenWidthStart;
            Graphics.PreferredBackBufferHeight = ScreenHeightStart;
            Graphics.ApplyChanges();

            Window.IsBorderless = false;
            Window.AllowUserResizing = false;

            // Service Provider
            ServiceProvider.Initialize(this);

            base.Initialize();
        }
        #endregion


        #region [ Load Content ]
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Assets
            Assets.LoadContent(Content);

            // A style for buttons to use.
            ControlStyle buttonStyle = new ControlStyle(
                Assets.Button,
                Assets.ButtonHover,
                Assets.ButtonPressed,
                null,
                Rectangle.Empty
                );
            buttonStyle.FontStyle = new FontStyle(Assets.Plumbis11, Color.White);


            // Control Manager - updates / draws all controls
            manager = new ContentManager(
                GraphicsDevice,
                new AnchoredRectangle(
                    0, 0,
                    Graphics.PreferredBackBufferWidth,
                    Graphics.PreferredBackBufferHeight
                )
            );




            // Panel:
            ControlStyle panelStyle = new ControlStyle(Assets.Terminal);
            panelStyle.FontStyle = buttonStyle.FontStyle;
            panelStyle.ScrollerStyle = new ScrollerStyle(
                Assets.ScrollBar, Assets.ScrollThumb, Assets.ScrollThumbHover);
            panelStyle.ScrollerStyle.ScrollThumbOffset = new Vector2(4, 0);
            panelStyle.ScrollerStyle.ScrollThumbWidth = 9;
            panelStyle.ScrollerStyle.ScrollBarWidth = 18;

            myPanel = new Panel("test", panelStyle, true, true);
            myPanel.MoveTo(new Vector2(200, 350));
            myPanel.DragAreaOffset = new Rectangle(0, 0, myPanel.Width, 23);
            myPanel.SetContentArea(10, 20, myPanel.Width - 10, myPanel.Height - 20);


            // Button:
            Button btn = new Button("DO NOT PUSH", buttonStyle);
            btn.OnClicked += btn_click;
            btn.AnchorTo(myPanel, PositionType.Inside_Top_Left, 10, 30, AnchorType.Bounds);

            Button btn2 = new Button("Button two", buttonStyle);
            btn2.OnClicked += btn_click2;
            btn2.AnchorTo(btn, PositionType.Below_Center, 0, 300, AnchorType.Bounds);

            // Label:  will re-use the same FontStyle as buttons
            Label myLabel =
                new Label("Here is a longer string of text to evaluate.", buttonStyle.FontStyle);
            myLabel.AnchorTo(btn2, PositionType.Below_Left, 0, 300, AnchorType.Bounds);

            myPanel.ContentManager.Add(btn);
            myPanel.ContentManager.Add(btn2);
            myPanel.ContentManager.Add(myLabel);
            manager.Add(myPanel);


            // FPS Counter:
            ServiceProvider.AddService(new FramesPerSecondCounter(100));

            // Dev Console
            ControlStyle styleDevConsole = new ControlStyle(Assets.DevConsole);
            styleDevConsole.FontStyle = buttonStyle.FontStyle;

            var console = new DevConsole(GraphicsDevice, Keys.OemTilde, new Panel("DevConsole", styleDevConsole));
            console.TextStartPosition = new Vector2(15, 20);

            ServiceProvider.AddService(console);

            // Cursor - note this needs to be added last
            ServiceProvider.AddService(new MouseCursor(GraphicsDevice));
            ServiceProvider.GetService<MouseCursor>().SetCursor(Assets.Cursor);

        }
        #endregion


        #region [ Button ]
        private void btn_click(object sender, EventArgs e)
        {
            var console = ServiceProvider.GetService<DevConsole>();
            console.Write("Button One", 1);
        }
        private void btn_click2(object sender, EventArgs e)
        {
            var console = ServiceProvider.GetService<DevConsole>();
            console.Write("Button Two", 1);
        }
        #endregion


        #region [ Unload ]
        protected override void UnloadContent() { }
        #endregion


        #region [ Update ]
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Write FPS to console
            string fps = ServiceProvider.GetService<FramesPerSecondCounter>().CurrentFramesPerSecond.ToString();
            ServiceProvider.GetService<DevConsole>().Write("FPS:  " + fps);

            // Service Provider
            ServiceProvider.Update(gameTime);

            // UPDATE CONTROL MANAGER
            manager.Update(gameTime);

            var console = ServiceProvider.GetService<DevConsole>();

            //console.Write("Panel ContentMgr Content Area Bounds: " + myPanel.ContentManager.ContentArea.Bounds.ToString());
            //console.Write("Panel Position: " + myPanel.Position.ToString());
            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 30));

            // DRAW CONTROL MANAGER
            manager.Draw();

            // Service Provider:  Last so that mouse on top:
            ServiceProvider.Draw(gameTime);

            base.Draw(gameTime);
        }
        #endregion

    }
}
