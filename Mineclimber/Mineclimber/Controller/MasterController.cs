using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mineclimber.Model;
using Mineclimber.View;

namespace Mineclimber.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MasterController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        LevelController levelController;
        CharacterController characterController;
        CharacterView characterView;
        Character character;
        BatView batView;

        KeyboardState keyboardState;

        GameState gamestate = GameState.StartingScreen;

        BetweenLevelController betweenLevelController;

        DestroyedBlockSimulation destroyedBlockSimulation;
        DestroyedBlockView destroyedBlockView;

        PauseScreen pauseScreenView;
        PauseScreenController pauseScreenController;

        Camera camera;

        Audio audio;
        private float TimeSinceMusicStarted;

        public static int Currentlevel = 0;
        float timeSinceLevelStarted = 0;
        float lastLevelTime;
        private Vector2 TimePosition;

        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TimePosition.X = GraphicsDevice.Viewport.Width - 40;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            betweenLevelController = new BetweenLevelController(new BetweenLevels(GraphicsDevice, Content));
            levelController = new LevelController(GraphicsDevice, Content);
            
            pauseScreenView = new PauseScreen(GraphicsDevice, Content);
            pauseScreenController = new PauseScreenController(pauseScreenView);

            audio = new Audio(Content);
            
            destroyedBlockSimulation = new DestroyedBlockSimulation();
            destroyedBlockView = new DestroyedBlockView(Content, GraphicsDevice);

            batView = new BatView(GraphicsDevice, Content);

            characterController = new CharacterController(levelController.GetTiles(), audio, destroyedBlockView, batView);
            characterView = new CharacterView(GraphicsDevice, Content);
            camera = new Camera(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            keyboardState =  Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            //Startar om nivån du är på
            if (keyboardState.IsKeyDown(Keys.Back))
            {
                levelController.GenerateNextLevel(Currentlevel);
                characterController.ResetCharacterLocation();
                characterController.DeleteParticles();
                camera.CameraPosition = camera.DefaultCameraPosition;
                timeSinceLevelStarted = 0;
            }
            //Pause screen
            if (keyboardState.IsKeyDown(Keys.P))
            {
                gamestate = GameState.GamePaused;
                audio.StopGameMusic();
            }

            if (gamestate == GameState.GamePaused)
            {
                pauseScreenController.UpdatePauseScreen(keyboardState);
                gamestate = pauseScreenController.GameState;
                if (gamestate == GameState.InGame)
                {
                    pauseScreenController.GameState = GameState.GamePaused;
                    audio.PlayGameMusic();
                }
            }
            if (gamestate == GameState.GameOver)
            {
                betweenLevelController.GameState = GameState.GameOver;
                betweenLevelController.UpdateStartingScreen(keyboardState);
                gamestate = betweenLevelController.GameState;
                if (gamestate == GameState.InGame)
                {
                    audio.PlayGameMusic();
                    characterController.ResetHealth();
                    characterController.ResetCharacterLocation();
                    levelController.GenerateNextLevel(Currentlevel);
                    camera.CameraPosition = camera.DefaultCameraPosition;
                    timeSinceLevelStarted = 0;
                    characterController.DeleteParticles();
                    betweenLevelController.GameState = GameState.BetweenLevels;
                }
            }
            if (gamestate == GameState.StartingScreen)
            {
                betweenLevelController.UpdateStartingScreen(keyboardState);
                gamestate = betweenLevelController.GameState;
                if (gamestate == GameState.InGame)
                {
                    Currentlevel += 1;
                    audio.PlayGameMusic();
                    betweenLevelController.GameState = GameState.BetweenLevels;
                }
            }
            else if (gamestate == GameState.BetweenLevels)
            {
                betweenLevelController.UpdateBetweenLevels(keyboardState);
                gamestate = betweenLevelController.GameState;
                if (gamestate == GameState.InGame)
                {
                    Currentlevel += 1;
                    audio.PlayGameMusic();
                    levelController.GenerateNextLevel(Currentlevel);
                    characterController.ResetCharacterLocation();
                    camera.CameraPosition = camera.DefaultCameraPosition;
                    betweenLevelController.GameState = GameState.BetweenLevels;
                }
            }

            if(gamestate == GameState.InGame)
            {
                timeSinceLevelStarted += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //character movement
                characterController.MoveCharacter(gameTime);
                characterController.UpdateBats((float)gameTime.ElapsedGameTime.TotalSeconds);
                character = characterController.GetCharacter();

                if (character.Health <= 0)
                {
                    gamestate = GameState.GameOver;
                    audio.StopGameMusic();
                }

                //Background music
                if (TimeSinceMusicStarted >= 195)
                {
                    TimeSinceMusicStarted = 0;
                    audio.PlayGameMusic();
                }
                TimeSinceMusicStarted += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Camera position
                if (character.PositionTopLeft.Y < Level.LevelHeight - 10)
                {
                    Vector2 cameraPosition = camera.CameraPosition;
                    cameraPosition.Y = camera.ConvertScale(-character.PositionTopLeft).Y + (GraphicsDevice.Viewport.Height / 2);
                    camera.CameraPosition = cameraPosition;
                }
                if(characterController.LevelCompleted())
                {
                    lastLevelTime = timeSinceLevelStarted;
                    timeSinceLevelStarted = 0;
                    characterController.DeleteParticles();

                    if (Currentlevel == 3)
                    {
                        gamestate = GameState.GameFinished;
                    }
                    else
                    {
                        gamestate = GameState.BetweenLevels;
                        betweenLevelController.GameState = GameState.BetweenLevels;
                    }
                }

                characterController.UpdateSplitterParticles((float)gameTime.ElapsedGameTime.TotalSeconds);

                //TimerPosition
                if (character.PositionTopLeft.Y < Level.LevelHeight - 10)
                {
                    TimePosition.Y = camera.CoordinateConverter(character.PositionTopLeft).Y - GraphicsDevice.Viewport.Height / 2;
                }
                else
                {
                    TimePosition.Y = camera.CoordinateConverter(new Vector2(0, Level.LevelHeight - 20)).Y;
                }  
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (gamestate == GameState.GamePaused)
            {
                pauseScreenController.DrawPauseScreen();
            }
            if (gamestate == GameState.StartingScreen)
            {
                betweenLevelController.DrawStartingScreen();
            }
            if (gamestate == GameState.BetweenLevels)
            {
                betweenLevelController.DrawBetweenLevels(Currentlevel + 1);
            }
            if (gamestate == GameState.GameFinished)
            {
                betweenLevelController.DrawGameFinished();
            }
            if (gamestate == GameState.GameOver)
            {
                betweenLevelController.DrawGameOver();
            }
            if (gamestate == GameState.InGame)
            {
                levelController.DrawLevel(camera);
                levelController.DrawTime(timeSinceLevelStarted, TimePosition);
                characterController.DrawParticles(camera);
                characterView.DrawCharacter(character, characterController.CharacterLookDirection, camera);
                if (Currentlevel == 3)
                {
                    characterController.DrawBats(camera);
                }
            }
            base.Draw(gameTime);
        }
    }
}
