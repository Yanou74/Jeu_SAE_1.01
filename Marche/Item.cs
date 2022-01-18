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
using System;
using System.Collections.Generic;

namespace Marche
{
    public class Item
    {
        private string id;
        private int level;
        private int etat;
        private AnimatedSprite animatedSprite;

        public Item(string id, int level, int etat, AnimatedSprite animatedSprite)
        {
            this.id = id;
            this.level = level;
            this.etat = etat;
            this.animatedSprite = animatedSprite;
        }

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {

                this.id = value;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                this.level = value;
            }
        }

        public int Etat
        {
            get
            {
                return this.etat;
            }

            set
            {
                this.etat = value;
            }
        }

        public AnimatedSprite AnimatedSprite
        {
            get
            {
                return this.animatedSprite;
            }

            set
            {
                this.animatedSprite = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   this.id == item.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.id, this.level, this.etat, this.animatedSprite);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
