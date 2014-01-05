using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class DestroyedBlockSimulation
    {
        private SplitterParticle[] splitterParticleList;
        private float lifeTime;

        public DestroyedBlockSimulation()
        {
            GenerateSplitterParticleList();
        }

        internal float LifeTime
        {
            get { return lifeTime; }
        }

        public void newParticlePosition(float elapsedTime)
        {
            lifeTime += elapsedTime;
            foreach (var splitterParticle in splitterParticleList)
            {
                Vector2 position = splitterParticle.Position;
                Vector2 newVelocity = new Vector2();
                Vector2 direction = splitterParticle.Direction;
                Vector2 acceleration = splitterParticle.GetAcceleration();

                newVelocity.X = elapsedTime * acceleration.X + direction.X;
                newVelocity.Y = elapsedTime * acceleration.Y + direction.Y;

                position.X += elapsedTime * newVelocity.X;
                position.Y += elapsedTime * newVelocity.Y;

                splitterParticle.Position = position;
                splitterParticle.Direction = newVelocity;
            }
        }

        public SplitterParticle[] GetSplitterParticleList()
        {
            return splitterParticleList;
        }

        public void GenerateSplitterParticleList()
        {
            Random rand = new Random();
            splitterParticleList = new SplitterParticle[49];
            for(int i = 0; i <= splitterParticleList.Length - 1; i++)
            {
                splitterParticleList[i] = new SplitterParticle();
                splitterParticleList[i].GenerateDirection(rand);
            }
        }
    }
}
