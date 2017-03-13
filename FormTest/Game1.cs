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
        public GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;
        ControlManager manager;
        public readonly int ScreenWidthStart = 1280;
        public readonly int ScreenHeightStart = 720;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = ScreenWidthStart;
            Graphics.PreferredBackBufferHeight = ScreenHeightStart;
            Graphics.ApplyChanges();

            Window.IsBorderless = false;
            Window.AllowUserResizing = false;   //TODO: Allow Window Resizing

            // Service Provider
            ServiceProvider.Initialize(this);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
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
            manager = new ControlManager(GraphicsDevice);


            // Button:
            Button btn = new Button("DO NOT PUSH", buttonStyle);
            btn.OnClicked += btn_click;
            btn.Position = new Vector2(100, 100);
            

            // Label:  will re-use the same FontStyle as buttons
            Label myLabel = new Label("Here is a longer string of text to evaluate.", buttonStyle.FontStyle);
            myLabel.Position = new Vector2(200, 200);
            manager.Add(myLabel);


            // Panel:
            ControlStyle panelStyle = new ControlStyle(Assets.Terminal);
            panelStyle.FontStyle = buttonStyle.FontStyle;
            Panel myPanel = new Panel(panelStyle);
            btn.AnchorTo(myPanel, PositionType.Inside_Top_Left, 10, 30, AnchorType.Bounds);
            manager.Add(myPanel);
            manager.Add(btn);

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

        private void btn_click(object sender, EventArgs e)
        {
            Console.WriteLine("click");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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

            base.Update(gameTime);
        }


        #region [ Draw ]
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30,30,30));

            // DRAW CONTROL MANAGER
            manager.Draw(gameTime);

            // Service Provider:  Last so that mouse on top:
            ServiceProvider.Draw(gameTime);

            base.Draw(gameTime);
        }
        #endregion

    }
}
