using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Marche
{
    public abstract class Componant
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteFont);

        public abstract void Update(GameTime gameTime);
    }
}
