using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace Marche
{
    public class GameManager : Game
    {
        public GraphicsDeviceManager _graphics;
        private Pause _pause;
        private bool _singleClick;
        private Sound _sfx;
        private bool alreadyplay = false;
        public SpriteBatch SpriteBatch { get; set; }
        public readonly ScreenManager _screenManager;



        // Position a mettre
        public Vector2 _goToPos;
        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            _sfx = new Sound();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            
            LoadPaysage();
            // TODO: Add your initialization logic here
            // Position par Défaut
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            _goToPos = new Vector2(512, 928);
            _singleClick = true;
            
            base.Initialize();

        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Audio

            //Ville1
            _sfx.soundEffects.Add(Content.Load<SoundEffect>("SFX/Harbor"));
            //Ferme
           // _sfx.soundEffects.Add(Content.Load<SoundEffect>("nature sketch"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (SoundEffect.MasterVolume == 0.0f)
                    SoundEffect.MasterVolume = 1.0f;
                else
                    SoundEffect.MasterVolume = 0.0f;
            }

            if(alreadyplay == false)
            {
                var instance = _sfx.soundEffects[0].CreateInstance();
                instance.IsLooped = true;
                instance.Play();
                alreadyplay = true;
            }
                
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void LoadPaysage()
        {
            _screenManager.LoadScreen(new MainMenu(this));
        }

        private void ToggleFS()
        {     
            if (_graphics.IsFullScreen)
                _graphics.IsFullScreen = false;
            else
                _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            
        }
    }
}
