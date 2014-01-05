using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineclimber.View;
using Microsoft.Xna.Framework.Input;

namespace Mineclimber.Controller
{
    class BetweenLevelController
    {
        private BetweenLevels view;
        private KeyboardState oldKeyboardState;
        private GameState gameState = GameState.StartingScreen;

        public BetweenLevelController(BetweenLevels startingScreenView)
        {
             view = startingScreenView;
             oldKeyboardState = new KeyboardState();
        }

        internal GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        internal void UpdateStartingScreen(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;
            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.InGame;
            }
        }

        internal void UpdateBetweenLevels(KeyboardState keyboard)
        {

            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;
            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.InGame;
            }
        }

        internal void DrawStartingScreen()
        {
            view.DrawStartingScreen();
        }

        internal void DrawBetweenLevels(int nextLevel)
        {
            view.DrawBetweenLevels(nextLevel);
        }

        internal void DrawGameOver()
        {
            view.DrawGameOver();
        }
    }
}
