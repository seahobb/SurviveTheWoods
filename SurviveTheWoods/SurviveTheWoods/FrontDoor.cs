using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class FrontDoor
    {
        public Texture2D Texture { get; set; }

        private Vector2 position;

        private bool invert;

       // public bool GameOver { get; set; } = false;

        /// <summary>
        /// Front door position
        /// </summary>
        /// <param name="position">the position of the door</param>
        public FrontDoor(Vector2 position, bool invert)
        {
            this.position = position;
            this.invert = invert;
        }

        /// <summary>
        /// Position of the door
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Draws the door
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int keysLeft)
        {

            if (keysLeft == 0)
            {
                if (invert)
                    spriteBatch.Draw(Texture, position, new Rectangle(113, 867, 12, 24), Color.White, 0, new Vector2(0, 0), 1.4f, SpriteEffects.FlipHorizontally, 0);
                else
                    spriteBatch.Draw(Texture, position, new Rectangle(113, 867, 12, 24), Color.White, 0, new Vector2(0, 0), 1.4f, SpriteEffects.None, 0);
            }
            else
            {
                if (invert)
                    spriteBatch.Draw(Texture, position, new Rectangle(113, 867, 12, 24), Color.White, 0, new Vector2(0, 0), 1.4f, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(Texture, position, new Rectangle(113, 867, 12, 24), Color.White, 0, new Vector2(0, 0), 1.4f, SpriteEffects.FlipHorizontally, 0);
            }
            
        }
    }
}
