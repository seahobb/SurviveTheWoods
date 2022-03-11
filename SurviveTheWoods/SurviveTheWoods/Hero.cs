using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SurviveTheWoods.Collisions;

namespace SurviveTheWoods
{
    public enum Direction
    {
        Down = 0,
        Left = 1,
        Right = 2,
        Up = 3
    }

    public class Hero
    {
        private double animationTimer;

        private short animationFrame;

        private KeyboardState keyboardState;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(400 - 16, 200 - 16), 32, 32);

        private Direction Direction = Direction.Down;

        private Vector2 position = new Vector2(640, 640);

        public Vector2 Position 
        {
            get { return position; }
            private set { }
        }

        /// <summary>
        /// Bounds of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Default color of when sprite might get injured
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Sprite texture
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Loads the hero sprite
        /// </summary>
        /// <param name="content">the hero sprite</param>
        /*public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Male 01-2");
        }*/

        /// <summary>
        /// Updates the hero sprite to walk with user input
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1) * 75 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Direction = Direction.Up;
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1) * 75 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Direction = Direction.Down;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0) * 75 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Direction = Direction.Left;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0) * 75 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Direction = Direction.Right;
            }

            bounds.X = position.X - 16;
            bounds.Y = position.Y - 16;
        }

        /// <summary>
        /// Draws the animated hero sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            keyboardState = Keyboard.GetState();

            bool keyDown = false;
            if (keyboardState.IsKeyDown(Keys.W) ||
                keyboardState.IsKeyDown(Keys.A) ||
                keyboardState.IsKeyDown(Keys.S) ||
                keyboardState.IsKeyDown(Keys.D) ||
                keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.Down) ||
                keyboardState.IsKeyDown(Keys.Right))
            {
                keyDown = true;
            }

            // Update animation frame
            if (animationTimer > 0.15 && keyDown)
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= 0.15;
            }

            // Draw the sprite
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
            spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
        }
    }
}
