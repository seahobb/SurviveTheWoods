// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SurviveTheWoods.StateManagement
{
    /// <summary>
    /// The ScreenManager is a component which manages one or more GameScreen instance.
    /// It maintains a stack of screens, calls their Update and Draw methods when 
    /// appropriate, and automatically routes input to the topmost screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        private readonly List<GameScreen> _screens = new List<GameScreen>();
        private readonly List<GameScreen> _tmpScreensList = new List<GameScreen>();

        private readonly ContentManager _content;
        private readonly InputState _input = new InputState();

        private bool _isInitialized;

        /// <summary>
        /// A SpriteBatch shared by all GameScreens
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// A SpriteFont shared by all GameScreens
        /// </summary>
        public SpriteFont TitleFont { get; private set; }

        /// <summary>
        /// A SpriteFont shared by all GameScreens
        /// </summary>
        public SpriteFont MenuFont { get; private set; }

        /// <summary>
        /// A blank texture that can be used by the screens.
        /// </summary>
        public Texture2D BlankTexture { get; private set; }

        public BaseChip BaseChip { get; private set; }

        /// <summary>
        /// Represents the hero sprite
        /// </summary>
        public Hero Hero { get; private set; }

        /// <summary>
        /// Array of the tree sprite
        /// </summary>
        public Tree[] Trees { get; private set; }

        /// <summary>
        /// Array of the autumn tree sprite
        /// </summary>
        public AutumnTree[] AutumnTrees { get; private set; }

        /// <summary>
        /// Array of the log sprite
        /// </summary>
        public Log[] Logs { get; private set; }



        /// <summary>
        /// Constructs a new ScreenManager
        /// </summary>
        /// <param name="game">The game this ScreenManager belongs to</param>
        public ScreenManager(Game game) : base(game)
        {
            _content = new ContentManager(game.Services, "Content");
        }

        /// <summary>
        /// Initializes the ScreenManager
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            _isInitialized = true;
        }

        /// <summary>
        /// Loads content for the ScreenManager and its screens
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TitleFont = _content.Load<SpriteFont>("nightFont");
            MenuFont = _content.Load<SpriteFont>("Arial");
            BaseChip = new BaseChip();

            Hero = new Hero() { Position = new Vector2(400, 200), Direction = Direction.Down };

            System.Random rand = new System.Random();

            Trees = new Tree[50];
            for (int i = 0; i < 50; i++)
            {
                Trees[i] = new Tree(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }

            AutumnTrees = new AutumnTree[20];
            for (int i = 0; i < 20; i++)
            {
                AutumnTrees[i] = new AutumnTree(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }

            Logs = new Log[20];
            for (int i = 0; i < 20; i++)
            {
                Logs[i] = new Log(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
            }
            // BlankTexture = _content.Load<Texture2D>("blank");

            //////////////////////////////////////

            BaseChip.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            foreach (var log in Logs) log.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            Hero.Texture = _content.Load<Texture2D>("Male 01-2");
            foreach (var tree in Trees) tree.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            foreach (var tree in AutumnTrees) tree.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");


            // Tell each of the screens to load their content 
            foreach (var screen in _screens)
            {
                screen.Activate();
            }
        }

        /// <summary>
        /// Unloads content for the ScreenManager's screens
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (var screen in _screens)
            {
                screen.Unload();
            }
        }

        /// <summary>
        /// Updates all screens managed by the ScreenManager
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public override void Update(GameTime gameTime)
        {
            // Read in the keyboard and gamepad
            _input.Update();

            // Make a copy of the screen list, to avoid confusion if 
            // the process of updating a screen adds or removes others
            _tmpScreensList.Clear();
            _tmpScreensList.AddRange(_screens);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            while (_tmpScreensList.Count > 0)
            {
                // Pop the topmost screen 
                var screen = _tmpScreensList[_tmpScreensList.Count - 1];
                _tmpScreensList.RemoveAt(_tmpScreensList.Count - 1);

                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
                {
                    // if this is the first active screen, let it handle input 
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(gameTime, _input);
                        otherScreenHasFocus = true;
                    }

                    // if this is an active non-popup, all subsequent 
                    // screens are covered 
                    if (!screen.IsPopup) coveredByOtherScreen = true;
                }
            }
        }

        /// <summary>
        /// Draws the appropriate screens managed by the SceneManager
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                if (screen.ScreenState == ScreenState.Hidden) continue;

                screen.Draw(gameTime);
            }
        }

        /// <summary>
        /// Adds a screen to the ScreenManager
        /// </summary>
        /// <param name="screen">The screen to add</param>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content
            if (_isInitialized) screen.Activate();

            _screens.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload its content 
            if (_isInitialized) screen.Unload();

            _screens.Remove(screen);
            _tmpScreensList.Remove(screen);
        }

        /// <summary>
        /// Exposes an array holding all the screens managed by the ScreenManager
        /// </summary>
        /// <returns>An array containing references to all current screens</returns>
        public GameScreen[] GetScreens()
        {
            return _screens.ToArray();
        }

        // Helper draws a translucent black fullscreen sprite, used for fading
        // screens in and out, and for darkening the background behind popups.
        /*public void FadeBackBufferToBlack(float alpha)
        {
             SpriteBatch.Begin();
             SpriteBatch.Draw(BlankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
             SpriteBatch.End();
            //throw new System.Exception();
        }*/

        public void Deactivate()
        {
        }

        public bool Activate()
        {
            return false;
        }
    }
}
