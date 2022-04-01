using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class Water
    {
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Draws certain parts of the atlas texture 
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //top left point on map, top left point on pic, size of pic
            spriteBatch.Draw(Texture, new Vector2(785, 290), new Rectangle(31, 15, 27, 26), Color.White); //corner
            spriteBatch.Draw(Texture, new Vector2(27+ 785, 290), new Rectangle(35, 15, 23, 26), Color.White); //top
            spriteBatch.Draw(Texture, new Vector2(27+23+ 785, 290), new Rectangle(35, 15, 23, 26), Color.White); //top
            spriteBatch.Draw(Texture, new Vector2(27+23+23+ 785, 290), new Rectangle(35, 15, 23, 26), Color.White); //top
            spriteBatch.Draw(Texture, new Vector2(27+23+23+23+ 785, 290), new Rectangle(35, 15, 23, 26), Color.White); //top
            spriteBatch.Draw(Texture, new Vector2(27+23+23+23+23+ 785, 290), new Rectangle(35, 15, 23, 26), Color.White); //top
            spriteBatch.Draw(Texture, new Vector2(27+23+23+23+23+23+ 785, 290), new Rectangle(39, 15, 27, 26), Color.White); //corner
            

            for (int i = 1; i < 6; i++)
            {
                spriteBatch.Draw(Texture, new Vector2(785, (25*i)+290), new Rectangle(31, 21, 27, 26), Color.White);
            }
            spriteBatch.Draw(Texture, new Vector2(785, (25 * 6)+290), new Rectangle(31, 24, 27, 26), Color.White); //corner


            //bottom:
            spriteBatch.Draw(Texture, new Vector2(27+ 785, (25*6)+290), new Rectangle(35, 24, 32, 26), Color.White);
           // spriteBatch.Draw(Texture, new Vector2(27+23+ 785, (25*6)+290), new Rectangle(35, 24, 27, 26), Color.White);
            //spriteBatch.Draw(Texture, new Vector2(27+23+23+ 785, (25*6)+255), new Rectangle(35, 24, 27, 26), Color.White);
           // spriteBatch.Draw(Texture, new Vector2(27+23+23+23+ 785, (25*6)+290), new Rectangle(35, 24, 27, 26), Color.White);
            spriteBatch.Draw(Texture, new Vector2(27+23+23+23+23+ 777, (25*6)+290), new Rectangle(35, 24, 32, 26), Color.White);
            spriteBatch.Draw(Texture, new Vector2(27+23+23+23+23+23+ 785, (25*6)+290), new Rectangle(40, 24, 27, 26), Color.White); //corner



            //right side
            for (int i = 1; i < 6; i++)
            {
                spriteBatch.Draw(Texture, new Vector2(27 + 23 + 23 + 23 + 23 + 23+ 785, (25 * i)+290), new Rectangle(40, 20, 27, 26), Color.White);
            }

            //middle

            for (int i = 1; i < 12; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    spriteBatch.Draw(Texture, new Vector2((13 * j)+785, (13* i)+290), new Rectangle(50, 0, 13, 13), Color.White);
                }
            }
        }
    }
}
