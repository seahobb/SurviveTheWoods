using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class WoodFloor
    {
        public Texture2D Texture { get; set; }

        private Vector2 position;

        /// <summary>
        /// Sets floor position
        /// </summary>
        /// <param name="position">the position of the floor</param>
        public WoodFloor()//Vector2 position)
        {
            //this.position = position;
        }

        /// <summary>
        /// Position of the floor
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Draws the floor
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //top, left, width(right-left) height(top-low)
           // spriteBatch.Draw(Texture, position, new Rectangle(576, 0, 15, 15), Color.White);

            //double for loop draw here
            for (int i = 0; i < 22; i++) 
           {
               for (int j = 0; j < 20; j++) 
               {
                   spriteBatch.Draw(Texture, new Vector2((i*16)+500, (j*16)-320), new Rectangle(0, 576, 16, 16), Color.White);
               }
           }
        }
    }
}
