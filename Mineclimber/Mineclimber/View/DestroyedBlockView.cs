using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class DestroyedBlockView
    {
        private SpriteBatch splitterImage;
        private Texture2D texture;

        public DestroyedBlockView(ContentManager content, GraphicsDevice graphicsDevice)
        {
            splitterImage = new SpriteBatch(graphicsDevice);
            texture = content.Load<Texture2D>("RockParticle");
        }

        public void Draw(SplitterParticle particle, Camera camera)
        {
            
            Vector2 position = camera.CoordinateConverter(particle.Position);
            Vector2 size = camera.ConvertScale(particle.GetParticleSize());
            Rectangle targetCoordinate = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            splitterImage.Begin();
            splitterImage.Draw(texture, targetCoordinate, Color.White);
            splitterImage.End();
        }
    }
}
