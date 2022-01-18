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
    class Paysage : GameScreen
    {
        private PositionSwitchScenecs _pss;
        private GameManager _gameManager;
        private SpriteBatch _spriteBatch;
        private Vector2 _mcPosition;        
        private AnimatedSprite _mc;

        private Pause _pause;
        private string animation;
        private int _vitessePerso;
        private Mouvement mouvement;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _tpPoints;
        private OrthographicCamera _camera;
        private MouseState mouseState;

        private Texture2D _pauseBackground;
        private Texture2D _goldCoin;
        private SpriteFont _textGold;
        private Button _pauseButton;
        private Button _resumeButton;
        private Button _quitButton;

        private List<Componant> _gameComponant;

        public Paysage(GameManager game) : base(game)
        {
            _gameManager = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _mcPosition = _gameManager._goToPos;
            animation = "idle";
            _vitessePerso = 300;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(_gameManager.Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            _pss = new PositionSwitchScenecs();
            mouvement = new Mouvement();
            _pause = new Pause();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            _pauseBackground = Content.Load<Texture2D>("UI/pauseBackground");
            _goldCoin = Content.Load<Texture2D>("UI/Gold");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            SpriteSheet spriteSheetCat = Content.Load<SpriteSheet>("Sprites/cat.sf", new JsonContentLoader());
            _tiledMap = Content.Load<TiledMap>("paysage/map1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _tpPoints = _tiledMap.GetLayer<TiledMapTileLayer>("tp_points");
            _mc = new AnimatedSprite(spriteSheet);
            _textGold = Content.Load<SpriteFont>("Fonts/defaultFont");

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
            if (_pause._isPaused)
            {
                foreach (var component in _gameComponant)
                    component.Update(gameTime);
            }
            else
            {
                // TODO: Add your update logic here
                _tiledMapRenderer.Update(gameTime);
                float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                float walkSpeed = deltaSeconds * _vitessePerso;
                mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, 600, 800, _mc, "bord_eau", "bord_montagnes", "parcelle", "dehors_maison_joueur", "maison_joueur_derriere", "arbre_tronc");
                _camera.LookAt(_mcPosition);
                CheckTPPoints();
                _mc.Play(animation);
                _mc.Update(deltaSeconds);
                mouseState = Mouse.GetState();
            }

            _pauseButton.Update(gameTime);



        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(47,129,54));

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2(1.5f, 1.5f));
            _spriteBatch.End();
            _tiledMapRenderer.Draw(11, _camera.GetViewMatrix());
            _tiledMapRenderer.Draw(15, _camera.GetViewMatrix());
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
            _spriteBatch.Draw(_goldCoin, new Vector2(1200, 50), Color.White);
            _spriteBatch.DrawString(_textGold, _gameManager._goldCount.ToString(), new Vector2(1100, 70), Color.LightGoldenrodYellow);
            _gameManager._dialogBox.Draw(_spriteBatch);
            _spriteBatch.End();
        }


        private void CheckTPPoints()
        {
            ushort tx = (ushort)(_mcPosition.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_mcPosition.Y / _tiledMap.TileHeight + 1);
            
            if (_tpPoints.GetTile(tx, ty).GlobalIdentifier == 3145)
            {
                _gameManager._goToPos = _pss.SwitchScene(4);
                _gameManager._screenManager.LoadScreen(new Marche(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));
                
            } else if (_tpPoints.GetTile(tx, ty).GlobalIdentifier == 3146)
            {
                _gameManager._goToPos = _pss.SwitchScene(5);
                _gameManager._screenManager.LoadScreen(new House(_gameManager), new FadeTransition(GraphicsDevice, Color.Black));

            }

        }

        private void PauseButton_Click(object sender, System.EventArgs e)
        {
            if (_pause._isPaused)
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
