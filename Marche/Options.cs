using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;

namespace Marche
{
    class Options : GameScreen
    {
        private GameManager _gameManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Texture2D _overlay;
        private Texture2D _checkBox;

        private List<Componant> _gameComponant;

        private Button _returnButton;
        private Button _fullscreenButton;
        private Button _soundButton;
        private SpriteFont _textFS;

        public Options(GameManager game) : base(game)
        {
            _gameManager = game;
            _graphics = _gameManager._graphics;
        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here

           
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _overlay = Content.Load<Texture2D>("UI/OptionOverlay");
            _checkBox = Content.Load<Texture2D>("UI/checkbox/UnCheck");
            // Bouton Play
            _returnButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 600),
                Text = "Retour",
            };
            _returnButton.Click += OptionButton_Click;

            _fullscreenButton = new Button(_checkBox, Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(700, 400),
            };
            _fullscreenButton.Click += FullScreen_Click;

            _soundButton = new Button(_checkBox, Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(700, 300),
            };
            _soundButton.Click += SoundButton_Click;

            _gameComponant = new List<Componant>()
            {
                _returnButton,
                _fullscreenButton,
                _soundButton,
            };

            _textFS = Content.Load<SpriteFont>("Fonts/defaultFont");

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameManager.Exit();

            foreach (var component in _gameComponant)
                component.Update(gameTime);

            if (_graphics.IsFullScreen)
            {
                _fullscreenButton.Text = "X";
            }
            else
            {
                _fullscreenButton.Text = "";
            }

            if (SoundEffect.MasterVolume == 1.0f)
                _soundButton.Text = "X";
            else
                _soundButton.Text = "";
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_overlay, new Vector2(340, 220), Color.White);
            spriteBatch.DrawString(_textFS, "FullScreen :", new Vector2(400, 410), new Color(148, 140, 99));
            spriteBatch.DrawString(_textFS, "Son :", new Vector2(400, 310), new Color(148, 140, 99));
            foreach (var component in _gameComponant)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        private void OptionButton_Click(object sender, System.EventArgs e)
        {
            _gameManager._screenManager.LoadScreen(new MainMenu(_gameManager));
        }

        private void FullScreen_Click(object sender, System.EventArgs e)
        {
                if (_graphics.IsFullScreen)
                {
                    _graphics.IsFullScreen = false;
                }
                else
                {
                    _graphics.IsFullScreen = true;
                }
            _graphics.ApplyChanges();
        }

        private void SoundButton_Click(object sender, System.EventArgs e)
        {
            if (SoundEffect.MasterVolume == 0.0f)
                SoundEffect.MasterVolume = 1.0f;
            else
                SoundEffect.MasterVolume = 0.0f;
        }
    }
}
