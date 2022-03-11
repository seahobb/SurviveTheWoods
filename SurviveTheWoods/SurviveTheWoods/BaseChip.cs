using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class BaseChip
    {
        //private Texture2D texture;
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// Loads the atlas texture
        /// </summary>
        /// <param name="content">the atlas texture</param>
       /* public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }*/

        /// <summary>
        /// Draws certain parts of the atlas texture 
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //80*16 = 1280x1280
            for (int i = 0; i < 83; i++) //50
            {
                for (int j = 0; j < 83; j++) //30 
                {
                    //800 width, 480 height. draw as many grass as to cover this area
                    spriteBatch.Draw(Texture, new Vector2(i*16, j*16), new Rectangle(0, 0, 16, 16), Color.White);
                    
                }
            }
        }
    }
}
