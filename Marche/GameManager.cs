using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace Marche
{
    public class GameManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private Pause _pause;
        private bool _singleClick;
        public SpriteBatch SpriteBatch { get; set; }
        public readonly ScreenManager _screenManager;


        // Position a mettre
        public Vector2 _goToPos;
        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            
            LoadPaysage();
            // TODO: Add your initialization logic here
            // Position par Défaut
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.ApplyChanges();
            _goToPos = new Vector2(544, 2944);
            _singleClick = true;
             _pause = new Pause();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && _singleClick == false)
            {
                _pause.IsPaused();
                _singleClick = true;
            }
            else
            {
                _singleClick = false;
            }
                


            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void LoadMarche()
        {
            _screenManager.LoadScreen(new Marche(this));
        }
        private void LoadPaysage()
        {
            _screenManager.LoadScreen(new Paysage(this));
        }
        private void LoadHousePlayer()
        {
            _screenManager.LoadScreen(new House(this));
        }

        private void SpeedCheat()
        {

        }
    }
}
