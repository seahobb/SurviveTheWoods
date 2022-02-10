// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SurviveTheWoods.StateManagement;
using SurviveTheWoods;
using Microsoft.Xna.Framework.Content;

namespace SurviveTheWoods.Screens
{
    // Base class for screens that contain a menu of options. The user can
    // move up and down to select an entry, or cancel to back out of the screen.
    public abstract class MenuScreen : GameScreen
    {
        /// <summary>
        /// Represents the base sprite chip
        /// </summary>
       /* public BaseChip BaseChip { get; private set; }

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
        public Log[] Logs { get; private set; }*/

        //private BaseChip baseChip;
       //////////////////////////////////// private Hero hero;
       // private Tree[] trees;
       // private AutumnTree[] autumnTrees;
        //private Log[] logs;

        private readonly List<MenuEntry> _menuEntries = new List<MenuEntry>();
        private int _selectedEntry;
        private readonly string _menuTitle;
        private ContentManager _content;

        private readonly InputAction _menuUp;
        private readonly InputAction _menuDown;
        private readonly InputAction _menuSelect;
        private readonly InputAction _menuCancel;

        // Gets the list of menu entries, so derived classes can add or change the menu contents.
        protected IList<MenuEntry> MenuEntries => _menuEntries;

        protected MenuScreen(string menuTitle)
        {
            _menuTitle = menuTitle;
            // LoadSprites();
            //////////////////hero = ScreenManager.Hero;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _menuUp = new InputAction(
                new[] { Buttons.DPadUp, Buttons.LeftThumbstickUp },
                new[] { Keys.Up }, true);
            _menuDown = new InputAction(
                new[] { Buttons.DPadDown, Buttons.LeftThumbstickDown },
                new[] { Keys.Down }, true);
            _menuSelect = new InputAction(
                new[] { Buttons.A, Buttons.Start },
                new[] { Keys.Enter, Keys.Space }, true);
            _menuCancel = new InputAction(
                new[] { Buttons.B, Buttons.Back },
                new[] { Keys.Back }, true);
        }

      /*  private void LoadSprites()
        {
            baseChip = new BaseChip();

            hero = new Hero() { Position = new Vector2(400, 200), Direction = Direction.Down };

            System.Random rand = new System.Random();

            trees = new Tree[50];
            for (int i = 0; i < 50; i++)
            {
                trees[i] = new Tree(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height));
            }

            autumnTrees = new AutumnTree[20];
            for (int i = 0; i < 20; i++)
            {
                autumnTrees[i] = new AutumnTree(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height));
            }

            logs = new Log[20];
            for (int i = 0; i < 20; i++)
            {
                logs[i] = new Log(new Vector2((float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * ScreenManager.GraphicsDevice.Viewport.Height));
            }

            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            // BaseChip = //do what i do in BaseChip.LoadContent here if below doesn't work
            baseChip.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            //BaseChip.LoadContent(_content);
            // foreach (var log in Logs) log.LoadContent(_content);
            foreach (var log in logs) log.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            //Hero.LoadContent(_content);
            hero.Texture = _content.Load<Texture2D>("Male 01-2");
            //foreach (var tree in Trees) tree.LoadContent(_content);
            foreach (var tree in trees) tree.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");
            //foreach (var tree in AutumnTrees) tree.LoadContent(_content);
            foreach (var tree in autumnTrees) tree.Texture = _content.Load<Texture2D>("[Base]BaseChip_pipo16");

            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }*/

        // Responds to user input, changing the selected entry and accepting or cancelling the menu.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            // For input tests we pass in our ControllingPlayer, which may
            // either be null (to accept input from any player) or a specific index.
            // If we pass a null controlling player, the InputState helper returns to
            // us which player actually provided the input. We pass that through to
            // OnSelectEntry and OnCancel, so they can tell which player triggered them.
            PlayerIndex playerIndex;

            if (_menuUp.Occurred(input, ControllingPlayer, out playerIndex))
            {
                _selectedEntry--;

                if (_selectedEntry < 0)
                    _selectedEntry = _menuEntries.Count - 1;
            }

            if (_menuDown.Occurred(input, ControllingPlayer, out playerIndex))
            {
                _selectedEntry++;

                if (_selectedEntry >= _menuEntries.Count)
                    _selectedEntry = 0;
            }

            if (_menuSelect.Occurred(input, ControllingPlayer, out playerIndex))
                OnSelectEntry(_selectedEntry, playerIndex);
            else if (_menuCancel.Occurred(input, ControllingPlayer, out playerIndex))
                OnCancel(playerIndex);
        }

        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            _menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }

        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        // Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        // Allows the screen the chance to position the menu entries. By default,
        // all menu entries are lined up in a vertical list, centered on the screen.
        protected virtual void UpdateMenuEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            var position = new Vector2(0f, 420f);

            // update each menu entry's location in turn
            foreach (var menuEntry in _menuEntries)
            {
                // each entry is to be centered horizontally
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

           ///////////////////// hero.Update(gameTime);

            // Update each nested MenuEntry object.
            for (int i = 0; i < _menuEntries.Count; i++)
            {
                bool isSelected = IsActive && i == _selectedEntry;
                _menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();

            var graphics = ScreenManager.GraphicsDevice;
            var spriteBatch = ScreenManager.SpriteBatch;
            var menuFont = ScreenManager.MenuFont;
            var titleFont = ScreenManager.TitleFont;
            var baseChip = ScreenManager.BaseChip;
            var trees = ScreenManager.Trees;
            var autumnTrees = ScreenManager.AutumnTrees;
            var logs = ScreenManager.Logs;
            /////////hero = ScreenManager.Hero;

            spriteBatch.Begin();

            baseChip.Draw(gameTime, spriteBatch);
            foreach (var log in logs) log.Draw(gameTime, spriteBatch);
            ////////////hero.Draw(gameTime, spriteBatch);
            foreach (var tree in trees) tree.Draw(gameTime, spriteBatch);
            foreach (var tree in autumnTrees) tree.Draw(gameTime, spriteBatch);

            for (int i = 0; i < _menuEntries.Count; i++)
            {
                var menuEntry = _menuEntries[i];
                bool isSelected = IsActive && i == _selectedEntry;
                menuEntry.Draw(this, isSelected, gameTime);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            var titlePosition = new Vector2(graphics.Viewport.Width / 2, 160);
            var titleOrigin = titleFont.MeasureString(_menuTitle) / 2;
            var titleColor = Color.SeaShell * TransitionAlpha;
            const float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(titleFont, _menuTitle, titlePosition, titleColor,
                0, titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
