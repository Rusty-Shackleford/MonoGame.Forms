using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Services
{
    public static class KVM
    {
        #region [ Constructor ]
        public static void Load(KeyboardListener kbd, MouseListener mouse, Game game)
        {
            _game = game;
            Keyboard = kbd;
            Mouse = mouse;
            Graphics = game.GraphicsDevice;
            View = new GameViewport(game);
        }
        #endregion


        #region [ Members ]
        private static Game _game;
        public static KeyboardListener Keyboard { get; private set; }
        public static MouseListener Mouse { get; private set; }
        public static GameViewport View { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        #endregion
    }
}
