﻿using Microsoft.Xna.Framework;
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

namespace Marche
{
    class Paysage : GameScreen
    {
        private GameManager _gameManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _mcPosition;
        private AnimatedSprite _mc;
        private string animation;
        private int _vitessePerso;
        private Mouvement mouvement;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;
        private MouseState mouseState;

        public Paysage(GameManager game) : base(game)
        {
            _gameManager = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _mcPosition = new Vector2(512, 2880);
            animation = "idle";
            _vitessePerso = 100;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(_gameManager.Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            mouvement = new Mouvement();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            _tiledMap = Content.Load<TiledMap>("paysage/map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _mc = new AnimatedSprite(spriteSheet);

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _gameManager.Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("arbre_tronc");
            mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, 600, 800, _mc, "arbre_tronc","montagne", "montagne_etage_2", "cascade", "maison_joueur", "dehors_maison_joueur", "parcelle");
            _camera.LookAt(_mcPosition);
            _mc.Play(animation);
            _mc.Update(deltaSeconds);

            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                _gameManager._screenManager.LoadScreen(new Marche(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2((float)1, (float)1));
            _spriteBatch.End();
            _tiledMapRenderer.Draw(7, _camera.GetViewMatrix());
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (movementDirection.Y >= 100)
            {
                movementDirection -= Vector2.Zero;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }

            // Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize();
            }

            


            return movementDirection;
        }

    }
}
