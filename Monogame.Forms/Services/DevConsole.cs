using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Controls.Styles;
using System;
using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;
using MonoGame.Extended.Timers;
using MonoGame.Extended.Input.InputListeners;

namespace MonoGame.Forms.Services
{
    public class DevConsole : IUpdate, IDraw
    {
        #region [ +Class: ConsoleMessage ]
        protected class ConsoleMessage
        {
            private BitmapFont Font { get; set; }
            public string Message { get; set; }
            public CountdownTimer Timer { get; set; }
            public bool Expired { get; private set; }
            public Vector2 Size { get { return Font.MeasureString(Message); } }

            public ConsoleMessage(string text, double time, BitmapFont font)
            {
                Message = text;
                Timer = new CountdownTimer(time);
                Timer.Completed += killmessage;
                Font = font;
            }

            private void killmessage(object sender, EventArgs e)
            {
                Expired = true;
            }
        }
        #endregion



        #region [ Constructor ]
        public DevConsole(GraphicsDevice graphics, Keys key, Panel panel)
        {
            _sb = new SpriteBatch(graphics);
            kbd.KeyReleased += toggle;
            _key = key;
            Panel = panel;
            _messages = new List<ConsoleMessage>();
        }
        #endregion


        #region [ Events ]
        private void toggle(object sender, KeyboardEventArgs e)
        {
            if (e.Key == _key)
            {
                Visible = !Visible;
            }
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

        private int _textHeight
        {
            get
            {
                int y = 0;
                string[] lines = _text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    y += (int)Panel.Style.FontStyle.Font.MeasureString(lines[i]).Height;
                }
                return y;
            }
        }


        private SpriteBatch _sb;
        private readonly KeyboardListener kbd = KVM.Keyboard;
        private List<ConsoleMessage> _messages;
        #endregion


        #region [ WRITE ]
        public void Write(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _text += "\n";
                _text += message;
            }
        }

        public void Write(string message, double time)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var consolemessage = new ConsoleMessage("\n" + message, time, Panel.Style.FontStyle.Font);
                _messages.Add(consolemessage);
            }
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            // Not sure about moving yet...
            Panel.Move(Position - Panel.Position);

            // Remove old messages
            RemoveExpiredMessages();

            // Update Timers
            UpdateMessages(gameTime);
        }
        #endregion


        #region [ Manage Messages ]
        private void UpdateMessages(GameTime gameTime)
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                _messages[i].Timer.Update(gameTime);
            }
        }
        private void RemoveExpiredMessages()
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                if (_messages[i].Expired)
                {
                    _messages.Remove(_messages[i]);
                }
            }
        }
        #endregion


        #region [ Draw ]
        public void Draw(GameTime gameTime)
        {
            Vector2 writePosition = TextStartPosition;

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
                    writePosition.Y += _textHeight + 2;
                }
                for (int i = 0; i < _messages.Count; i++)
                {
                    _sb.DrawString(Panel.Style.FontStyle.Font, _messages[i].Message, writePosition, Panel.Style.FontStyle.Color);
                    writePosition.Y += _messages[i].Size.Y + 2;
                }
            }
            _text = "";


            _sb.End();
        }
        #endregion


    }
}
