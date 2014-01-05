using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Mineclimber.Model
{
    class Character
    {
        /// <summary>
        /// Fields containing character attributes
        /// </summary>
        private Vector2 m_size;
        private int m_health = 3;
        private Vector2 m_position;
        private Vector2 direction;
        private float speed = 4f;
        private float gravityAcceleration = 0.2f;
        private Vector2 defaultPosition = new Vector2(6f, Level.LevelHeight - 3);

        /// <summary>
        /// Constructor
        /// Generates the character size and position
        /// </summary>
        public Character()
        {
            m_size = new Vector2(0.8f, 1.9f);
            m_position = DefaultPosition;
            direction = new Vector2(0, 0);
        }

        internal Vector2 DefaultPosition
        {
            get
            {
                return defaultPosition;
            }
        }

        internal void SetVelocity(Vector2 a_velocity)
        {
            direction = a_velocity;
        }

        internal void SetPosition(Vector2 a_pos)
        {
            //a_pos.Y -= m_size.Y;
            //a_pos.X -= m_size.X;
            PositionTopLeft = a_pos;
        }

        internal Vector2 Direction
        {
            get
            { return direction; }
            set
            { direction = value; }
        }

        /// <summary>
        /// Return character size
        /// </summary>
        internal Vector2 Size
        {
            get
            {
                return m_size;
            }
        }

        /// <summary>
        /// Return the Gravity acceleration
        /// </summary>
        internal float Acceleration
        {
            get
            {
                return gravityAcceleration;
            }
            set
            {
                gravityAcceleration = value;
            }
        }

        /// <summary>
        /// Return the character speed
        /// </summary>
        internal float Speed
        {
            get
            {
                return speed;
            }
        }

        /// <summary>
        /// return & set the characters top left position
        /// </summary>
        internal Vector2 PositionTopLeft
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        /// <summary>
        /// Return & Set the character health
        /// </summary>
        internal int Health
        {
            get
            {
                return m_health;
            }
            set
            {
                m_health = value;
            }
        }
    }

    enum CharacterLookDirection
    {
        Right,
        Left
    }
}
