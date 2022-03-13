using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public class Skeleton
    {
        private double animationTimer;

        private short animationFrame;

        // private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200 - 16, 300 - 16), 32, 32);
        private BoundingRectangle bounds;

        private Direction Direction = Direction.Down;

        //private Vector2 position = new Vector2(200, 300);
        private Vector2 position;

        private double directionTimer;

        private int x;

        private int y;

        public bool Dead { get; set; } = false;

        public Skeleton(ref System.Random r)
        {
            x = r.Next(200, 1128);
            y = r.Next(200, 1128);
            bounds = new BoundingRectangle(new Vector2(x - 16, y - 16), 32, 32);
            position = new Vector2(x, y);
        }

        /// <summary>
        /// Bounds of the skeleton
        /// </summary>
        public BoundingRectangle Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        /// <summary>
        /// Position of the skeleton
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// Default color of when sprite might get injured
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Sprite texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Updates the sprite to walk automatically
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            if (!Dead)
            {
                // Update the direction timer
                directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

                // Random rand = new Random();

                if (directionTimer > 5.0)
                {
                    switch (Direction)//rand.Next(0, 3))
                    {
                        case Direction.Right:
                            Direction = Direction.Down;
                            break;
                        case Direction.Up:
                            Direction = Direction.Right;
                            break;
                        case Direction.Down:
                            Direction = Direction.Left;
                            break;
                        case Direction.Left:
                            Direction = Direction.Up;
                            break;
                    }
                    directionTimer -= 5.0;
                }

                // Move the sprite in the direction it is walking
                switch (Direction)
                {
                    case Direction.Up:
                        position += new Vector2(0, -1) * 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Down:
                        position += new Vector2(0, 1) * 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Left:
                        position += new Vector2(-1, 0) * 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Right:
                        position += new Vector2(1, 0) * 20 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                }

                bounds.X = position.X - 16;
                bounds.Y = position.Y - 16;
            }
            
        }

        /// <summary>
        /// Draws the animated sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Dead)
            {
                // Update animation timer
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                // Update animation frame
                if (animationTimer > 0.25)
                {
                    animationFrame++;
                    if (animationFrame > 2) animationFrame = 0;
                    animationTimer -= 0.25;
                }

                // Draw the sprite
                var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
                spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                // Draw the sprite
                var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
                spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.FlipVertically, 0);
            }
        }
    }
}
