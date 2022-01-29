using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheWoods
{
    public class STWGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private BaseChip baseChip;
        private Hero hero;
        private Tree[] trees;
        private AutumnTree[] autumnTrees;
        private Log[] logs;
        private ExitGameButton exit;
        private SpriteFont font;
        private SpriteFont arialFont;

        private InputManager inputManager;
        
        /// <summary>
        /// A survival game
        /// </summary>
        public STWGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            baseChip = new BaseChip();

            hero = new Hero() { Position = new Vector2(400, 200), Direction = Direction.Down};

            System.Random rand = new System.Random();

            trees = new Tree[50];
            for (int i = 0; i < 50; i++)
            {
                trees[i] = new Tree(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }

            autumnTrees = new AutumnTree[20];
            for (int i = 0; i < 20; i++)
            {
                autumnTrees[i] = new AutumnTree(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }

            logs = new Log[20];
            for (int i = 0; i < 20; i++)
            {
                logs[i] = new Log(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }

            exit = new ExitGameButton();

            inputManager = new InputManager();

            base.Initialize();
        }

        /// <summary>
        /// Loads content for the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            baseChip.LoadContent(Content);
            foreach (var log in logs) log.LoadContent(Content);
            hero.LoadContent(Content);
            foreach (var tree in trees) tree.LoadContent(Content);
            foreach (var tree in autumnTrees) tree.LoadContent(Content);
            exit.LoadContent(Content);
            font = Content.Load<SpriteFont>("nightFont");
            arialFont = Content.Load<SpriteFont>("Arial");
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Update(GameTime gameTime)
        {
            hero.Update(gameTime);

            inputManager.Update(gameTime);

            if (inputManager.ExitButtonPressed) Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Draw(GameTime gameTime)
        {
            Color green = new Color(107, 158, 0);
            GraphicsDevice.Clear(green);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            baseChip.Draw(gameTime, _spriteBatch);
            foreach (var log in logs) log.Draw(gameTime, _spriteBatch);
            hero.Draw(gameTime, _spriteBatch);
            foreach (var tree in trees) tree.Draw(gameTime, _spriteBatch);
            foreach (var tree in autumnTrees) tree.Draw(gameTime, _spriteBatch);
            exit.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(font, "Survive the \nWoods!", new Vector2(220, 50), Color.SeaShell);
            //game doesn't actually start yet
            _spriteBatch.DrawString(arialFont, "Press 'space' to start", new Vector2(310, 420), Color.SeaShell);
            _spriteBatch.DrawString(arialFont, "Exit", new Vector2(735, 405), Color.SeaShell);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
