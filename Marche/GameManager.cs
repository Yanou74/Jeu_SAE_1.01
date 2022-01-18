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
        // Dialog Box
        public SpriteFont DialogFont { get; private set; }
        public KeyboardState KeyState { get; private set; }
        public KeyboardState PreviousKeyState { get; private set; }
        public Vector2 CenterScreen
            => new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f, _graphics.GraphicsDevice.Viewport.Height / 2f);

        public DialogBox _dialogBox;

        ///////////////////////////////////////

        public Cat_IA _cia;
        public GraphicsDeviceManager _graphics;
        private Sound _sfx;
        private bool alreadyplay = false;
        public SpriteBatch SpriteBatch { get; set; }
        public readonly ScreenManager _screenManager;
        public int _goldCount;



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
            _goldCount = 0;
            _cia = new Cat_IA();

            base.Initialize();

        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Audio
            _sfx.soundEffects.Add(Content.Load<SoundEffect>("SFX/Harbor"));
            // Dialogfont
            DialogFont = Content.Load<SpriteFont>("Fonts/dialogFont");
            _dialogBox = new DialogBox
            {
                Text = "Bienvenue dans votre Ferme !\n" +
                      "Pour vous deplacer, utilisez les touches directionnelles.\n" +
                      "Le but du jeu est d'ameliorer votre ferme en vendant vos productions\n" +
                      "A l'EST, vous pourrez acceder au marche afin de vendre vos productions.\n" +
                      "Une derniere chose, nous avons mis un chat a votre disposission afin de vous aider au long de votre aventure...\n" +
                      "MiAOU MIAOU (Salut, je suis ton bras droit !)\n" +
                      "Si vous etes bloque quelque part ou vous ne savez pas quoi faire, votre chat pourra alors vous conseiller sur les choix a faire tout au long de l'aventure. Pour appeler votre chat, appuyez sur la touche Z." +
                      "Bonne cultivation !"
            };

            // Initialize the dialog box (this also calls the Show() method)
            _dialogBox.Initialize();
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

            Cheats();
            if(alreadyplay == false)
            {
                var instance = _sfx.soundEffects[0].CreateInstance();
                instance.IsLooped = true;
                instance.Play();
                alreadyplay = true;
            }

            // Dialogbox
            _dialogBox.Update();

            // Debug key to show opening a new dialog box on demand
            if (Program.Game.KeyState.IsKeyDown(Keys.Z))
            {
                if (!_dialogBox.Active)
                {
                    _dialogBox = new DialogBox { Text = _cia.Request(_goldCount)};
                    _dialogBox.Initialize();
                }
            }

            // Update input states
            PreviousKeyState = KeyState;
            KeyState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {


            // TODO: Add your drawing code her
            base.Draw(gameTime);
        }

        private void LoadPaysage()
        {
            _screenManager.LoadScreen(new MainMenu(this));
        }

        private void Cheats()
        {
            if(_goldCount < 9999)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                    _goldCount += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                    _goldCount += 10;
            }
            // Ajout de gold
            
        }
    }
}
