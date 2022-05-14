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

        private BoundingRectangle boundsRect = new BoundingRectangle(new Vector2(400 - 16, 200 - 16), 32, 32);

        private BoundingCircle bounds = new BoundingCircle(new Vector2(640, 640), 16);

        private Direction Direction = Direction.Down;

        private Vector2 position = new Vector2(640, 640);

        private Vector2 text_pos = new Vector2(640 - 430, 640 - 280);

        public bool PreventSpriteUp { get; set; } = false;
        public bool PreventSpriteBottom { get; set; } = false;
        public bool PreventSpriteLeft { get; set; } = false;
        public bool PreventSpriteRight { get; set; } = false;

        public bool InjuredSprite { get; set; }

        public bool DeadSprite { get; set; }

        public Vector2 Position 
        {
            get { return position; }
            private set { }
        }

        /// <summary>
        /// Bounds of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Bounds of the sprite
        /// </summary>
        public BoundingRectangle BoundsRect => boundsRect;

        /// <summary>
        /// Default color of when sprite might get injured
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Sprite texture
        /// </summary>
        public Texture2D Texture { get; set; }

        public bool firstTry { get; set; } = true;

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
            if (DeadSprite)
            {
                position = new Vector2(640, 640);
                text_pos = new Vector2(640 - 430, 640 - 280);
                firstTry = false;
                DeadSprite = false;
            }
            else if (InjuredSprite)
            {
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W) && !PreventSpriteUp)
                {
                    position += new Vector2(0, -1) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(0, -1) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Up;
                }

                else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S) && !PreventSpriteBottom)
                {
                    position += new Vector2(0, 1) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(0, 1) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Down;
                }

                else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) && !PreventSpriteLeft)
                {
                    position += new Vector2(-1, 0) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(-1, 0) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Left;
                }

                else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) && !PreventSpriteRight)
                {
                    position += new Vector2(1, 0) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(-1, 0) * 55 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Right;
                }
            }
            else if (!InjuredSprite && !DeadSprite)
            {
                if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) && !PreventSpriteUp)
                {//600 speed
                    position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Up;
                }

                else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S) && !PreventSpriteBottom)
                {
                    position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Down;
                }

                else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) && !PreventSpriteLeft)
                {
                    position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Left;
                }

                else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) && !PreventSpriteRight)
                {
                    position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    text_pos += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = Direction.Right;
                }
            }





            boundsRect.X = position.X - 16;
            boundsRect.Y = position.Y - 16;
            bounds.Center = position;// new Vector2(position.X - 32, position.Y - 32);
        }

        /// <summary>
        /// Draws the animated hero sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the sprite batch to draw with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Graphics.SpriteFont gameFont, int keysLeft, bool gameOver)
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
            if (InjuredSprite)
                spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
            else if (!InjuredSprite)
                spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);
            else if (DeadSprite)
                spriteBatch.Draw(Texture, position, source, Color, 0, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0);

            if (gameOver)
            {

                //text_pos: 300, -240
                //text_pos.X -= 1;
                text_pos.X = 520;
                text_pos.Y = -150;
                
                string s = "You survived! Press 'enter' to exit";

                spriteBatch.DrawString(gameFont, s, text_pos, Color.White);

                if (keyboardState.IsKeyDown(Keys.Enter)) System.Windows.Forms.Application.Exit();
            }
            else spriteBatch.DrawString(gameFont, $"Keys left: {keysLeft}", text_pos, Color.White);
        }
    }
}
