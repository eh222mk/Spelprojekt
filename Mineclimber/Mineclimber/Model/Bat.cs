using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.Model
{
    class Bat
    {
        private Vector2 velocity;
        private Vector2 position;
        private Vector2 size;

        public Bat(Vector2 _position, Vector2 _velocity)
        {
            velocity = _velocity;
            position = _position;
            size = new Vector2(0.75f, 0.5f);
        }

        internal Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        internal Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        internal Vector2 Size
        {
            get { return size; }
        }
    }
}
