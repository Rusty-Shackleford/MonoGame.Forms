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
        ContentManager WindowContentManager;
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
            buttonStyle.FontStyle = new FontStyle(Assets.Font_MineCraftia11, Color.White);


            // Control Manager - updates / draws all controls
            WindowContentManager = new ContentManager(
                GraphicsDevice,
                new AnchoredRectangle(
                    0, 0,
                    Graphics.PreferredBackBufferWidth,
                    Graphics.PreferredBackBufferHeight
                )
            );


            // Panel:
            ControlStyle panelStyle = new ControlStyle(Assets.Terminal_Bg);
            panelStyle.FontStyle = buttonStyle.FontStyle;
            panelStyle.ScrollerStyle = Assets.ScrollerStyle;


            myPanel = new Panel("Test Window", panelStyle, true, true);
            myPanel.MoveTo(new Vector2(200, 350));
            myPanel.DragAreaOffset = new Rectangle(0, 0, myPanel.Width, 23);
            myPanel.SetContentArea(10, 20, myPanel.Width - 10, myPanel.Height - 20);


            // Button:
            Button btn = new Button("Click Me", buttonStyle);
            btn.OnClicked += btn_click;
            btn.AnchorTo(myPanel, PositionType.Inside_Top_Left, 10, 30, AnchorType.Bounds);

            // Label:  will re-use the same FontStyle as buttons
            Label myLabel1 =
                new Label("Scroll Down?", buttonStyle.FontStyle);
            myLabel1.AnchorTo(btn, PositionType.Below_Left, 0, 50, AnchorType.Bounds);

            Label myLabel2 =
                new Label("-- That's All Folks! --", buttonStyle.FontStyle);
            myLabel2.AnchorTo(myLabel1, PositionType.Below_Left, 0, 150, AnchorType.Bounds);

            myPanel.ContentManager.Add(btn);
            myPanel.ContentManager.Add(myLabel1);
            myPanel.ContentManager.Add(myLabel2);

            WindowContentManager.Add(myPanel);      // Important :)


            // FPS Counter:
           // ServiceProvider.AddService(new FramesPerSecondCounter(100));

            // Dev Console
            ControlStyle styleDevConsole = new ControlStyle(Assets.DevConsole_Bg);
            styleDevConsole.FontStyle = buttonStyle.FontStyle;

            var console = new DevConsole(GraphicsDevice, Keys.OemTilde, new Panel("DevConsole", styleDevConsole));
            console.TextStartPosition = new Vector2(15, 20);

            ServiceProvider.AddService(console);

            // Cursor - note this needs to be added last
            ServiceProvider.AddService(new Cursor(this));
            ServiceProvider.GetService<Cursor>().SetCursor(Assets.Cursor);

        }
        #endregion


        #region [ Button ]
        private void btn_click(object sender, EventArgs e)
        {
        }
        #endregion


        #region [ Unload ]
        protected override void UnloadContent() { }
        #endregion


        #region [ Update ]
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            // Write FPS to console
            // string fps = ServiceProvider.GetService<FramesPerSecondCounter>().CurrentFramesPerSecond.ToString();
            //  ServiceProvider.GetService<DevConsole>().Write("FPS:  " + fps);

            // Service Provider
            ServiceProvider.Update(gameTime);

            // UPDATE CONTROL MANAGER
            WindowContentManager.Update(gameTime);

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
            WindowContentManager.Draw();

            // Service Provider:  Last so that mouse on top:
            ServiceProvider.Draw(gameTime);

            base.Draw(gameTime);
        }
        #endregion

    }
}
