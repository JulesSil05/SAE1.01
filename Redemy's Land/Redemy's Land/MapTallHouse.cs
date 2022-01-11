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

namespace RedemysLand
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
                _myGame._persoPosition = new Vector2(440, 694);
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

            _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame.SpriteBatch.Draw(_myGame._perso, _myGame._persoPosition);
            _myGame._tiledMapRenderer.Draw(_myGame._camera.GetViewMatrix());
            _myGame.SpriteBatch.End();
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur1, _myGame._positionCoeur1, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur2, _myGame._positionCoeur2, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur3, _myGame._positionCoeur3, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur4, _myGame._positionCoeur4, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur5, _myGame._positionCoeur5, Color.White);

            _myGame.SpriteBatch.Draw(_myGame._textureCase1, _myGame._positionCase1, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase2, _myGame._positionCase2, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase3, _myGame._positionCase3, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase4, _myGame._positionCase4, Color.White);

            _myGame.SpriteBatch.DrawString(_myGame._police, "" + Math.Round(_myGame._chronoGame) + "", _myGame._positionTexte, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureEcranFin, _myGame._positionEcranFin, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureExitGameButton, _myGame._positionExitGameButton, Color.White);
            _myGame.SpriteBatch.End();

        }
    }
}
