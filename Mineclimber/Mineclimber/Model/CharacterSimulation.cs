using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mineclimber.Model
{
    class CharacterSimulation
    {
        class CollisionDetails
        {
            public Vector2 m_speedAfterCollision;
            public Vector2 m_positionAfterCollision;
            public bool m_hasCollidedWithGround = false;
            public Tile m_theCollidedTile = null;
            private Vector2 a_oldPos;
            private Vector2 a_velocity;

            public CollisionDetails(Vector2 a_oldPos, Vector2 a_velocity)
            {
                m_positionAfterCollision = a_oldPos;
                m_speedAfterCollision = a_velocity;
            }

        }

        private Character character;
        private Tile[][] levelTiles;
        private Vector2 tileSize;

        private FloatRectangle characterRectangle;

        private bool hasCollidedWithGround = false;
        private bool hasCollidedWithRoof = false;

        private Tile tileCollidedRoof = null;
        private Tile collidedTile = null;

        /// <summary>
        /// Constructor
        /// creates character and set the tiles of the level
        /// </summary>
        /// <param name="tiles">The tiles of the map</param>
        public CharacterSimulation(Tile[][] tiles)
        {
            character = new Character();
            levelTiles = tiles;
            tileSize = levelTiles[30][0].Size;

            characterRectangle = new FloatRectangle(character.PositionTopLeft, new Vector2(character.PositionTopLeft.X + character.Size.X, character.PositionTopLeft.Y + character.Size.Y));
        }

        private float timeSinceJumped = 0;
        /// <returns>If character is in air</returns>
        internal bool CanJump()
        {
            if (hasCollidedWithGround && timeSinceJumped > 0.25)
            {
                timeSinceJumped = 0;
                return true;
            }
            return false;
        }

        /// <returns>Character</returns>
        internal Character GetCharacter()
        {
            return character;
        }

        internal bool GetHasCollidedWithRoof()
        {
            return hasCollidedWithRoof;
        }

        internal Tile getTileCollidedRoof()
        {
            return tileCollidedRoof;
        }

        internal Tile[][] GetLevel
        {
            get { return levelTiles; }
        }

        internal void UpdateTiles(Tile[][] NewTiles)
        {
            levelTiles = NewTiles;
        }

        /// <summary>
        /// The code that makes the character Jump
        /// Changes the character direction to up wich will later go back down by gravity
        /// </summary>
        /// <param name="elapsedTime">The elapsed time in seconds</param>
        internal void CharacterJump(float elapsedTime)
        {
            Vector2 direction = character.Direction;
            direction.Y -= (character.Speed * 2f) * elapsedTime;

            character.Direction = direction;
        }

        /// <summary>
        /// Updates the character position to the direction that is set.
        /// only updates the position if the character didn't collide with a tile.
        /// If character collided the position will be set to the previous position
        /// Checks collition
        /// </summary>
        /// <param name="elapsedTime"></param>
        internal void UpdateCharacter(float elapsedTime)
        {
            timeSinceJumped += elapsedTime;
            Vector2 oldPos = character.PositionTopLeft;
            UpdateOldPos(elapsedTime);
            Vector2 newPos = character.PositionTopLeft;

            oldPos.Y += character.Size.Y;
            oldPos.X += character.Size.X / 2;

            //Makes newPos to bottomcenter
            newPos.Y += character.Size.Y;
            newPos.X += character.Size.X / 2;        

            hasCollidedWithGround = false;
            hasCollidedWithRoof = false;
            Vector2 speed = character.Direction;
            //Collision
            if (didCollide(newPos, character.Size))
            {
                CollisionDetails details = getCollisionDetails(oldPos, newPos, character.Size, speed);
                hasCollidedWithGround = details.m_hasCollidedWithGround;

                //set the new speed and position after collision
                details.m_positionAfterCollision.Y -= character.Size.Y;
                details.m_positionAfterCollision.X -= character.Size.X / 2;

                character.SetPosition(details.m_positionAfterCollision);
                character.SetVelocity(details.m_speedAfterCollision);
                if (hasCollidedWithGround)
                {
                    if (collidedTile.tiletype == TileType.SlideBlockRight)
                    {
                        Vector2 direction = character.Direction;
                        direction.X = direction.X + elapsedTime * (character.Speed * 2);
                        character.Direction = direction;
                    }
                    if (collidedTile.tiletype == TileType.SlideBlockLeft)
                    {
                        Vector2 direction = character.Direction;
                        direction.X = direction.X - elapsedTime * (character.Speed * 2);
                        character.Direction = direction;
                    }
                }
            }
            
        }

        /// <summary>
        /// Sets the new value for the position
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void UpdateOldPos(float elapsedTime)
        {
            Vector2 direction = character.Direction;
            direction = MoveCharacterDown(elapsedTime, direction);

            character.PositionTopLeft += character.Direction;

            direction.X = 0;
            character.Direction = direction;
        }


        /// <summary>
        /// Skriven av Daniel Toll
        /// </summary>
        /// <param name="a_oldPos"></param>
        /// <param name="a_newPosition"></param>
        /// <param name="a_size"></param>
        /// <param name="a_velocity"></param>
        /// <returns></returns>
        private CollisionDetails getCollisionDetails(Vector2 a_oldPos, Vector2 a_newPosition, Vector2 a_size, Vector2 a_velocity)
        {
            CollisionDetails ret = new CollisionDetails(a_oldPos, a_velocity);

            Vector2 slidingXPosition = new Vector2(a_newPosition.X, a_oldPos.Y); //Y movement ignored
            Vector2 slidingYPosition = new Vector2(a_oldPos.X, a_newPosition.Y); //X movement ignored

            if (didCollide(slidingXPosition, a_size) == false)
            {
                return doOnlyXMovement(ref a_velocity, ret, ref slidingXPosition);
            }
            else if (didCollide(slidingYPosition, a_size) == false)
            {
                return doOnlyYMovement(ref a_velocity, ret, ref slidingYPosition);
            }
            else
            {
                return doStandStill(ret, a_velocity);
            }
        }


        private bool didCollide(Vector2 a_centerBottom, Vector2 a_size)
        {
            FloatRectangle occupiedArea = FloatRectangle.createFromCenterBottom(a_centerBottom, a_size);
            if (TilesCollition(occupiedArea))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Moves the character down with gravity
        /// Checks if character collided with floor, if did it doesn't move down
        /// </summary>
        /// <param name="elapsedTime">elapsed time in seconds</param>
        /// <param name="direction">The current direction</param>
        /// <returns>the new direction</returns>
        private Vector2 MoveCharacterDown(float elapsedTime, Vector2 direction)
        {
            if (hasCollidedWithGround == false)
            {
                direction.Y += character.Acceleration * elapsedTime;
            }
            return direction;
        }

        /// <summary>
        /// Moves the character left
        /// </summary>
        /// <param name="elapsedTime">elapsed time in seconds</param>
        internal void MoveCharacterLeft(float elapsedTime)
        {
            Vector2 direction = character.Direction;
            direction.X -= character.Speed * elapsedTime;

            character.Direction = direction;
        }

        /// <summary>
        /// Moves the character right
        /// </summary>
        /// <param name="elapsedTime">elapsed time in seconds</param>
        internal void MoveCharacterRight(float elapsedTime)
        {
            Vector2 direction = character.Direction;
            direction.X += character.Speed * elapsedTime;

            character.Direction = direction;
        }

        /// <summary>
        /// Returns true if character collided on the sides
        /// </summary>
        /// <returns>Returns boolean</returns>
        private bool TilesCollition(FloatRectangle occupiedArea)
        {
            Vector2 tileSize = new Vector2(1, 1);
            for (int y = 0; y < levelTiles.Length; y++)
            {
                for (int x = 0; x < levelTiles[y].Length; x++)
                {
                    FloatRectangle rect = FloatRectangle.createFromTopLeft(new Vector2(x, y), tileSize);
                    if (occupiedArea.isIntersecting(rect))
                    {
                        if (levelTiles[y][x].tiletype == TileType.Rockblock || levelTiles[y][x].tiletype == TileType.BedRock || 
                            levelTiles[y][x].tiletype == TileType.SlideBlockLeft || levelTiles[y][x].tiletype == TileType.SlideBlockRight)
                        {
                            collidedTile = levelTiles[y][x];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private CollisionDetails doStandStill(CollisionDetails ret, Vector2 a_velocity)
        {
            if (a_velocity.Y > 0)
            {
                ret.m_hasCollidedWithGround = true;
            }
            ret.m_speedAfterCollision = new Vector2(0, 0);
            return ret;
        }

        private CollisionDetails doOnlyYMovement(ref Vector2 a_velocity, CollisionDetails ret, ref Vector2 slidingYPosition)
        {
            a_velocity.X *= -0.5f; //bounce from wall
            ret.m_speedAfterCollision = a_velocity;
            ret.m_positionAfterCollision = slidingYPosition;
            return ret;
        }

        private CollisionDetails doOnlyXMovement(ref Vector2 a_velocity, CollisionDetails ret, ref Vector2 slidingXPosition)
        {
            ret.m_positionAfterCollision = slidingXPosition;
            //did we slide on ground?
            if (a_velocity.Y > 0)
            {
                ret.m_hasCollidedWithGround = true;
            }

            ret.m_speedAfterCollision = doSetSpeedOnVerticalCollision(a_velocity);

            return ret;
        }

        private Vector2 doSetSpeedOnVerticalCollision(Vector2 a_velocity)
        {
            //did we collide with ground?
            if (a_velocity.Y > 0)
            {
                a_velocity.Y = 0; //no bounce
            }
            else
            {
                //collide with roof
                hasCollidedWithRoof = true;
                tileCollidedRoof = collidedTile;
                a_velocity.Y *= -1.0f;
            }

            a_velocity.X *= 0.10f;

            return a_velocity;
        }


        internal bool IsCharacterOnTopOfMap()
        {
            if(hasCollidedWithGround && character.PositionTopLeft.Y - character.Size.Y <= 0)
            {
                return true;
            }
            return false;
        }

        internal void ResetCharacterLocation()
        {
            character.PositionTopLeft = character.DefaultPosition;
        }
    }
}
