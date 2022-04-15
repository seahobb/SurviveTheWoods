// Adapted from the MonoGame port of the original XNA GameStateExample 
// https://github.com/tomizechsterson/game-state-management-monogame

using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SurviveTheWoods.StateManagement;
using SurviveTheWoods.ParticleSystem;
using System.Collections.Generic;
using System.Text;

namespace SurviveTheWoods.Screens
{
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;
        private EndFog _endFog;
        private BaseChip _baseChip;
        private Water _water;
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

       /* private Heart _heart1;
        private Heart _heart2;
        private Heart _heart3;
        private Heart _heart4;
        private Heart _heart5;*/

        private Ghost[] ghostArr = new Ghost[4];
        private Skeleton[] skeletonArr = new Skeleton[4];
        private int[] skeletonHealth = new int[4];
        private int[] ghostHealth = new int[4];

        private HeartHealth heartHealth1;
        private HeartHealth heartHealth2;
        private HeartHealth heartHealth3;
        private HeartHealth heartHealth4;
        private HeartHealth heartHealth5;
        private HeartHealth heartHealth6;
        private HeartHealth heartHealth7;
        private HeartHealth heartHealth8;
        private HeartHealth heartHealth9;
        private HeartHealth heartHealth10;

        BloodParticleSystem _blood;

        Tilemap _tilemap;

        //private Game _game;

        private Vector2 _playerPosition = new Vector2(100, 100);
        private Vector2 _enemyPosition = new Vector2(100, 100);

        private readonly Random _random = new Random();
        private double directionTimer;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;


        private short hurtCount = 0;

