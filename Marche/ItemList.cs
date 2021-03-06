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
        public AnimatedSprite _fruit_legumes;
        public AnimatedSprite _fruit_legumes2;
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
            SpriteSheet spriteSheet2 = Content.Load<SpriteSheet>("items/fruit_legumes.sf", new JsonContentLoader());
            _fruit_legumes = new AnimatedSprite(spriteSheet);
            _fruit_legumes2 = new AnimatedSprite(spriteSheet2);

        }
        public override void Update(GameTime gameTime)
        {
            /*ITEM D'INVENTAIRE */
             itemList = AddItem(gameTime, _fruit_legumes, "carottes_item", "0101CAROTTE", 2,1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "patates_item", "0101PATATE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "navet_item", "0101NAVET", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "oignon_item", "0101OIGNON", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "poivrons_item", "0101POIVRON", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "pastèque_item", "0101PASTEQUE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "citrouille_item", "0101CITROUILLE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "mais_item", "0101MAIS", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "salade_item", "0101SALADE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "poireaux_item", "0101POIREAUX", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "aubergine_item", "0101AUBERGINE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "mure_item", "0102MURE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "fraise_item", "0102FRAISE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "framboise_item", "0102FRAMBOISE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "tomate_item", "0101TOMATE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "haricots_item", "0101HARICOT", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes, "raisin_item", "0102RAISIN", 2, 1, itemList);

            /*ITEM DE MAP*/
             itemList = AddItem(gameTime, _fruit_legumes2, "carotte", "1101CAROTTE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "patate", "1101PATATE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "navet", "1101NAVET", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "oignon", "1101OIGNON", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "poivron", "1101POIVRON", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "pastèque", "1101PASTEQUE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "citrouille", "1101CITROUILLE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "mais", "1101MAIS", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "salade", "1101SALADE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "poireaux", "1101POIREAUX", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "aubergine", "1101AUBERGINE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "mure", "1102MURE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "fraise", "1102FRAISE", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "framboise", "1102FRAMBOISE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "tomate", "1101TOMATE", 2, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "haricot", "1101HARICOT", 1, 1, itemList);
             itemList = AddItem(gameTime, _fruit_legumes2, "raisin", "1102RAISIN", 2, 1, itemList);

        }
        public override void Draw(GameTime gameTime)
        {
        }


        public List<Item> AddItem(GameTime gameTime, AnimatedSprite _sprite, string animation, string id, int level, int etat, List<Item> list)
        {
            if (_sprite == null)
                return list;
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _sprite.Play(animation);
            _sprite.Update(deltaSeconds);
            Item item = new Item(id, level, etat, _sprite);
            list.Add(item);
            return list;
        }

        public List<Item> ReturnList()
        {
            return itemList;
        }
    }
}
