using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mineclimber.Controller
{
    /// <summary>
    /// The different game states
    /// </summary>
    enum GameState
    {
        StartingScreen,
        InGame,
        BetweenLevels,
        GamePaused,
        GameFinished,
        GameOver
    }
}
