using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
namespace MonoGame.Forms.Services
{
    public class MouseCursor : IUpdate, IDraw
    {
        #region [ Constructor ]
        public MouseCursor(GraphicsDevice graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);
            Visible = false;
            Enabled = true;
        }
        #endregion


        #region [ Field: Cursor ]
        private Texture2D _texture;

        private Texture2D _cursorTexture;
        public Texture2D CursorTexture
        {
            get { return _cursorTexture; }
            set
            {
                _cursorTexture = value;
                _texture = value;
                Visible = true;
            }
        }


        public void SetCursor(Texture2D texture)
        {
            _texture = texture;
            if (CursorTexture == null)
            {
                CursorTexture = texture;
            }
        }

        public void ResetCursor()
        {
            _texture = _cursorTexture;
        }
        #endregion


        private Vector2 _position;
        public Vector2 Position { get { return _position; } }
        private SpriteBatch _spriteBatch;
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public byte Alpha = 255;

        Color _color
        {
            get { return new Color(255, 255, 255, Alpha); }
        }


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                var currentMouseState = Mouse.GetState();
                _position = new Vector2(currentMouseState.Position.X, currentMouseState.Position.Y);
            }
        }
        #endregion


        #region [ Method: Draw ]
        public void Draw(GameTime gameTime)
        {
            if (Visible && _texture != null)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(texture: _texture, position: _position, color: _color);
                _spriteBatch.End();
            }
        }
        #endregion
    }
}
