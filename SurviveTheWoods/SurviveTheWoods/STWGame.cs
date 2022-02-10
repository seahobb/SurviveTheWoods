// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using SurviveTheWoods;
using SurviveTheWoods.Screens;
using SurviveTheWoods.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurviveTheWoods
{
    public class STWGame : Game
    {
        private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        //private ExitGameButton exit;
        // private SpriteFont font;
        // private SpriteFont arialFont;

        private InputManager inputManager;
        
        /// <summary>
        /// A survival game
        /// </summary>
        public STWGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           

            //useful for other classes: inputManager = new InputManager();

            base.Initialize();
        }

        /// <summary>
        /// Loads content for the game
        /// </summary>
        protected override void LoadContent()
        {
           // _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Update(GameTime gameTime)
        {
            //hero.Update(gameTime);

            //useful for other classes: inputManager.Update(gameTime);

            //if (inputManager.ExitButtonPressed) Exit();

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
            base.Draw(gameTime);    // The real drawing happens inside the ScreenManager component

            /////////////////////////////////

            

            // TODO: Add your drawing code here
            //_spriteBatch.Begin();

           // baseChip.Draw(gameTime, _spriteBatch);
            /*foreach (var log in logs) log.Draw(gameTime, _spriteBatch);
            hero.Draw(gameTime, _spriteBatch);
            foreach (var tree in trees) tree.Draw(gameTime, _spriteBatch);
            foreach (var tree in autumnTrees) tree.Draw(gameTime, _spriteBatch);*/
            //exit.Draw(gameTime, _spriteBatch);
            //_spriteBatch.DrawString(font, "Survive the \nWoods!", new Vector2(220, 50), Color.SeaShell);
            //game doesn't actually start yet
            //_spriteBatch.DrawString(arialFont, "Press 'space' to start", new Vector2(310, 420), Color.SeaShell);
            //_spriteBatch.DrawString(arialFont, "Exit", new Vector2(735, 405), Color.SeaShell);

           // _spriteBatch.End();

            //base.Draw(gameTime);
        }

       
    }
}
