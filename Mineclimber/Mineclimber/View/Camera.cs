using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Mineclimber.Model;

namespace Mineclimber.View
{
    class Camera
    {
        /// <summary>
        /// Scale of the screen
        /// </summary>
        private Vector2 scale;
        private Vector2 cameraPosition;
        private Vector2 defaultCameraPosition;
        /// <summary>
        /// Constructor, set the scale value to the size of the screen
        /// </summary>
        /// <param name="_screenSize">size of the screen</param>
        public Camera(Vector2 _screenSize)
        {
            scale.X = _screenSize.X / 10;
            scale.Y = _screenSize.Y / 10;
            defaultCameraPosition = CoordinateConverter(new Vector2(0, -Level.LevelHeight + 20));
            cameraPosition = defaultCameraPosition;
        }

        internal Vector2 DefaultCameraPosition
        {
            get
            {
                return defaultCameraPosition;
            }
        }
        
        /// <summary>
        /// Returns cameraposition
        /// Sets cameraposition
        /// </summary>
        internal Vector2 CameraPosition
        {
            get     { return cameraPosition; }
            set { cameraPosition = value; }
        }

        /// <summary>
        /// adds the directionY to the cameraposition 
        /// </summary>
        /// <param name="directionX"></param>
        internal void MoveCameraY(float directionY)
        {
            cameraPosition.Y += directionY;
        }

        internal Vector2 ConvertScale(Vector2 logicalCoordinate)
        {
            Vector2 visualCoordinate = new Vector2((logicalCoordinate.X * scale.X) / 2, (logicalCoordinate.Y * scale.Y) / 2);
            return visualCoordinate;
        }

        /// <summary>
        /// Converts logical coordinate to visual
        /// </summary>
        /// <param name="logicalCoordinate">logical Coordinate</param>
        /// <returns>Visual coordinate</returns>
        internal Vector2 CoordinateConverter(Vector2 logicalCoordinate)
        {
            Vector2 visualCoordinate = new Vector2((logicalCoordinate.X * scale.X) / 2, (logicalCoordinate.Y * scale.Y) / 2);
            return visualCoordinate + cameraPosition;
        }

        /// <summary>
        /// Converts visual coordinate to logical
        /// </summary>
        /// <param name="visualCoordinate">Visual coordinate</param>
        /// <returns>Logical coordinate</returns>
        internal Vector2 ConvertVisualToLogical(Vector2 visualCoordinate)
        {
            Vector2 logicalCoordinate = new Vector2(visualCoordinate.X / scale.X, visualCoordinate.Y / scale.Y);
            return logicalCoordinate;
        }



    }
}
