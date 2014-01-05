using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.View
{
    class SplitterParticle
    {
        private float maxSpeed = 3f;
        private float minSpeed = 2f;
        private Vector2 particleSize; //size diameter
        private Vector2 position;
        private Vector2 direction;
        private Vector2 acceleration;
        private float lifeTime = 0;

        public SplitterParticle()
        {
            particleSize.X = 0.4f;
            particleSize.Y = 0.4f;

            acceleration.X = 0f;
            acceleration.Y = 8f;
        }

        public float LifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                lifeTime = value;
            }
        }

        public void ParticleStartingPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetAcceleration()
        {
            return acceleration;
        }

        public Vector2 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position.X = value.X;
                position.Y = value.Y;
            }
        }

        public Vector2 GetParticleSize()
        {
            return particleSize;
        }

        public void GenerateDirection(Random rand)
        {
            Vector2 randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between minSpeed to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * (maxSpeed - minSpeed));
            direction = randomDirection;
        }
    }
}
