using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

using System;

namespace Marche
{
    class Marche : GameScreen
    {
        public PositionSwitchScenecs _pss;
        private GameManager _gameManager;
        private SpriteBatch _spriteBatch;

        private Mouvement mouvement;
        private Vector2 _mcPosition;
        private AnimatedSprite _mc;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        private string animation;
        private int _vitessePerso;

        private OrthographicCamera _camera;

        public const int LARGEUR_MAP = 32;
        public const int HAUTEUR_MAP = 32;

        private TiledMapTileLayer tpPoints;

        public Marche(GameManager game) : base(game)
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
            var viewportadapter = new BoxingViewportAdapter(_gameManager.Window, GraphicsDevice, 500, 400);
            _camera = new OrthographicCamera(viewportadapter);
            _pss = new PositionSwitchScenecs();
            mouvement = new Mouvement();

        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            _mc = new AnimatedSprite(spriteSheet);
            _tiledMap = Content.Load<TiledMap>("marche");
            tpPoints = _tiledMap.GetLayer<TiledMapTileLayer>("Tp_Points");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;

            mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, HAUTEUR_MAP, LARGEUR_MAP, _mc, "Obstacle");
            CheckTPPoints();
            _camera.LookAt(_mcPosition);
            _mc.Play(animation);
            _mc.Update(deltaSeconds);

            
            
            
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2((float)2, (float)2));
            _spriteBatch.End();
            _tiledMapRenderer.Draw(12, _camera.GetViewMatrix());

        }

        public void CheckTPPoints()
        {
            ushort tx = (ushort)(_mcPosition.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_mcPosition.Y / _tiledMap.TileHeight + 1);
            
            if(tpPoints.GetTile(tx, ty).GlobalIdentifier == 7028)
            {
                _gameManager._goToPos = _pss.SwitchScene(1);
                _gameManager._screenManager.LoadScreen(new Paysage(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));
                
            }
                    
        }

    }
}
