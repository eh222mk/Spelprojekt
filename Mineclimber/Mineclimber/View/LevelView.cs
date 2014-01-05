using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Mineclimber.Model;

namespace Mineclimber.View
{
    class LevelView
    {
        /// <summary>
        /// Fields
        /// </summary>
        private SpriteBatch m_spriteBatch;
        private Texture2D m_rockTexture;
        private SpriteFont timerSprite;

        /// <summary>
        /// Constructor, set value of the spritepatch and loads the texture
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="content"></param>
        public LevelView(GraphicsDevice graphicsDevice, ContentManager content)
        {
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_rockTexture = content.Load<Texture2D>("rocktile");
            timerSprite = content.Load<SpriteFont>("levelTime");
        }

        /// <summary>
        /// Draws the tiles on the level
        /// </summary>
        /// <param name="tile">The tile that will be drawn</param>
        internal void DrawLevel(Tile tile, Camera m_camera)
        {
            if (tile.tiletype == TileType.Rockblock)
            {
                DrawTile(tile, m_camera, Color.White);
            }
            else if (tile.tiletype == TileType.SlideBlockLeft ||tile.tiletype == TileType.SlideBlockRight)
            {
                DrawTile(tile, m_camera, Color.Red);
            }
            else if (tile.tiletype == TileType.BedRock)
            {
                DrawTile(tile, m_camera, Color.Blue);
            }
            else if (tile.tiletype == TileType.Empty)
            {
                DrawTile(tile, m_camera, Color.Gray);
            }
        }

        private void DrawTile(Tile tile, Camera m_camera, Color color)
        {
            Vector2 tileSize = m_camera.ConvertScale(tile.Size);
            Vector2 position = m_camera.CoordinateConverter(tile.Position);
            Rectangle tileRectangle = new Rectangle((int)position.X, (int)position.Y, (int)tileSize.X, (int)tileSize.Y);

            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_rockTexture, tileRectangle, color);
            m_spriteBatch.End();
        }

        internal void DrawTimer(float time, Vector2 position)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.DrawString(timerSprite, time.ToString(), position, Color.White);
            m_spriteBatch.End();
        }
    }
}
