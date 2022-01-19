using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Collections.Generic;

namespace Marche
{
    class Inventaire : GameScreen
    {
        private GameManager _gameManager;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private Texture2D _inventaire;

        public Inventaire(GameManager game) : base(game)
        {
            _gameManager = game;
            _graphics = _gameManager._graphics;
        }
        public override void Initialize()
        {

        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _inventaire = Content.Load<Texture2D>("inventaire");
        }
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_inventaire, new Vector2(320, 550), Color.White);
            _spriteBatch.End();
        }
    }
}