using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class Bed
    {
        public Texture2D Texture { get; set; }

        private Vector2 position;

        /// <summary>
        /// Sets bed position
        /// </summary>
        /// <param name="position">the position of the bed</param>
        public Bed()//Vector2 position)
        {
            //this.position = position;
        }

        /// <summary>
        /// Position of the bed
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Draws the bed
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //top, left, width(right-left) height(top-low)
             spriteBatch.Draw(Texture, new Vector2(700, -200), new Rectangle(49, 1553, 30, 30), Color.White);
        }
    }
}
