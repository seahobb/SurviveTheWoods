using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public class Heart
    {
        private Vector2 heroPos;
        public Heart(Vector2 heroPos)
        {
            this.heroPos = heroPos; 
        }
        public Texture2D Texture { get; set; }

        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Draws the heart 
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, short x)
        {
            heroPos.X -= 380;
            heroPos.Y += 100;

            switch (x)
            {
                case 1:
                    heroPos.X += 50;
                    spriteBatch.Draw(Texture, heroPos, Color);
                    break;
                case 2:
                    heroPos.X += 100;
                    spriteBatch.Draw(Texture, heroPos, Color);
                    break;
                case 3:
                    heroPos.X += 150;
                    spriteBatch.Draw(Texture, heroPos, Color);
                    break;
                case 4:
                    heroPos.X += 200;
                    spriteBatch.Draw(Texture, heroPos, Color);
                    break;
                case 5:
                    heroPos.X += 250;
                    spriteBatch.Draw(Texture, heroPos, Color);
                    break;
            }
        }
    }
}
