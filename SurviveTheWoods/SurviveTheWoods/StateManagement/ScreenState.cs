// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using System;
using System.Collections.Generic;
using System.Text;

namespace SurviveTheWoods.StateManagement
{
    /// <summary>
    /// Enumerations of the possible screen states
    /// </summary>
    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden
    }
}
