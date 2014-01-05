using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineclimber.Model;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Mineclimber.View;

namespace Mineclimber.Controller
{
    class CharacterController
    {
        private CharacterSimulation characterSimulation;

        private KeyboardState keyboardState;
        private CharacterLookDirection lookDirection;
        private Audio audio;

        private Tile CharacterCrackTile;

        private DestroyedBlockView destroyedBlockView;
        private SplitterParticle[] particleList;

        private List<DestroyedBlockSimulation> destroyedBlockSimulation = new List<DestroyedBlockSimulation>();
        private List<SplitterParticle[]> splitterParticleArrayList = new List<SplitterParticle[]>();

        /// <summary>
        /// Constructor
        /// Takes the level tiles as parameter
        /// creates the charactersimulation
        /// </summary>
        /// <param name="tiles"></param>
        public CharacterController(Tile[][] tiles, Audio _audio, DestroyedBlockView DV)
        {
            characterSimulation = new CharacterSimulation(tiles);
            audio = _audio;
            destroyedBlockView = DV;
        }

        /// <summary>
        /// returns the direction the character is looking
        /// </summary>
        internal CharacterLookDirection CharacterLookDirection
        {
            get { return lookDirection; }
        }

        /// <returns>Returns the character</returns>
        internal Character GetCharacter()
        {
            return characterSimulation.GetCharacter();
        }

        /// <summary>
        /// Controller for the character movement
        /// </summary>
        /// <param name="gameTime">Gametime</param>
        internal void MoveCharacter(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if(characterSimulation.GetHasCollidedWithRoof())
                CrackTile();

            if (keyboardState.IsKeyDown(Keys.Space) && characterSimulation.CanJump())
            {
                characterSimulation.CharacterJump((float)gameTime.ElapsedGameTime.TotalSeconds);
                audio.PlayJumpSound();
            }
             
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                MoveRight((float)gameTime.ElapsedGameTime.TotalSeconds);
                lookDirection = CharacterLookDirection.Right;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                MoveLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
                lookDirection = CharacterLookDirection.Left;
            }

            characterSimulation.UpdateCharacter((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        /// Move the character Right
        /// </summary>
        /// <param name="gameTime">gametime in seconds</param>
        private void MoveRight(float gameTime)
        {
            characterSimulation.MoveCharacterRight(gameTime);
        }

        /// <summary>
        /// Moves the character left
        /// </summary>
        /// <param name="gameTime">gametime in seconds</param>
        private void MoveLeft(float gameTime)
        {
            characterSimulation.MoveCharacterLeft(gameTime);
        }

        private void CrackTile()
        {
            audio.PlayHitSound();
            CharacterCrackTile = characterSimulation.getTileCollidedRoof();
            CharacterCrackTile.CrackTile();
            Tile[][] NewTiles = characterSimulation.GetLevel;
            NewTiles[(int)CharacterCrackTile.Position.Y][(int)CharacterCrackTile.Position.X] = CharacterCrackTile;

            if (NewTiles[(int)CharacterCrackTile.Position.Y][(int)CharacterCrackTile.Position.X].HasCracked)
            {
                destroyedBlockSimulation.Add(new DestroyedBlockSimulation());
                particleList = destroyedBlockSimulation[destroyedBlockSimulation.Count - 1].GetSplitterParticleList();

                foreach (SplitterParticle particle in particleList)
                {
                    particle.ParticleStartingPosition(NewTiles[(int)CharacterCrackTile.Position.Y][(int)CharacterCrackTile.Position.X].Position +
                        NewTiles[(int)CharacterCrackTile.Position.Y][(int)CharacterCrackTile.Position.X].Size / 2);
                }

                splitterParticleArrayList.Add(particleList);
                particleList = null;
            }
        }

        internal bool LevelCompleted()
        {
            return characterSimulation.IsCharacterOnTopOfMap();
        }

        internal void ResetCharacterLocation()
        {
            characterSimulation.ResetCharacterLocation();
        }

        internal void UpdateSplitterParticles(float elapsedTime)
        {
            for (int i = 0; i < destroyedBlockSimulation.Count; i++)
            {
                destroyedBlockSimulation[i].newParticlePosition(elapsedTime);
                if (destroyedBlockSimulation[i].LifeTime > 4)
                {
                    destroyedBlockSimulation.RemoveAt(i);
                    splitterParticleArrayList.RemoveAt(i);
                }
            }
        }

        internal void DrawParticles(Camera camera)
        {
            for (int i = 0; i < splitterParticleArrayList.Count; i++)
            {
                foreach(SplitterParticle particle in splitterParticleArrayList[i])
                {
                    destroyedBlockView.Draw(particle, camera);
                }
            }
        }

        internal void DeleteParticles()
        {
            for (int i = 0; i < destroyedBlockSimulation.Count; i++)
            {
                destroyedBlockSimulation.RemoveAt(i);
                splitterParticleArrayList.RemoveAt(i);
            }
        }
    }
}
