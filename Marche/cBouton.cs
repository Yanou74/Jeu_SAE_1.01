using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Marche
{
    class cBouton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color couleur = new Color(255, 255, 255, 255);

        public Vector2 size;

        public cBouton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 3, graphics.Viewport.Height / 2);

        }

        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);


            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (couleur.A == 255) down = false;
                if (couleur.A == 0) down = true;
                if (down) couleur.A += 3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (couleur.A < 255)
            {
                couleur.A += 3;
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, couleur);

        }
    }
}