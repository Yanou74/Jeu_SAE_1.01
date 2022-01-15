using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private SpriteBatch spriteBatch;
        private Texture2D _overlay;

        private List<Componant> _gameComponant;

        private Button _returnButton;

        public Options(GameManager game) : base(game)
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
            _overlay = Content.Load<Texture2D>("UI/OptionOverlay");

            // Bouton Play
            _returnButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 600),
                Text = "Retour",
            };
            _returnButton.Click += OptionButton_Click;

            _gameComponant = new List<Componant>()
            {
                _returnButton,
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
            spriteBatch.Begin();
            spriteBatch.Draw(_overlay, new Vector2(340, 220), Color.White);
            foreach (var component in _gameComponant)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        private void OptionButton_Click(object sender, System.EventArgs e)
        {
            _gameManager._screenManager.LoadScreen(new MainMenu(_gameManager));
        }
    }
}
