using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public class Key
    {
        public Texture2D Texture { get; set; }

        public bool StopDraw { get; set; } = false;

        private Vector2 position;

        private BoundingCircle bounds; 

        /// <summary>
        /// key position
        /// </summary>
        /// <param name="position">the position of the door</param>
        public Key(Vector2 position)
        {
            this.position = position;
            bounds = new BoundingCircle(new Vector2(position.X, position.Y), 8);
            //bounds = new BoundingCircle(position - new Vector2(8, 8), 8);
            // bounds = new BoundingRectangle(new Vector2(position.X, position.Y), 8, 8);
            bounds.Center = new Vector2(position.X + 48, position.Y + 48);
        }

        /// <summary>
        /// Position of the key
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }


        /* public BoundingRectangle Bounds
         {
             get => bounds;
             set => bounds = value;
         }*/
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Draws the key
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //, width(right-left) height(top-low)
            if (StopDraw) return;

            spriteBatch.Draw(Texture, position, new Rectangle(115, 2096, 16, 16), Color.White, 0, new Vector2(0, 0), 2.0f,
                SpriteEffects.None, 0);
        }
    }
}
