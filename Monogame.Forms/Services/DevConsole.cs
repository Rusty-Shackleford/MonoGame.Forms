using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.InputListeners;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using System;
using MonoGame.Extended.BitmapFonts;



namespace MonoGame.Forms.Services
{
    public class DevConsole: IUpdate, IDraw
    {

        #region [ Constructor ]
        public DevConsole(GraphicsDevice graphics, Keys key, Panel panel)
        {
            _sb = new SpriteBatch(graphics);
            kbd.KeyReleased += toggle;
            _key = key;
            Panel = panel;
        }
        #endregion


        #region [ Members ]
        public bool Visible { get; set; }
        public Vector2 Position { get; set; }

        public Panel Panel { get; set; }
        private Keys _key;

        private string _text;
        public string Text { get { return _text; } }
        public Vector2 TextStartPosition { get; set; }

        private SpriteBatch _sb;
        private readonly KeyboardListener kbd = ServiceProvider.GetService<KeyboardListener>();
        #endregion


        public void Write(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _text += "\n";
                _text += message;
            }
        }

        private void toggle(object sender, KeyboardEventArgs e)
        {
            if (e.Key == _key)
            {
                Visible = !Visible;
            }
        }


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            // Not sure about moving yet...
            Panel.Position = Position;
        }
        #endregion


        #region [ Draw ]
        public void Draw(GameTime gameTime)
        {
            _sb.Begin();
            if (Visible)
            {
                if (Panel != null)
                {
                    Panel.Draw(_sb);
                }
                if (!string.IsNullOrEmpty(_text))
                {
                    _sb.DrawString(Panel.Style.FontStyle.Font, _text, TextStartPosition, Panel.Style.FontStyle.Color);
                }
            }
            _text = "";
            _sb.End();
        }
        #endregion


    }
}
