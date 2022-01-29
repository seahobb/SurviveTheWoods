using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class BaseChip
    {
        private Texture2D texture;

        /// <summary>
        /// Loads the atlas texture
        /// </summary>
        /// <param name="content">the atlas texture</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }

        /// <summary>
        /// Draws certain parts of the atlas texture 
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    spriteBatch.Draw(texture, new Vector2(i*16, j*16), new Rectangle(0, 0, 16, 16), Color.White);
                }
            }
        }
    }
}
