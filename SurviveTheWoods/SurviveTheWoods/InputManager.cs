using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SurviveTheWoods.Collisions;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace SurviveTheWoods
{
    public class InputManager
    {
        MouseState mouseState;

        private BoundingRectangle exitBounds = new BoundingRectangle(new Vector2(730, 430), 48, 48); //x y width height, 48 is wrong, but not used

        /// <summary>
        /// Bool for if the exit button has been pressed
        /// </summary>
        public bool ExitButtonPressed { get; private set; }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            float x = Cursor.Position.X;
            float y = Cursor.Position.Y;
            Vector2 vector = new Vector2(x, y);
            if (mouseState.LeftButton == ButtonState.Pressed && exitBounds.CollidesWith(vector))
            {
                ExitButtonPressed = true;
            }
        }
    }
}
