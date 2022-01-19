using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using MonoGame.Extended.Screens;

namespace RedemysLand
{
    public class Map : GameScreen
    {
        private Game1 _myGame; 

        public Map(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            _myGame._tiledMap = Content.Load<TiledMap>("map");
            _myGame._tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _myGame._tiledMap);
            _myGame.mapLayer = _myGame._tiledMap.GetLayer<TiledMapTileLayer>("contraintes");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _myGame._vitesseIA = 40;

            if (_myGame._persoPosition.X >= _myGame._IAposition.X - 32 && _myGame._persoPosition.X <= _myGame._IAposition.X + 32 && _myGame._persoPosition.Y >= _myGame._IAposition.Y - 32 && _myGame._persoPosition.Y <= _myGame._IAposition.Y + 32)
            {
                _myGame._vitesseIA = 0;
                _myGame.animationIA = "faceIA";
                _myGame._positionDialogueZombie = new Vector2(_myGame._IAposition.X - 30, _myGame._IAposition.Y - 90);
                
            }

            else
            {
                _myGame._positionDialogueZombie = new Vector2(-10000, -10000);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = _myGame._camera.GetViewMatrix();

            _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame.SpriteBatch.Draw(_myGame._textureBoutonE, _myGame._positionBoutonE, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureBoutonConcocter, _myGame._positionBoutonConcocter, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureBoutonEchange, _myGame._positionBoutonEchange, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureChestKey, _myGame._positionChestKey, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoffre, _myGame._positionCoffre, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureChestKey, _myGame._positionChestKey, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._IA, _myGame._IAposition);
            _myGame.SpriteBatch.Draw(_myGame._perso, _myGame._persoPosition);
            _myGame.SpriteBatch.Draw(_myGame._bird, _myGame._BirdPosition);
            _myGame._tiledMapRenderer.Draw(_myGame._camera.GetViewMatrix());
            _myGame.SpriteBatch.Draw(_myGame._texturetjd4, _myGame._positiontjd4, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureDialoguePnj, _myGame._positionDialoguePnj, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureDialogueZombie, _myGame._positionDialogueZombie, Color.White);
            _myGame.SpriteBatch.End();
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_myGame._textureVictoire, _myGame._positionVictoire, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureEcranMauvais, _myGame._positionEcranMauvais, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur1, _myGame._positionCoeur1, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur2, _myGame._positionCoeur2, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur3, _myGame._positionCoeur3, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur4, _myGame._positionCoeur4, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCoeur5, _myGame._positionCoeur5, Color.White);

            _myGame.SpriteBatch.DrawString(_myGame._policePorteMonnaie, "" + _myGame._valeurPorteMonnaie + "", _myGame._positionPorteMonnaie, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase1, _myGame._positionCase1, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase2, _myGame._positionCase2, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase3, _myGame._positionCase3, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase4, _myGame._positionCase4, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase5, _myGame._positionCase5, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureCase6, _myGame._positionCase6, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureMoneyBag, _myGame._positionMoneyBag, Color.White);
            _myGame.SpriteBatch.DrawString(_myGame._police, "" + Math.Round(_myGame._chronoGame) + "", _myGame._positionTexte, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._texturePanneau, _myGame._positionPanneau, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureEcranFin, _myGame._positionEcranFin, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureFioleShop, _myGame._positionFioleShop, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureArbreShop, _myGame._positionArbreShop, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureExitGameButton, _myGame._positionExitGameButton, Color.White);
            _myGame.SpriteBatch.Draw(_myGame._textureExitButton, _myGame._positionExitButton, Color.White); 
            _myGame.SpriteBatch.End();

        }
    }
}
