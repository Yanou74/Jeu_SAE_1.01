using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;

namespace Marche
{
    public class Mouvement
    {

        public void Move(ref Vector2 posPerso, ref string animation, TiledMap _tiledMap, float walkSpeed, int hauteur, int largeur, AnimatedSprite _mc, params string[] _layersName)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(posPerso.X / _tiledMap.TileWidth - 0.5);
                ushort ty = (ushort)(posPerso.Y / _tiledMap.TileHeight + 1);
                animation = "walkWest";
                if (!IsCollision( tx, ty, _tiledMap, _layersName) && posPerso.X > 0 + _mc.TextureRegion.Width / 2)
                    posPerso.X -= walkSpeed;


            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(posPerso.X / _tiledMap.TileWidth + 0.5);
                ushort ty = (ushort)(posPerso.Y / _tiledMap.TileHeight + 1);
                animation = "walkEast";
                if (!IsCollision(tx, ty, _tiledMap, _layersName) && posPerso.X < _tiledMap.Width * largeur + _mc.TextureRegion.Width / 2)
                    posPerso.X += walkSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(posPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(posPerso.Y / _tiledMap.TileHeight + 0.5);
                animation = "walkNorth";
                if (!IsCollision(tx, ty, _tiledMap, _layersName) && posPerso.Y > 0 + _mc.TextureRegion.Height / 2)
                    posPerso.Y -= walkSpeed;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(posPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(posPerso.Y / _tiledMap.TileHeight + 1.5);
                animation = "walkSouth";
                if (!IsCollision(tx, ty, _tiledMap, _layersName) && posPerso.Y < _tiledMap.Height * hauteur + _mc.TextureRegion.Height / 2)
                    posPerso.Y += walkSpeed;

            }
            else
            {
                animation = "idle";
            }
    }

        public bool IsCollision(ushort x, ushort y, TiledMap _tiledMap, params string[] _layersName)
        {
            bool col = false;

            foreach (string _layer in _layersName)
            {
                TiledMapTileLayer _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>(_layer);
                TiledMapTile? tile;
                if (_mapLayer.TryGetTile(x, y, out tile) == false)
                    col = false;
                if (!tile.Value.IsBlank)
                    return true;
            }
            return col;
        }
    }
}