        //private OceanParticleSystem oceans;
        private STWGame passgame;
        public GameplayScreen(STWGame passgame)
        {
            this.passgame = passgame;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back }, true);

            _blood = new BloodParticleSystem(passgame, 20);
            passgame.Components.Add(_blood);

        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("Arial");

            _tilemap = ScreenManager.Tilemap;

            _endFog = ScreenManager.EndFog;
            _baseChip = ScreenManager.BaseChip;
            _water = ScreenManager.Water;
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

            ghostArr[0] = _ghost1;
            ghostArr[1] = _ghost2;
            ghostArr[2] = _ghost3;
            ghostArr[3] = _ghost4;

            skeletonArr[0] = _skeleton1;
            skeletonArr[1] = _skeleton2;
            skeletonArr[2] = _skeleton3;
            skeletonArr[3] = _skeleton4;

            for (int i = 0; i < 3; i++)
            {
                skeletonHealth[i] = 3;
                ghostHealth[i] = 3;
            }

            heartHealth1 = ScreenManager.HeartHealth1;
            heartHealth2 = ScreenManager.HeartHealth2;
            heartHealth3 = ScreenManager.HeartHealth3;
            heartHealth4 = ScreenManager.HeartHealth4;
            heartHealth5 = ScreenManager.HeartHealth5;
            heartHealth6 = ScreenManager.HeartHealth6;
            heartHealth7 = ScreenManager.HeartHealth7;
            heartHealth8 = ScreenManager.HeartHealth8;
            heartHealth9 = ScreenManager.HeartHealth9;
            heartHealth10 = ScreenManager.HeartHealth10;

            /* _heart1 = ScreenManager.Heart1;
             _heart2 = ScreenManager.Heart2;
             _heart3 = ScreenManager.Heart3;
             _heart4 = ScreenManager.Heart4;
             _heart5 = ScreenManager.Heart5;*/

           /* health[0] = (_heart1);
            health[1] = (_heart2);
            health[2] = (_heart3);
            health[3] = (_heart4);
            health[4] = (_heart5);*/


            //oceans = new OceanParticleSystem(ScreenManager.Game, 20);
            //_game = ScreenManager.Game;
            //_game.Components.Add(oceans);

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



        private Heart[] health = new Heart[5];
        private KeyboardState keyboardState;
        

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            Vector2 changingPos = new Vector2(0, 0);
            //changingPos.X -= 150; //-=640
            //changingPos.Y -= 150;
            /*if (_hero.Position.X > 300)
            {
                changingPos.X += 300;
                oceans.PlaceOcean(changingPos);
            }
                
            if (_hero.Position.Y > 300)
            {
                changingPos.Y += 300;
                oceans.PlaceOcean(changingPos);
            }*/

            //then try to subtract amount that matrix transform does


            

            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            ghostArr[0].Update(gameTime);
            ghostArr[1].Update(gameTime);
            ghostArr[2].Update(gameTime);
            ghostArr[3].Update(gameTime);

            skeletonArr[0].Update(gameTime);
            skeletonArr[1].Update(gameTime);
            skeletonArr[2].Update(gameTime);
            skeletonArr[3].Update(gameTime);

           /* _heart1.Update(gameTime);
            _heart2.Update(gameTime);
            _heart3.Update(gameTime);
            _heart4.Update(gameTime);
            _heart5.Update(gameTime);*/

            _hero.Color = Color.White;
           
            _hero.Update(gameTime);

            heartHealth1.Update(gameTime);
            heartHealth2.Update(gameTime);
            heartHealth3.Update(gameTime);
            heartHealth4.Update(gameTime);
            heartHealth5.Update(gameTime);
            heartHealth6.Update(gameTime);
            heartHealth7.Update(gameTime);
            heartHealth8.Update(gameTime);
            heartHealth9.Update(gameTime);
            heartHealth10.Update(gameTime);

            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < 4; i++)
            {
                
                if ((ghostArr[i].Bounds.CollidesWith(_hero.Bounds) && !ghostArr[i].Dead) ||
                        (skeletonArr[i].Bounds.CollidesWith(_hero.Bounds) && !skeletonArr[i].Dead))
                {
                    if (directionTimer > 0.5)
                    {
                        _hero.Color = Color.Red;
                        ScreenManager.HurtSound.Play();
                        hurtCount++;
                        //health[hurtCount-1].Color = Color.Black;

                        if (hurtCount == 8)
                        {
                            _hero.InjuredSprite = true;
                        }
                        if (hurtCount >= 10)
                        {
                            _blood.PlaceBlood(new Vector2(380, 250));
                            _hero.DeadSprite = true;
                            _hero.InjuredSprite = false;
                            hurtCount = 0;
                        }

                        directionTimer -= 0.5;
                    }


                    // Detect hero hitting monster
                    keyboardState = Keyboard.GetState();

                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        if (ghostArr[i].Bounds.CollidesWith(_hero.Bounds))
                        {
                            //check if dead
                            if (ghostHealth[i] == 0)
                            {
                                ghostArr[i].Color = Color.Black;
                                ghostArr[i].Dead = true;
                            }
                            else
                            {
                                ghostHealth[i]--;
                                ghostArr[i].Color = Color.Red;
                                ScreenManager.HurtSound.Play();
                            }
                        }
                        else if (skeletonArr[i].Bounds.CollidesWith(_hero.Bounds))
                        {
                            //check if dead
                            if (skeletonHealth[i] == 0)
                            {
                                skeletonArr[i].Color = Color.Black;
                                skeletonArr[i].Dead = true;
                            }
                            else
                            {
                                skeletonHealth[i]--;
                                skeletonArr[i].Color = Color.Red;
                                ScreenManager.HurtSound.Play();
                            }
                        }
                    }
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
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.DarkOliveGreen, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;


            float offsetX = 435 - _hero.Position.X;
            float offsetY = 280 - _hero.Position.Y;
        
            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0); 

            spriteBatch.Begin(transformMatrix: transform);

           // _baseChip.Draw(gameTime, spriteBatch);
            _tilemap.Draw(gameTime, spriteBatch);
            
            foreach (var log in _logs) log.Draw(gameTime, spriteBatch);
            _hero.Draw(gameTime, spriteBatch);

            /*ghostArr[0].Draw(gameTime, spriteBatch);
            ghostArr[1].Draw(gameTime, spriteBatch);
            ghostArr[2].Draw(gameTime, spriteBatch);
            ghostArr[3].Draw(gameTime, spriteBatch);*/

            skeletonArr[0].Draw(gameTime, spriteBatch);
            skeletonArr[1].Draw(gameTime, spriteBatch);
            skeletonArr[2].Draw(gameTime, spriteBatch);
            skeletonArr[3].Draw(gameTime, spriteBatch);

            /*_heart1.Draw(gameTime, spriteBatch, 1);
            _heart2.Draw(gameTime, spriteBatch, 2);
            _heart3.Draw(gameTime, spriteBatch, 3);
            _heart4.Draw(gameTime, spriteBatch, 4);
            _heart5.Draw(gameTime, spriteBatch, 5);*/

            foreach (var tree in _trees) tree.Draw(gameTime, spriteBatch);

            foreach (var tree in _autumnTrees) tree.Draw(gameTime, spriteBatch);

            _water.Draw(gameTime, spriteBatch);
            //_endFog.Draw(gameTime, spriteBatch);

            //spriteBatch.DrawString(_gameFont, "// TODO", _playerPosition, Color.Green);
            //spriteBatch.DrawString(_gameFont, "Insert Gameplay Here",
            //                      _enemyPosition, Color.DarkRed);

            spriteBatch.End();

            //BlendState bs = new BlendState();
            //bs.AlphaSourceBlend = Blend.SourceAlpha;
           // bs.AlphaDestinationBlend = Blend.One;
           // bs.AlphaDestinationBlend = Blend.One;
            //bs.AlphaBlendFunction = Color.White * (float)bs / (float)byte.MaxValue;


            spriteBatch.Begin(transformMatrix: transform, blendState: BlendState.AlphaBlend);
            
            ghostArr[0].Draw(gameTime, spriteBatch);
            ghostArr[1].Draw(gameTime, spriteBatch);
            ghostArr[2].Draw(gameTime, spriteBatch);
            ghostArr[3].Draw(gameTime, spriteBatch);
            spriteBatch.End();

            /* spriteBatch.Begin();
             var origin = new Vector2(199, 99);
             var font = ScreenManager.MenuFont;
             var color = Color.SeaShell * TransitionAlpha;

             int health = 10 - hurtCount;
             string s = "          Health: " + health;
             spriteBatch.DrawString(font, s,
                 new Vector2(580, 630), color, 0, origin,
                 2.0f, SpriteEffects.None, 0);

             spriteBatch.End();*/

            int health = 10 - hurtCount;

            /*spriteBatch.Begin();
            var origin = new Vector2(199, 99);
            var font = ScreenManager.MenuFont;
            var color = Color.SeaShell * TransitionAlpha;

            if (health == 10 && !_hero.firstTry)
            {
                string s = "          Restarting... ";
                spriteBatch.DrawString(font, s, new Vector2(580, 630), color, 0, origin, 2.0f, SpriteEffects.None, 0);
              

                //TimeSpan ts = TimeSpan.FromSeconds(2);
               // if (ts == gameTime.ElapsedGameTime)
               // {
               //     s = "";
              //      spriteBatch.DrawString(font, s, new Vector2(580, 630), color, 0, origin, 2.0f, SpriteEffects.None, 0);
               // }
            }
            else
            {
                string s = "";
                spriteBatch.DrawString(font, s, new Vector2(580, 630), color, 0, origin, 2.0f, SpriteEffects.None, 0);
            }
            spriteBatch.End();*/

            switch (health)
            {
                case 10:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw(); heartHealth7.Draw(); heartHealth8.Draw(); heartHealth9.Draw(); heartHealth10.Draw();
                    break;
                case 9:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw(); heartHealth7.Draw(); heartHealth8.Draw(); heartHealth9.Draw();
                    break;
                case 8:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw(); heartHealth7.Draw(); heartHealth8.Draw();
                    break;
                case 7:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw(); heartHealth7.Draw();
                    break;
                case 6:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw();
                    break;
                case 5:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    break;
                case 4:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw();
                    break;
                case 3:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw();
                    break;
                case 2:
                    heartHealth1.Draw(); heartHealth2.Draw();
                    break;
                case 1:
                    heartHealth1.Draw();
                    break;
                case 0:
                    heartHealth1.Draw(); heartHealth2.Draw(); heartHealth3.Draw(); heartHealth4.Draw(); heartHealth5.Draw();
                    heartHealth6.Draw(); heartHealth7.Draw(); heartHealth8.Draw(); heartHealth9.Draw(); heartHealth10.Draw();
                    break;
            }
           



            // If the game is transitioning on or off, fade it out to black.
            /*if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }*/
        }
    }
}
