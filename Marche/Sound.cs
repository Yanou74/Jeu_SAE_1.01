using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace Marche
{
    public class Sound
    {
    
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<SoundEffect> soundEffects;
        private float volume;


        public Sound()
        {
            soundEffects = new List<SoundEffect>();
        }


        public protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Ville1
            soundEffects.Add(Content.Load<SoundEffect>("022815townbgm"));
            //Ferme
            soundEffects.Add(Content.Load<SoundEffect>("nature sketch"));
            //Desert2
            soundEffects.Add(Content.Load<SoundEffect>("Desert-Biome-Sketch"));
            //Maison
            soundEffects.Add(Content.Load<SoundEffect>("Home"));



            //soundEffects[1].Play();


            var instance = soundEffects[0].CreateInstance();
            instance.IsLooped = true;
            instance.Play();

        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                
                soundEffects[0].CreateInstance().Play();

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                soundEffects[1].CreateInstance().Play();

            if (Keyboard.GetState().IsKeyDown(Keys.E))
                soundEffects[2].CreateInstance().Play();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                soundEffects[3].CreateInstance().Play();




            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (SoundEffect.MasterVolume == 0.0f)
                    SoundEffect.MasterVolume = 1.0f;
                else
                    SoundEffect.MasterVolume = 0.0f;
            }






                // TODO: Add your update logic here

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}










    }
}
