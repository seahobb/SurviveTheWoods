using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class FrontWall
    {
        public Texture2D Texture { get; set; }

        private Vector2 position;

        /// <summary>
        /// Sets front wall position
        /// </summary>
        /// <param name="position">the position of the wall</param>
        public FrontWall(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Position of the wall
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Draws the wall
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //top, left, width(right-left) height(top-low)
            spriteBatch.Draw(Texture, position, new Rectangle(0, 858, 47, 35), Color.White);
            //(32, 16, 32, 32)
        }
    }
}
