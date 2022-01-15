using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace Marche
{
    class MainMenu : GameScreen
    {
        private GameManager _gameManager;
        private SpriteBatch spriteBatch;
        private Texture2D _background;
        private Texture2D _titleGame;

        private List<Componant> _gameComponant;

        //Buttons 
        public Button _playButton;
        private Button _quitButton;

        public MainMenu(GameManager game) : base(game)
        {
            _gameManager = game;

        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("MainMenuElements/background");
            _titleGame = Content.Load<Texture2D>("MainMenuElements/titleGame");

            _playButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 328),
                Text = "Play",
            };
            _playButton.Click += PlayButton_Click;


            _quitButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 480),
                Text = "Quitter",
            };
            _quitButton.Click += QuitButton_Click;

            _gameComponant = new List<Componant>()
            {
                _playButton,
                _quitButton,
            };

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameManager.Exit();

            foreach (var component in _gameComponant)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            spriteBatch.Draw(_titleGame, new Vector2(320, 30), Color.White);
            foreach (var component in _gameComponant)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            _gameManager._screenManager.LoadScreen(new Paysage(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            _gameManager.Exit();
        }


    }
}
