﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Mineclimber.Model;

namespace Mineclimber.View
{
    class CharacterView
    {
        /// <summary>
        /// fields
        /// </summary>
        private SpriteBatch m_spriteBatch;
        private Texture2D m_characterTexture;

        /// <summary>
        /// Constructor set value of the spritepatch and loads the texture
        /// </summary>
        /// <param name="graphicsDevice">graphicsDevice</param>
        /// <param name="content">ContentManager</param>
        public CharacterView(GraphicsDevice graphicsDevice, ContentManager content)
        {
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_characterTexture = content.Load<Texture2D>("temporateminer");
            //m_camera = new Camera(new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height));
        }


        /// <summary>
        /// Draws the players character
        /// </summary>
        /// <param name="character">The character</param>
        internal void DrawCharacter(Character character, CharacterLookDirection lookDirection, Camera m_camera)
        {
            if (lookDirection == CharacterLookDirection.Right)
            {
                Vector2 tileSize = m_camera.ConvertScale(character.Size);
                Vector2 position = character.PositionTopLeft;
                position = m_camera.CoordinateConverter(position);
                Rectangle tileRectangle = new Rectangle((int)position.X, (int)position.Y, (int)tileSize.X, (int)tileSize.Y);

                m_spriteBatch.Begin();
                m_spriteBatch.Draw(m_characterTexture, tileRectangle, Color.White);
                m_spriteBatch.End();
            }
            else
            {
                Vector2 tileSize = m_camera.ConvertScale(character.Size);
                Vector2 position = character.PositionTopLeft;
                position = m_camera.CoordinateConverter(position);
                Rectangle tileRectangle = new Rectangle((int)position.X, (int)position.Y, (int)tileSize.X, (int)tileSize.Y);

                m_spriteBatch.Begin();
                m_spriteBatch.Draw(m_characterTexture, tileRectangle, null, Color.White, 0, new Vector2(), SpriteEffects.FlipHorizontally, 0);
                m_spriteBatch.End();
            }
        }
    }
}
