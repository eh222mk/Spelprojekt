using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Mineclimber.Model;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class BatView
    {
        private SpriteBatch spriteBatch;
        private Texture2D batTexture;

        public BatView(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            batTexture = content.Load<Texture2D>("newBat");
        }

        internal void DrawBat(Bat bat, Camera camera)
        {
            Vector2 size = camera.ConvertScale(bat.Size);
            Vector2 position = camera.CoordinateConverter(bat.Position);
            Rectangle tileRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            spriteBatch.Begin();
            spriteBatch.Draw(batTexture, tileRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
