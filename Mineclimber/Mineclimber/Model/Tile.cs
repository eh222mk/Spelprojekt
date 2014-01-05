using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.Model
{
    class Tile
    {
        /// <summary>
        /// Fields containing tile attributes
        /// </summary>
        private Vector2 size;
        private Vector2 position;
        private TileType tileType;
        private int health;
        private bool hasCracked = false;
        
        /// <summary>
        /// Constructor, set the position and type of the tile
        /// Set the size of the tile.
        /// </summary>
        /// <param name="tiletype">The tile type</param>
        /// <param name="Position">The tile Position</param>
        public Tile(TileType tiletype, Vector2 Position, int _health = 1)
        {
            health = _health;
            this.position = Position;
            size = new Vector2(1f, 1f);
            tileType = tiletype;
        }

        internal bool HasCracked
        {
            get{ return hasCracked; }
        }

        /// <summary>
        /// Return the tile size
        /// </summary>
        internal Vector2 Size
        {
            get
            {
                return size;
            }
        }

        internal int Health
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Returns & sets the position of the tile
        /// </summary>
        internal Vector2 Position 
        {
            get 
            {
                return position;
            }
            set
            {
                position = value;
            }
        }


        /// <summary>
        /// Returns the type of the tile
        /// </summary>
        internal TileType tiletype
        {
            get
            {
                return tileType;
            }
            set
            {
                tileType = value;
            }
        }

        internal void CrackTile()
        {
            Health -= 1;
            if (Health <= 0)
            {
                tiletype = TileType.Empty;
                hasCracked = true;
            }
        }
    }

    /// <summary>
    /// Enumerable containing the different types of tiles
    /// </summary>
    enum TileType
    {
        Empty,
        Rockblock,
        SlideBlockRight,
        SlideBlockLeft,
        BedRock
    }
}
