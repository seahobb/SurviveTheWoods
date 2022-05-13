using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public class Ghost
    {
        private double animationTimer;

        private short animationFrame;

        //private BoundingRectangle bounds = new BoundingRectangle(new Vector2(500 - 16, 70 - 16), 32, 32);
        private BoundingRectangle boundsRect;

        private BoundingCircle bounds;

        private Direction Direction = Direction.Down;

       // private Vector2 position = new Vector2(500, 70); //500, 70
        private Vector2 position;

        private double directionTimer;

        private int x;

        private int y;

        public Ghost(ref System.Random r)
        {
            x = r.Next(200, 1128);
            y = r.Next(200, 1128);
            boundsRect = new BoundingRectangle(new Vector2(x - 16, y - 16), 32, 32);
            bounds = new BoundingCircle(new Vector2(x, y), 16);
            position = new Vector2(x, y);

        }

        /// <summary>
        /// Bounds of the sprite
        /// </summary>
        public BoundingRectangle BoundsRect
        {
            get => boundsRect;
            set => boundsRect = value;
        }

        /// <summary>
        /// bounds of the sprite
        /// </summary>
        public BoundingCircle Bounds
        {
            get => bounds;
            set => bounds = value;
        }

        /// <summary>
        /// Position of the sprite
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public bool Dead { get; set; } = false;

        /// <summary>
        /// Default color of when sprite might get injured
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Sprite texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Updates the ghost sprite to walk automatically
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

                // Move the ghost in the direction it is walking
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

                boundsRect.X = position.X - 16;
                boundsRect.Y = position.Y - 16;
                //bounds.Center = position;
                bounds.Center = position;// new Vector2(position.X - 32, position.Y - 32);
            }
            
        }

        /// <summary>
        /// Draws the animated ghost sprite
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
                spriteBatch.Draw(Texture, position, source, Color * 0.5f, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                //float rotate = (float)MathHelper.Pi / 2;

                var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
                spriteBatch.Draw(Texture, position, source, Color * 0.5f, 90, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
            }


        }
    }
}
