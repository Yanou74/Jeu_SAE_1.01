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
using System;
namespace Marche
{
    class Paysage : GameScreen
    {
        private PositionSwitchScenecs _pss;
        private GameManager _gameManager;
        private SpriteBatch _spriteBatch;
        private Vector2 _mcPosition;
        private AnimatedSprite _mc;
        private string animation;
        private int _vitessePerso;
        private Mouvement mouvement;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _tpPoints;
        private OrthographicCamera _camera;
        private MouseState mouseState;

        public Paysage(GameManager game) : base(game)
        {
            _gameManager = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _mcPosition = _gameManager._goToPos;
            animation = "idle";
            _vitessePerso = 100;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(_gameManager.Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            _pss = new PositionSwitchScenecs();
            mouvement = new Mouvement();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            _tiledMap = Content.Load<TiledMap>("paysage/map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _tpPoints = _tiledMap.GetLayer<TiledMapTileLayer>("tp_points");
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
            mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, 600, 800, _mc, "bord_eau", "bord_montagnes", "parcelle","dehors_maison_joueur", "maison_joueur_derriere", "arbre_tronc");
            _camera.LookAt(_mcPosition);
            CheckTPPoints();
            _mc.Play(animation);
            _mc.Update(deltaSeconds);

            mouseState = Mouse.GetState();
            

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2((float)1, (float)1));
            _spriteBatch.End();
            _tiledMapRenderer.Draw(11, _camera.GetViewMatrix());
            _tiledMapRenderer.Draw(15, _camera.GetViewMatrix());
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
        private void CheckTPPoints()
        {
            ushort tx = (ushort)(_mcPosition.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_mcPosition.Y / _tiledMap.TileHeight + 1);
            
            if (_tpPoints.GetTile(tx, ty).GlobalIdentifier == 3401)
            {
                _gameManager._goToPos = _pss.SwitchScene(4);
                _gameManager._screenManager.LoadScreen(new Marche(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));
                
            } else if (_tpPoints.GetTile(tx, ty).GlobalIdentifier == 3402)
            {
                _gameManager._goToPos = _pss.SwitchScene(5);
                _gameManager._screenManager.LoadScreen(new House(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));

            }

        }

    }
}
