/*=========================
 * Ecran de démarrage
 * deplacement + texture personnage
 * load de map
 * 
=========================*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

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
        private bool _playButtonClicked;
        private Texture2D _textureExitButton; //--jules--Definition texture du bouton de sortie
        private Vector2 _positionExitButton; //--jules--Definition position du bouton de sortie

        private Texture2D _textureOngletMenu; //--jules--Definition texture onglet menu
        private Vector2 _positionOngletMenu; //--jules--Definition position onglet menu
        private Texture2D _textureOngletMenuQuitter; //--jules--Definition texture onglet menu
        private Vector2 _positionOngletMenuQuitter; //--jules--Definition position onglet menu

        private Texture2D _textureSettingButton; //--jules--Definition texture onglet menu
        private Vector2 _positionSettingButton; //--jules--Definition position onglet menu

        private Texture2D _textureCloseButton; //--jules--Definition texture bouton fermer
        private Vector2 _positionCloseButton; //--jules--Definition position bouton fermer

        private Texture2D _textureOui; //--jules--Definition texture bouton fermer
        private Vector2 _positionOui; //--jules--Definition position bouton fermer
        private Texture2D _textureNon; //--jules--Definition texture bouton fermer
        private Vector2 _positionNon; //--jules--Definition position bouton fermer

        private Song _backMusic; //--jules--Définition de la musique de fond pour le menu
        private SoundEffect _clickButton; //--jules--Définition du son pour le clique sur un boutton du menu

        private MouseState _mouseState; //--jules--Définition etat de la souris

        private Vector2 _persoPosition;
        private AnimatedSprite _perso;
        private int _vitessePerso = 100;
        //--jules--=============================

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1552; //--jules--Definition de la largeur de l'écran
            _graphics.PreferredBackBufferHeight = _graphics.PreferredBackBufferHeight * 2; //--jules--Definition de la hauteur de l'écran
            _graphics.ApplyChanges(); //--jules--Application des changements de taille
            _positionBackground = new Vector2(0, 0); //--jules--Position du fond
            _positionLogo = new Vector2(_graphics.PreferredBackBufferWidth - 1280, 200); //--jules--Position du logo
            _positionStartButton = new Vector2((_graphics.PreferredBackBufferWidth - 1170), 600); //--jules--Position du bouton de démarrage
            _positionExitButton = new Vector2((_graphics.PreferredBackBufferWidth - 600), 600); //--jules--Position du bouton de sortie
            _positionOngletMenu = new Vector2(-10000, -10000); //--jules--Position onglet menu
            _positionSettingButton = new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 100); //--jules--Position du bouton paramètres
            _positionCloseButton = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionOngletMenuQuitter = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionOui = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionNon = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _persoPosition = new Vector2(100, 100);
            _playButtonClicked = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _textureBackground = Content.Load<Texture2D>("background_menu"); //--jules--Chargement texture de fond
            _textureLogo = Content.Load<Texture2D>("logo"); //--jules--Chargement texture du logo
            _textureStartButton = Content.Load<Texture2D>("start_button"); //--jules--Chargement texture du bouton de démarrage
            _textureExitButton = Content.Load<Texture2D>("exit_button"); //--jules--Chargement texture du bouton de sortie
            _backMusic = Content.Load<Song>("back_music"); //--jules--Chargement du fichier audio de la musique de fond du menu
            _clickButton = Content.Load<SoundEffect>("click_button"); //--jules--Chargement du fichier audio de la musique de fond du menu
            MediaPlayer.Play(_backMusic); //--jules--Démarrage de la musique de fond
            MediaPlayer.IsRepeating = true; //--jules--Répétition de la musique
            _textureOngletMenu = Content.Load<Texture2D>("back_onglet_parametre"); //--jules--Chargement du fond du menu
            _textureSettingButton = Content.Load<Texture2D>("setting_button"); //--jules--Chargement du bouton paramètre
            _textureCloseButton = Content.Load<Texture2D>("close"); //--jules--Chargement du bouton fermer
            _textureOngletMenuQuitter = Content.Load<Texture2D>("back_onglet_menu"); //--jules--Chargement du bouton fermer
            _textureOui = Content.Load<Texture2D>("Yes"); //--jules--Chargement du bouton fermer
            _textureNon = Content.Load<Texture2D>("No"); //--jules--Chargement du bouton fermer
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("character.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _positionBackground.X = _positionBackground.X - 1; //--jules--Déplacement du fond 
            if (_positionBackground.X < -3064)
                _positionBackground.X = 0;

            //--jules--Interception souris
            _mouseState = Mouse.GetState();

            Rectangle rBouton = new Rectangle((int)_positionStartButton.X, (int)_positionStartButton.Y, _textureStartButton.Width, _textureStartButton.Height); //--jules--HitBox bouton play
            Rectangle rBoutonQuitter = new Rectangle((int)_positionExitButton.X, (int)_positionExitButton.Y, _textureExitButton.Width, _textureExitButton.Height); //--jules--HitBox bouton quitter
            Rectangle rBoutonParametre = new Rectangle((int)_positionSettingButton.X, (int)_positionSettingButton.Y, _textureSettingButton.Width, _textureSettingButton.Height); //--jules--HitBox bouton quitter

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (rBoutonParametre.Contains(_mouseState.Position)) //--jules--config bouton paramètres
                {
                    //Affichage du menu
                    _clickButton.Play();
                    _positionOngletMenu = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 537, _graphics.PreferredBackBufferHeight / 2 - 330); //--jules--Position onglet menu
                    _positionCloseButton = new Vector2(1200, 200); //--jules--Position du bouton fermer
                    _positionStartButton = new Vector2(10000, 10000); //--jules--Position du bouton de démarrage
                    _positionExitButton = new Vector2(10000, 10000); //--jules--Position du bouton de sortie
                    _positionSettingButton = new Vector2(10000, 10000); //--jules--Position du bouton paramètres
                }

                //Interaction avec le menu
                Rectangle rBoutonClose = new Rectangle((int)_positionCloseButton.X, (int)_positionCloseButton.Y, _textureCloseButton.Width, _textureCloseButton.Height); //--jules--HitBox bouton fermer
                if (rBoutonClose.Contains(_mouseState.Position)) //--jules--config bouton fermer
                {
                    _clickButton.Play();
                    _positionOngletMenu = new Vector2(10000, 10000); //--jules--Position onglet menu
                    _positionCloseButton = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                    _positionStartButton = new Vector2((_graphics.PreferredBackBufferWidth - 1170), 600); //--jules--Position du bouton de démarrage
                    _positionExitButton = new Vector2((_graphics.PreferredBackBufferWidth - 600), 600); //--jules--Position du bouton de sortie
                    _positionSettingButton = new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 100); //--jules--Position du bouton paramètres
                }

                if (rBouton.Contains(_mouseState.Position) || _playButtonClicked == true) //--jules--config bouton play
                {
                    _playButtonClicked = true;
                    _clickButton.Play();
                    //--jules--
                    //====================================
                    //CINEMATIQUE DE LANCEMENT DE LA PARTIE
                    //====================================
                    //Lancement de la partie
                    

                    KeyboardState keyboardState = Keyboard.GetState();
                    _positionBackground = new Vector2(-10000, -10000); //--jules--Position du fond
                    _positionLogo = new Vector2(-10000, -10000); //--jules--Position du logo
                    _positionStartButton = new Vector2(-10000, -10000); //--jules--Position du bouton de démarrage
                    _positionExitButton = new Vector2(-10000, -10000); //--jules--Position du bouton de sortie
                    _positionOngletMenu = new Vector2(10000, 10000); //--jules--Position onglet menu
                    _positionSettingButton = new Vector2(-10000, -100000); //--jules--Position du bouton paramètres
                    _positionCloseButton = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                    _positionOngletMenuQuitter = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                    _positionOui = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                    _positionNon = new Vector2(10000, 10000); //--jules--Position du bouton fermer

                    float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    float walkSpeed = deltaSeconds * _vitessePerso;

                    string animation = "idle";

                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        animation = "walkWest";
                        _persoPosition.X -= walkSpeed;
                    }
                    _perso.Play(animation);
                    _perso.Update(deltaSeconds);
                    
                }

                if (rBoutonQuitter.Contains(_mouseState.Position)) //--jules--config bouton quitter
                {
                    //Affichage du menu quitter
                    _clickButton.Play();
                    _positionOngletMenuQuitter = new Vector2((_graphics.PreferredBackBufferWidth / 2) - 268, (_graphics.PreferredBackBufferHeight / 2) - 165); //--jules--Position du menu quitter
                    _positionOui = new Vector2(600, 540); //--jules--Position du menu quitter
                    _positionNon = new Vector2(840, 540); //--jules--Position du menu quitter
                    _positionStartButton = new Vector2(10000, 10000); //--jules--Position du bouton de démarrage
                    _positionExitButton = new Vector2(10000, 10000); //--jules--Position du bouton de sortie
                    _positionLogo = new Vector2(10000, 10000); //--jules--Position du logo
                    _positionSettingButton = new Vector2(10000, 10000); //--jules--Position du bouton paramètres

                }
                //Interaction avec le menu quitter
                Rectangle rBoutonOui = new Rectangle((int)_positionOui.X, (int)_positionOui.Y, _textureOui.Width, _textureOui.Height); //--jules--HitBox bouton fermer
                if (rBoutonOui.Contains(_mouseState.Position)) //--jules--config bouton fermer
                {
                    Exit();
                }
                Rectangle rBoutonNon = new Rectangle((int)_positionNon.X, (int)_positionNon.Y, _textureNon.Width, _textureNon.Height); //--jules--HitBox bouton fermer
                if (rBoutonNon.Contains(_mouseState.Position)) //--jules--config bouton fermer
                {
                    _clickButton.Play();
                    _positionOngletMenuQuitter = new Vector2(10000, 10000); //--jules--Position onglet menu
                    _positionCloseButton = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                    _positionOui = new Vector2(10000, 10000); //--jules--Position du menu quitter
                    _positionNon = new Vector2(10000, 10000); //--jules--Position du menu quitter
                    _positionStartButton = new Vector2((_graphics.PreferredBackBufferWidth - 1170), 600); //--jules--Position du bouton de démarrage
                    _positionExitButton = new Vector2((_graphics.PreferredBackBufferWidth - 600), 600); //--jules--Position du bouton de sortie
                    _positionLogo = new Vector2(_graphics.PreferredBackBufferWidth - 1280, 200); //--jules--Position du logo
                    _positionSettingButton = new Vector2(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferHeight - 100); //--jules--Position du bouton paramètres
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
            _spriteBatch.Draw(_textureExitButton, _positionExitButton, Color.White); //--jules--affichage du bouton quitter
            _spriteBatch.Draw(_textureOngletMenu, _positionOngletMenu, Color.White); //--jules--affichage de l'onglet du menu
            _spriteBatch.Draw(_textureSettingButton, _positionSettingButton, Color.White); //--jules--affichage du bouton paramètres 
            _spriteBatch.Draw(_textureCloseButton, _positionCloseButton, Color.White); //--jules--affichage du bouton fermer 
            _spriteBatch.Draw(_textureOngletMenuQuitter, _positionOngletMenuQuitter, Color.White); //--jules--affichage du bouton fermer 
            _spriteBatch.Draw(_textureOui, _positionOui, Color.White); //--jules--affichage du bouton fermer 
            _spriteBatch.Draw(_textureNon, _positionNon, Color.White); //--jules--affichage du bouton fermer 
            _spriteBatch.Draw(_perso, _persoPosition);
            _spriteBatch.End(); //--jules--fin affichage
            base.Draw(gameTime);
        }
    }
}
