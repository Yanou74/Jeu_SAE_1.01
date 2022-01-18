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
    class ItemList : GameScreen
    {
        public List<Item> itemList = new List<Item>();
        private GameManager _gameManager;
        private GraphicsDeviceManager _graphics;
        private AnimatedSprite _fruit_legumes;
        private SpriteBatch _spriteBatch;

        public ItemList(GameManager game) : base(game)
        {
            _gameManager = game;
            _graphics = _gameManager._graphics;
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("items/fruit_legumes_fixe.sf", new JsonContentLoader());
            _fruit_legumes = new AnimatedSprite(spriteSheet);

        }
        public override void Update(GameTime gameTime)
        {
            AddItem(gameTime, _fruit_legumes, "carottes_item", "0101CARROTE", 2,1, itemList);
            AddItem(gameTime, _fruit_legumes, "patates_item", "0101PATATE", 1, 1, itemList);
            AddItem(gameTime, _fruit_legumes, "navet_item", "0101NAVET", 2, 1, itemList);

        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            foreach(Item item in itemList)
         {
             _spriteBatch.Draw(item.AnimatedSprite, new Vector2(50, 50), 0, new Vector2(2f, 2f));
         }
            _spriteBatch.End();
        }


        public List<Item> AddItem(GameTime gameTime, AnimatedSprite _sprite, string animation, string id, int level, int etat, List<Item> list)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _sprite.Play(animation);
            _sprite.Update(deltaSeconds);
            Item item = new Item(id, level, etat, _sprite);
            list.Add(item);
            return list;
        }
    }
}
