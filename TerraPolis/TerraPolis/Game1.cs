/*=========================
 * Ecran de démarrage
 * deplacement + texture personnage
 * load de map

=========================*/

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace TerraPolis
{
    public class Game1 : Game
    {
        //--jules--==========VARIABLES==========
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureBackground; //--jules--Definition texture de fond
        private Vector2 _positionBackground; //--jules--Definition position de fond
        private Texture2D _textureLogo; //--jules--Definition texture du logo
        private Vector2 _positionLogo; //--jules--Definition position du logo

        private Texture2D _textureStartButton; //--jules--Definition texture du bouton de démarrage
        private Vector2 _positionStartButton; //--jules--Definition position du bouton de démarrage
        private Texture2D _textureExitButton; //--jules--Definition texture du bouton de sortie
        private Vector2 _positionExitButton; //--jules--Definition position du bouton de sortie

        private MouseState _mouseState;


        //--jules--=============================

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 776;
            _graphics.ApplyChanges();

            _positionBackground = new Vector2(0, 0); //--jules--Position du fond
            _positionLogo = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 217, 100); //--jules--Position du logo
            _positionStartButton = new Vector2(_graphics.PreferredBackBufferWidth / 4 - 63, 300); //--jules--Position du bouton de démarrage
            _positionExitButton = new Vector2(_graphics.PreferredBackBufferWidth - 279, 300); //--jules--Position du bouton de sortie
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _textureBackground = Content.Load<Texture2D>("background_menu"); //--jules--Chargement texture de fond
            _textureLogo = Content.Load<Texture2D>("logo"); //--jules--Chargement texture du logo
            _textureStartButton = Content.Load<Texture2D>("start_button"); //--jules--Chargement texture du bouton de démarrage
            _textureExitButton = Content.Load<Texture2D>("exit_button"); //--jules--Chargement texture du bouton de sortie
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _positionBackground.X = _positionBackground.X - 1; //--jules--Déplacement du fond 
            if (_positionBackground.X < -1532)
                _positionBackground.X = 0;

            //--jules--Interception souris
            _mouseState = Mouse.GetState();

            Rectangle rBouton = new Rectangle((int)_positionStartButton.X, (int)_positionStartButton.Y, _textureStartButton.Width, _textureStartButton.Height); //--jules--HitBox bouton play
            Rectangle rBoutonQuitter = new Rectangle((int)_positionExitButton.X, (int)_positionExitButton.Y, _textureExitButton.Width, _textureExitButton.Height); //--jules--HitBox bouton quitter

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (rBouton.Contains(_mouseState.Position)) //--jules--config bouton play
                {


                }

                else if (rBoutonQuitter.Contains(_mouseState.Position)) //--jules--config bouton quitter
                {
                    Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(); //--jules--debut affichage
            _spriteBatch.Draw(_textureBackground, _positionBackground, Color.White * 0.5f); //--jules--affichage fond du menu
            _spriteBatch.Draw(_textureLogo, _positionLogo, Color.White); //--jules--affichage du logo
            _spriteBatch.Draw(_textureStartButton, _positionStartButton, Color.White); //--jules--affichage du bouton jouer
            _spriteBatch.Draw(_textureExitButton, _positionExitButton, Color.White); //--jules--affichage fdu bouton quitter
            _spriteBatch.End(); //--jules--fin affichage
            base.Draw(gameTime);
        }
    }
}
