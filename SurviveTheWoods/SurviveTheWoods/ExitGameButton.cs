using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class ExitGameButton
    {
        private Texture2D texture;

        /// <summary>
        /// Loads the exit button texture
        /// </summary>
        /// <param name="content">button texture</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }

        /// <summary>
        /// Draws the exit button
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(730, 430), new Rectangle(64, 3888, 16, 16), Color.White, (float)0.0, new Vector2(0,0), (float)3.0, SpriteEffects.None, (float)0.0);
        }
    }
}
