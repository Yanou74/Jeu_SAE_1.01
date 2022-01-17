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
        private SpriteBatch _spriteBatch;
        public List<SoundEffect> soundEffects;
        private float volume;


        public Sound()
        {
            soundEffects = new List<SoundEffect>();
        }
    }
}

