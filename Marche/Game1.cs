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
using System;

namespace Marche
{
    public class Game1 : Game
    {

       
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Mouvement mouvement;
        private Vector2 _mcPosition;
        private AnimatedSprite _mc;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        private string animation;
        private int _vitessePerso;

        public static TiledMapTileLayer _collisionsLayer; // collisions
        private OrthographicCamera _camera;

        public const int LARGEUR_MAP = 32;
        public const int HAUTEUR_MAP = 32;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // _graphics.IsFullScreen = true;
            _mcPosition = new Vector2(400, 400);
            animation = "idle";
            _vitessePerso = 100;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("mc.sf", new JsonContentLoader());
            _mc = new AnimatedSprite(spriteSheet);
            _tiledMap = Content.Load<TiledMap>("marche");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _collisionsLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;

            Mouvement.Move(ref _mcPosition, ref animation, _tiledMap, walkSpeed, HAUTEUR_MAP, LARGEUR_MAP, _mc);
            _camera.LookAt(_mcPosition);
            _mc.Play(animation);
            _mc.Update(deltaSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_mc, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), 0, new Vector2((float)1.5, (float)1.5));
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }

    }
}
