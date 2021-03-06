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

        private readonly ScreenManager _screenManager;

        //private InputManager inputManager;
        private static System.Random random = new System.Random();

        public System.Random Random => random;

        //https://github.com/CartBlanche/MonoGame-Samples/blob/7deb685071fb1e356d46b89328e142e7d991cb27/ParticleSample/ParticleSampleGame.cs#L304
        public static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        /// <summary>
        /// A survival game
        /// </summary>
        public STWGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();

            //mainmenu->gameplayscreen (send this to gameplayscreen)
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(this), null);
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
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Update(GameTime gameTime)
        {
            //useful for other classes: inputManager.Update(gameTime);

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
        }

       
    }
}
