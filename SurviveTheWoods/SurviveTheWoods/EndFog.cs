using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public class EndFog
    {
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Draws certain parts of the atlas texture 
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //sides
            /*for (int y = 0; y < 1440; y += 240)
            {
                spriteBatch.Draw(Texture, new Vector2(-320, y), Color.White);
            }
            for (int y = -240; y < 1680; y += 240)
            {
                spriteBatch.Draw(Texture, new Vector2(1280+48, y), Color.White);
            }

            //top and bottom
            for (int x = -320; x < 1280; x += 320)
            {
                spriteBatch.Draw(Texture, new Vector2(x+48, -240), Color.White);
            }
            for (int x = -320; x < 1280; x += 320)
            {
                spriteBatch.Draw(Texture, new Vector2(x+48+112, 1280+48), Color.White);
            }*/
        }
    }
}
