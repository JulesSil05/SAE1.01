using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System.Threading;

namespace deplacement
{
    public class MapTallHouse : GameScreen
    {
        private Game1 _myGame; // pour récupérer le jeu en cours

        public MapTallHouse(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("tall_house");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("contraintes");
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            if (_myGame._persoPosition.Y >= 220)
            {
                _myGame.LoadScreenMap();
                _myGame._persoPosition = new Vector2(55, 466);
            }
            ushort x = (ushort)(_myGame._persoPosition.X / _myGame._tiledMap.TileWidth);
            ushort y = (ushort)(_myGame._persoPosition.Y / _myGame._tiledMap.TileHeight);
            
            if ((_myGame.mapLayer.GetTile(x, y).GlobalIdentifier) == 1526)
            {
                _myGame.LoadScreenBedroom();
                _myGame._persoPosition = new Vector2(57, 28);
            }
            
        }
        
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = _myGame._camera.GetViewMatrix();

            _myGame._spriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame._spriteBatch.Draw(_myGame._perso, _myGame._persoPosition);
            _myGame._tiledMapRenderer.Draw(_myGame._camera.GetViewMatrix());
            _myGame._spriteBatch.End();

        }
    }
}
