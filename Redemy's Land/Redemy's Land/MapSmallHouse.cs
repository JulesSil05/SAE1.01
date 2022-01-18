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
    public class MapSmallHouse : GameScreen
    {
        private Game1 _myGame; // pour récupérer le jeu en cours
        private Texture2D _texturePlante;
        private Vector2 _positionPlante = new Vector2 (178, 60);

        public MapSmallHouse(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {
            _myGame._vitesseIA = 0;
            base.Initialize();
        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("small_house");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("contraintes");
            _texturePlante = Content.Load<Texture2D>("plante");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (_myGame._persoPosition.Y >= 170)
            {
                _myGame.LoadScreenMap();
                _myGame._persoPosition = new Vector2(695, 1185);
            }


            if (_myGame._planteRamassee == true)
            {
                _myGame._textureCase2 = Content.Load<Texture2D>("case2ramasse");
                _positionPlante = new Vector2(-10000, -10000);
            }
            else if (_myGame._persoPosition.X >= 183 && _myGame._persoPosition.X <= 193 && _myGame._persoPosition.Y >= 55 && _myGame._persoPosition.Y <= 65 || _myGame._planteRamassee == true)
            {
                _myGame._planteRamassee = true;
                _positionPlante = new Vector2(-10000, -10000);
                _myGame._textureCase2 = Content.Load<Texture2D>("case2ramasse");
            }
            else
            {
                _positionPlante = new Vector2(178, 60);
                _myGame._textureCase2 = Content.Load<Texture2D>("case2");
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = _myGame._camera.GetViewMatrix();

            _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame.SpriteBatch.Draw(_texturePlante, _positionPlante, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._perso, _myGame._persoPosition);
            _myGame.SpriteBatch.Draw(_myGame._bird, _myGame._BirdPosition);
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
