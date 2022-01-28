using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheWoods
{
    public class STWGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Grass grass;
        private SpriteFont font;


        public STWGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            grass = new Grass();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grass.LoadContent(Content);
            font = Content.Load<SpriteFont>("nightFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Color green = new Color(107, 158, 0);
            GraphicsDevice.Clear(green);
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // float x = (float)GraphicsDevice.Viewport.Width / 2;
            //float y = (float)GraphicsDevice.Viewport.Height / 2;
            grass.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(font, "Survive the \nWoods!", new Vector2(220, 50), Color.SeaShell);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
