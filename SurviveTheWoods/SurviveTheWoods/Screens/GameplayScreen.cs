// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SurviveTheWoods.StateManagement;

namespace SurviveTheWoods.Screens
{
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;
        private EndFog _endFog;
        private BaseChip _baseChip;
        private Tree[] _trees;
        private AutumnTree[] _autumnTrees;
        private Log[] _logs;
        private Hero _hero;
        private Ghost _ghost1;
        private Ghost _ghost2;
        private Ghost _ghost3;
        private Ghost _ghost4;
        private Skeleton _skeleton1;
        private Skeleton _skeleton2;
        private Skeleton _skeleton3;
        private Skeleton _skeleton4;

        private Vector2 _playerPosition = new Vector2(100, 100);
        private Vector2 _enemyPosition = new Vector2(100, 100);

        private readonly Random _random = new Random();
        private double directionTimer;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back }, true);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("Arial");

            _endFog = ScreenManager.EndFog;
            _baseChip = ScreenManager.BaseChip;
            _trees = ScreenManager.Trees;
            _autumnTrees = ScreenManager.AutumnTrees;
            _logs = ScreenManager.Logs;
            _hero = ScreenManager.Hero;
            _ghost1 = ScreenManager.Ghost1;
            _ghost2 = ScreenManager.Ghost2;
            _ghost3 = ScreenManager.Ghost3;
            _ghost4 = ScreenManager.Ghost4;
            _skeleton1 = ScreenManager.Skeleton1;
            _skeleton2 = ScreenManager.Skeleton2;
            _skeleton3 = ScreenManager.Skeleton3;
            _skeleton4 = ScreenManager.Skeleton4;

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }


        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            _ghost1.Update(gameTime);
            _ghost2.Update(gameTime);
            _ghost3.Update(gameTime);
            _ghost4.Update(gameTime);

            _skeleton1.Update(gameTime);
            _skeleton2.Update(gameTime);
            _skeleton3.Update(gameTime);
            _skeleton4.Update(gameTime);

            _hero.Color = Color.White;
            _hero.Update(gameTime);

            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;


            if (_ghost1.Bounds.CollidesWith(_hero.Bounds) || _skeleton1.Bounds.CollidesWith(_hero.Bounds)
                || _ghost2.Bounds.CollidesWith(_hero.Bounds) || _skeleton2.Bounds.CollidesWith(_hero.Bounds)
                || _ghost3.Bounds.CollidesWith(_hero.Bounds) || _skeleton3.Bounds.CollidesWith(_hero.Bounds)
                || _ghost4.Bounds.CollidesWith(_hero.Bounds) || _skeleton4.Bounds.CollidesWith(_hero.Bounds)) 
            {
                if (directionTimer > 1.0)
                {
                    _hero.Color = Color.Red;
                    ScreenManager.HurtSound.Play();
                    directionTimer -= 1.0;
                }
            }


            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                _enemyPosition.X += (float)(_random.NextDouble() - 0.5) * randomization;
                _enemyPosition.Y += (float)(_random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                var targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - _gameFont.MeasureString("Insert Gameplay Here").X / 2,
                    200);

                _enemyPosition = Vector2.Lerp(_enemyPosition, targetPosition, 0.05f);

                // This game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                var movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                var thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                _playerPosition += movement * 8f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.DarkOliveGreen, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;


            float offsetX = 435 - _hero.Position.X;
            float offsetY = 280 - _hero.Position.Y;
        
            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0); 

            spriteBatch.Begin(transformMatrix: transform);
            
            _baseChip.Draw(gameTime, spriteBatch);
            foreach (var log in _logs) log.Draw(gameTime, spriteBatch);
            _hero.Draw(gameTime, spriteBatch);

            _ghost1.Draw(gameTime, spriteBatch);
            _ghost2.Draw(gameTime, spriteBatch);
            _ghost3.Draw(gameTime, spriteBatch);
            _ghost4.Draw(gameTime, spriteBatch);
            
            _skeleton1.Draw(gameTime, spriteBatch);
            _skeleton2.Draw(gameTime, spriteBatch);
            _skeleton3.Draw(gameTime, spriteBatch);
            _skeleton4.Draw(gameTime, spriteBatch);

            foreach (var tree in _trees) tree.Draw(gameTime, spriteBatch);
            foreach (var tree in _autumnTrees) tree.Draw(gameTime, spriteBatch);
            _endFog.Draw(gameTime, spriteBatch);

            //spriteBatch.DrawString(_gameFont, "// TODO", _playerPosition, Color.Green);
            //spriteBatch.DrawString(_gameFont, "Insert Gameplay Here",
            //                      _enemyPosition, Color.DarkRed);

            spriteBatch.End();




            // If the game is transitioning on or off, fade it out to black.
            /*if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }*/
        }
    }
}
