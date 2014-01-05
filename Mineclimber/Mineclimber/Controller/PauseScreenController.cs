using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineclimber.View;
using Microsoft.Xna.Framework.Input;

namespace Mineclimber.Controller
{
    class PauseScreenController
    {
        private PauseScreen view;
        private KeyboardState oldKeyboardState;
        private GameState gameState = GameState.GamePaused;

        public PauseScreenController(PauseScreen pauseScreenView)
        {
            this.view = pauseScreenView;
            oldKeyboardState = new KeyboardState();
        }

        internal GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        internal void UpdatePauseScreen(KeyboardState keyboard)
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

        internal void DrawPauseScreen()
        {
            view.DrawStartingScreen();
        }
    }
}
