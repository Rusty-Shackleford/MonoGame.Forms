﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Anchoring;
using MonoGame.Forms.Controls.Styles;

namespace MonoGame.Forms
{
    public interface IContainable : IAnchorable
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
