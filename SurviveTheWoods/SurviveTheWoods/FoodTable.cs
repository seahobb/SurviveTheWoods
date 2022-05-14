using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class FoodTable
    {
        public Texture2D Texture { get; set; }

        private Vector2 position;

        /// <summary>
        /// Sets table position
        /// </summary>
        /// <param name="position">the position of the table</param>
        public FoodTable()//Vector2 position)
        {
            //this.position = position;
        }

        /// <summary>
        /// Position of the table
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Draws the table
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //top, left, width(right-left) height(top-low)
            spriteBatch.Draw(Texture, new Vector2(590, -120), new Rectangle(0, 1917, 126, 30), Color.White, 0, new Vector2(0, 0), 1.5f,
                SpriteEffects.None, 0);
        }
    }
}
