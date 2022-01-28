using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods
{
    public class Grass
    {
        private Texture2D texture;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[A]Grass4_pipo");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    spriteBatch.Draw(texture, new Vector2((i * 85) - 5, (j * 85) - 5), new Rectangle(160, 0, 100, 95),
                        Color.White);
                }
            }
        }
    }
}
