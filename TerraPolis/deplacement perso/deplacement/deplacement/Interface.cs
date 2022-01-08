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

namespace deplacement
{
    public class Interface : GameScreen
    {
        private Game1 _myGame; // pour récupérer le jeu en cours

        public Interface(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { 
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _myGame._spriteBatch.Begin();

            _myGame._spriteBatch.End();

        }
    }
}
