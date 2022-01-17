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
using System.Collections.Generic;
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

        private Vector2 _catPosition;
        private AnimatedSprite _cat;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        private Pause _pause;
        private string animation;
        private int _vitessePerso;
        private OrthographicCamera _camera;

        private Texture2D _pauseBackground;

        public const int LARGEUR_MAP = 32;
        public const int HAUTEUR_MAP = 32;

        private TiledMapTileLayer tpPoints;
        private Button _pauseButton;
        private Button _resumeButton;
        private Button _quitButton;

        private List<Componant> _gameComponant;

        public Marche(GameManager game) : base(game)
        {
            _gameManager = game;

        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here

            _mcPosition = _gameManager._goToPos;
            _catPosition = new Vector2(20, 20);
            animation = "idle";
            _vitessePerso = 100;            
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(_gameManager.Window, GraphicsDevice, 500, 400);
            _camera = new OrthographicCamera(viewportadapter);
            _pss = new PositionSwitchScenecs();
            mouvement = new Mouvement();
            _pause = new Pause();

        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _pauseBackground = Content.Load<Texture2D>("UI/pauseBackground");
            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            SpriteSheet spriteCat = Content.Load<SpriteSheet>("Sprites/cat.sf", new JsonContentLoader());
            _mc = new AnimatedSprite(spriteSheet);
            _cat = new AnimatedSprite(spriteCat);
            _tiledMap = Content.Load<TiledMap>("marche");
            tpPoints = _tiledMap.GetLayer<TiledMapTileLayer>("Tp_Points");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            // Bouton Pause
            _pauseButton = new Button(Content.Load<Texture2D>("UI/pauseButton"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(50, 50),
                Text = "",
            };
            _pauseButton.Click += PauseButton_Click;
            _resumeButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 330),
                Text = "Reprendre",
            };
            _resumeButton.Click += PlayButton_Click;

            // Bouton QUitter
            _quitButton = new Button(Content.Load<Texture2D>("UI/Button"), Content.Load<SpriteFont>("Fonts/defaultFont"))
            {
                Position = new Vector2(390, 570),
                Text = "Quitter",
            };
            _quitButton.Click += QuitButton_Click;

            _gameComponant = new List<Componant>()
            {
                _resumeButton,
                _quitButton,
            };
        }

        public override void Update(GameTime gameTime)
        {          
            // TODO: Add your update logic here
            if(_pause._isPaused)
            {
                foreach (var component in _gameComponant)
                    component.Update(gameTime);
            }
            else
            {
                _tiledMapRenderer.Update(gameTime);
                float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                float walkSpeed = deltaSeconds * _vitessePerso;
                mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, HAUTEUR_MAP, LARGEUR_MAP, _mc, "Obstacle");
                CheckTPPoints();
                _camera.LookAt(_mcPosition);
                _mc.Play(animation);
                _cat.Play(animation);
                _mc.Update(deltaSeconds);
                _cat.Update(deltaSeconds);
            }
            _pauseButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2((float)2, (float)2));
            _spriteBatch.Draw(_cat, new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, GraphicsDevice.Viewport.Height / 2 + 20), 0, new Vector2((float)2, (float)2));
            _spriteBatch.End();
            _tiledMapRenderer.Draw(12, _camera.GetViewMatrix());
            if (_pause._isPaused)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(_pauseBackground, Vector2.Zero, Color.White);
                _pauseButton.Draw(gameTime, _spriteBatch);
                foreach (var component in _gameComponant)
                    component.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
            }
            _spriteBatch.Begin();
            _pauseButton.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
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



        // Boutons

        private void PauseButton_Click(object sender, System.EventArgs e)
        {
            if(_pause._isPaused)
                _pause._isPaused = false;
            else
                _pause._isPaused = true;
        }

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            _pause._isPaused = false;
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            _gameManager.Exit();
        }

    }
}
