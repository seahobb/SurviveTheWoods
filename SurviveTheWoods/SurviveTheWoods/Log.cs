using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class Log
    {
        private Texture2D texture;

        private Vector2 position;

        /// <summary>
        /// Sets log position
        /// </summary>
        /// <param name="position">position of the log</param>
        public Log(Vector2 position)
        {
            this.position = position;
            //this.bounds = new BoundingRectangle()
        }

        /// <summary>
        /// Loads the log sprite
        /// </summary>
        /// <param name="content">the log sprite</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }

        /// <summary>
        /// Draws the log sprite
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(96, 80, 32, 16), Color.White);
        }
    }
}
