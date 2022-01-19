using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using MonoGame.Extended.Screens;

namespace RedemysLand
{
    public class Grotte : GameScreen
    {
        private Texture2D _textureEmeraude;
        private Vector2 _positionEmeraude;
        
        private Game1 _myGame; 

        public Grotte(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            _positionEmeraude = new Vector2(126,742);
            
            _textureEmeraude = Content.Load<Texture2D>("emeraude");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("grotte");            
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("contraintes");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _myGame._vitesseIA = 0;
            if (_myGame._persoPosition.Y >= 1100)
            {
                _myGame.LoadScreenMap();
                _myGame._persoPosition = new Vector2(1112, 292);
            }
            if (_myGame._argentRecu == true)
            {
                _myGame._textureCase4 = Content.Load<Texture2D>("case4");
                _positionEmeraude = new Vector2(-10000, -10000);
            }
            else if (_myGame._persoPosition.X >= 114 && _myGame._persoPosition.X <= 128 && _myGame._persoPosition.Y >= 731 && _myGame._persoPosition.Y <= 743 || _myGame._emeraudeRamassee == true)
            {
                _myGame._emeraudeRamassee = true;
                _positionEmeraude = new Vector2(-10000, -10000);
                _myGame._textureCase4 = Content.Load<Texture2D>("case4ramasse");
            }
            
            else
                _positionEmeraude = new Vector2(115, 740);

            ushort x = (ushort)(_myGame._persoPosition.X / _myGame._tiledMap.TileWidth);
            ushort y = (ushort)(_myGame._persoPosition.Y / _myGame._tiledMap.TileHeight);

            if ((_myGame.mapLayer.GetTile(x, y).GlobalIdentifier) == 1779 )
            {
                _myGame.LoadScreenFondGrotte();
                _myGame._persoPosition = new Vector2(130, 166);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = _myGame._camera.GetViewMatrix();

            _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame._tiledMapRenderer.Draw(_myGame._camera.GetViewMatrix());
            _myGame.SpriteBatch.Draw(_textureEmeraude, _positionEmeraude, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._perso, _myGame._persoPosition);
            _myGame.SpriteBatch.Draw(_myGame._bird, _myGame._BirdPosition);
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
            _myGame.SpriteBatch.Draw(_myGame._textureCase5, _myGame._positionCase5, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase6, _myGame._positionCase6, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureMoneyBag, _myGame._positionMoneyBag, Color.White);
            _myGame.SpriteBatch.DrawString(_myGame._police, "" + Math.Round(_myGame._chronoGame) + "", _myGame._positionTexte, Color.White);
            _myGame.SpriteBatch.DrawString(_myGame._policePorteMonnaie, "" + _myGame._valeurPorteMonnaie + "", _myGame._positionPorteMonnaie, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureEcranFin, _myGame._positionEcranFin, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureExitGameButton, _myGame._positionExitGameButton, Color.White);
            _myGame.SpriteBatch.End();

        }
    }
}
