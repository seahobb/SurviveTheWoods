using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SurviveTheWoods.Collisions
{
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects a click within the hard-coded axis points
        /// </summary>
        /// <param name="exitButton">Button points</param>
        /// <param name="mousePosition">Mouse cursor points</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle exitButton, Vector2 mousePosition)
        {
            if (mousePosition.X >= 1292 && mousePosition.X <= 1336 &&
                mousePosition.Y >= 719 && mousePosition.Y <= 764) return true;
            else return false;
        }
    }
}
