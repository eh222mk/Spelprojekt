using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class BetweenLevels
    {
        private SpriteBatch m_spriteBatch;
        private Texture2D m_startingScreenTexture;
        private Texture2D m_betweenLevelScreenTexture;
        private Texture2D m_gameFinishedTexture;
        private Camera m_camera;
        private GraphicsDevice m_graphicsDevice;
        private SpriteFont betweenLevelFont;
        private Texture2D m_gameOverTexture;

        /// <summary>
        /// Constructor, loads starting screen, spritebatch & camera
        /// </summary>
        /// <param name="graphicsDevice">The graphicsdevice</param>
        /// <param name="content">ContentManager</param>
        public BetweenLevels(GraphicsDevice graphicsDevice, ContentManager content)
        {
            m_graphicsDevice = graphicsDevice;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_startingScreenTexture = content.Load<Texture2D>("badStartingScreen");
            m_betweenLevelScreenTexture = content.Load<Texture2D>("completedLevel");
            m_gameFinishedTexture = content.Load<Texture2D>("GameFinished");
            m_gameOverTexture = content.Load<Texture2D>("GameOver");
            betweenLevelFont = content.Load<SpriteFont>("BetweenLevelFont");
            m_camera = new Camera(new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
        }

        /// <summary>
        /// Draws the starting screen
        /// </summary>
        internal void DrawStartingScreen()
        {
            DrawTexture(m_startingScreenTexture);
        }


        internal void DrawBetweenLevels(int nextLevel)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);
            string text = "";
            Vector2 textPosition = m_camera.ConvertScale(new Vector2(1, 12));
            if(nextLevel == 2)
            {
                text = @"You will slide to either the right or left on the red blocks. 
The blue blocks are indestructable.";
            }
            if (nextLevel == 3)
            {
                text = "Watch out for the bats.";
            }

            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_betweenLevelScreenTexture, tileRectangle, Color.White);
            m_spriteBatch.DrawString(betweenLevelFont, text, textPosition, Color.Black);
            m_spriteBatch.End();
        }

        internal void DrawGameFinished()
        {
            DrawTexture(m_gameFinishedTexture);
        }

        internal void DrawGameOver()
        {
            DrawTexture(m_gameOverTexture);
        }

        private void DrawTexture(Texture2D texture)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);

            m_spriteBatch.Begin();
            m_spriteBatch.Draw(texture, tileRectangle, Color.White);
            m_spriteBatch.End();
        }
    }
}
