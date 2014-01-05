using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class PauseScreen
    {
        private SpriteBatch spriteBatch;
        private Texture2D PauseScreenTexture;
        private GraphicsDevice graphicsDevice;

        /// <summary>
        /// Constructor, loads starting screen, spritebatch & camera
        /// </summary>
        /// <param name="graphicsDevice">The graphicsdevice</param>
        /// <param name="content">ContentManager</param>
        public PauseScreen(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            PauseScreenTexture = content.Load<Texture2D>("pauseScreen");
            this.graphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Draws the starting screen
        /// </summary>
        internal void DrawStartingScreen()
        {
            Rectangle rectangle = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(PauseScreenTexture, rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
