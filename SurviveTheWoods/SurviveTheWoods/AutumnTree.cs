using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods
{
    public class AutumnTree
    {
        private Vector2 position;

        /// <summary>
        /// Texture of the tree
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Sets tree position
        /// </summary>
        /// <param name="position">the position of the tree</param>
        public AutumnTree(Vector2 position)
        {
            this.position = position;
            //this.bounds = new BoundingRectangle()
        }

        /// <summary>
        /// Loads the tree sprite
        /// </summary>
        /// <param name="content">the tree sprite</param>
        /*public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("[Base]BaseChip_pipo16");
        }*/

        /// <summary>
        /// Draws the tree sprite
        /// </summary>
        /// <param name="gameTime">current game time</param>
        /// <param name="spriteBatch">the sprite batch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, new Rectangle(64, 16, 32, 32), Color.White);
        }
    }
}
