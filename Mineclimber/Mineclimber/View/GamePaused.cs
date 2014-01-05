using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class GamePaused
    {
        private SpriteBatch spriteBatch;
        private Texture2D pauseScreen;

        public GamePaused(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            pauseScreen = content.Load<Texture2D>("Starting screen");
        }

        internal void DrawPausedScreen(Camera camera, Vector2 size)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, (int)size.X, (int)size.Y);

            spriteBatch.Begin();
            spriteBatch.Draw(pauseScreen, tileRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
