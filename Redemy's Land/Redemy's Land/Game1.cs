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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace RedemysLand
{
    public class Game1 : Game
    {
        //MENU
        private GraphicsDeviceManager _graphics;

        public KeyboardState keyboardState = Keyboard.GetState();

        private Texture2D _textureBackground; //--jules--Definition texture de fond
        private Vector2 _positionBackground; //--jules--Definition position de fond
        private Texture2D _textureLogo; //--jules--Definition texture du logo
        private Vector2 _positionLogo; //--jules--Definition position du logo

        private Texture2D _textureStartButton; //--jules--Definition texture du bouton de démarrage
        private Vector2 _positionStartButton; //--jules--Definition position du bouton de démarrage
        private bool _playButtonClicked;
        private Texture2D _textureExitButton; //--jules--Definition texture du bouton de sortie
        private Vector2 _positionExitButton; //--jules--Definition position du bouton de sortie

        private Texture2D _textureOngletMenuQuitter; //--jules--Definition texture onglet menu
        private Vector2 _positionOngletMenuQuitter; //--jules--Definition position onglet menu

        private Texture2D _textureCloseButton; //--jules--Definition texture bouton fermer
        private Vector2 _positionCloseButton; //--jules--Definition position bouton fermer

        private Texture2D _textureOui; //--jules--Definition texture bouton fermer
        private Vector2 _positionOui; //--jules--Definition position bouton fermer
        private Texture2D _textureNon; //--jules--Definition texture bouton fermer
        private Vector2 _positionNon; //--jules--Definition position bouton fermer

        private Song _backMusic; //--jules--Définition de la musique de fond pour le menu
        private SoundEffect _clickButton; //--jules--Définition du son pour le clique sur un boutton du menu

        public MouseState _mouseState; //--jules--Définition etat de la souris

        //GAME
        public Vector2 _persoPosition;
        public AnimatedSprite _perso;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        private int _vitessePerso = 80;

        public OrthographicCamera _camera;
        public Vector2 _cameraPosition;

        public TiledMapTileLayer mapLayer;

        public bool verifPanneau;

        public readonly ScreenManager _screenManager;
        public SpriteBatch SpriteBatch { get; set; }

        public Texture2D _texturePanneau;
        public Vector2 _positionPanneau;
        public Texture2D _textureCoeur1, _textureCoeur2, _textureCoeur3, _textureCoeur4, _textureCoeur5;
        public Vector2 _positionCoeur1, _positionCoeur2, _positionCoeur3, _positionCoeur4, _positionCoeur5;
        public Texture2D _textureCase1, _textureCase2, _textureCase3, _textureCase4, _textureCase5, _textureCase6;
        public Vector2 _positionCase1, _positionCase2, _positionCase3, _positionCase4, _positionCase5, _positionCase6;

        public float _chronoGame;
        public SpriteFont _police;
        public Vector2 _positionTexte;

        public Texture2D _textureTexteIntro;
        public Vector2 _positionTexteIntro;

        public Texture2D _textureMoneyBag;
        public Vector2 _positionMoneyBag;
        public SpriteFont _policePorteMonnaie;
        public int _valeurPorteMonnaie;
        public Vector2 _positionPorteMonnaie;

        public Texture2D _textureEcranFin;
        public Vector2 _positionEcranFin;

        public Texture2D _textureVictoire;
        public Vector2 _positionVictoire;

        public Texture2D _textureExitGameButton;
        public Vector2 _positionExitGameButton;

        public Texture2D _textureBoutonE;
        public Vector2 _positionBoutonE;

        public Texture2D _textureBoutonConcocter;
        public Vector2 _positionBoutonConcocter;

        public Texture2D _texturetjd4;
        public Vector2 _positiontjd4;

        public Texture2D _textureDialoguePnj;
        public Vector2 _positionDialoguePnj;

        public Texture2D _textureFioleShop, _textureArbreShop, _textureChestKey;
        public Vector2 _positionFioleShop, _positionArbreShop, _positionChestKey;

        public Texture2D _textureCoffre;
        public Vector2 _positionCoffre = new Vector2(960, 1295);

        public string AnimCharacter;
        public string animation = "face";

        public bool _emeraudeRamassee = false;
        public bool _argentRecu = false;
        public bool _achatMagasin = false;
        public bool _achatFiole= false;
        public bool _achatArbre = false;

        public bool _pommeRougeRamassee = false;
        public bool _pommeBleuRamassee = false;

        public bool _cleCoffreRamasse = false;
        public bool _coffreOuvert = false;
        public bool _cleMaisonRamasse = false;  

        public bool _planteRamassee = false;
        public bool _victoireClique = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1552; //--jules--Definition de la largeur de l'écran
            _graphics.PreferredBackBufferHeight = 960; //--jules--Definition de la hauteur de l'écran
            _graphics.ApplyChanges(); //--jules--Application des changements de taille

            _persoPosition = new Vector2(864, 886);
            _cameraPosition = _persoPosition;
            _positionPanneau = new Vector2(-626, 130);
            _positionCoeur1 = new Vector2(20, 10);
            _positionCoeur2 = new Vector2(140, 10);
            _positionCoeur3 = new Vector2(260, 10);
            _positionCoeur4 = new Vector2(380, 10);
            _positionCoeur5 = new Vector2(500, 10);

            _positionCase1 = new Vector2(20, 600);
            _positionCase2 = new Vector2(20, 700);
            _positionCase3 = new Vector2(20, 800);
            _positionCase4 = new Vector2(1400, 600);
            _positionCase5 = new Vector2(1400, 700);
            _positionCase6 = new Vector2(1400, 800);

            _positionTexte = new Vector2(1300, 20);
            _positionMoneyBag = new Vector2(1280, 150);
            _positionPorteMonnaie = new Vector2(1400, 150);

            _positionTexteIntro = new Vector2(500, 140);
            verifPanneau = false;

            _chronoGame = 500;

            _positionEcranFin = new Vector2(-10000, -10000);
            _positionExitGameButton = new Vector2(-10000, -10000);

            _positionVictoire = new Vector2(-10000, -10000);

            _positiontjd4 = new Vector2(-10000, -10000);
            _positionDialoguePnj = new Vector2(-10000, -10000);
            
            

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            
            //MENU
            _positionBackground = new Vector2(0, 0); //--jules--Position du fond
            _positionLogo = new Vector2(_graphics.PreferredBackBufferWidth - 1280, 200); //--jules--Position du logo
            _positionStartButton = new Vector2((_graphics.PreferredBackBufferWidth - 1170), 600); //--jules--Position du bouton de démarrage
            _positionExitButton = new Vector2((_graphics.PreferredBackBufferWidth - 600), 600); //--jules--Position du bouton de sortie
            _positionBoutonE = new Vector2(-10000, -10000); //--jules--Position du bouton de sortie
            _positionBoutonConcocter = new Vector2(-10000, -10000); //--jules--Position du bouton de sortie
            _positionCloseButton = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionOngletMenuQuitter = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionOui = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _positionNon = new Vector2(-10000, -10000); //--jules--Position du bouton fermer
            _playButtonClicked = false;
            _valeurPorteMonnaie = 0;
            _positionFioleShop = new Vector2(-10000, -10000);
            _positionArbreShop = new Vector2(-10000, -10000);
            _positionChestKey = new Vector2(457, 783);

            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 600, 360);
            _camera = new OrthographicCamera(viewportAdapter);
        }
        protected override void LoadContent()
        {
           SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("motw.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _tiledMap = Content.Load<TiledMap>("map");
            _texturePanneau = Content.Load<Texture2D>("panneau_porte_ferme");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureCoeur1 = Content.Load<Texture2D>("coeur");
            _textureCoeur2 = Content.Load<Texture2D>("coeur");
            _textureCoeur3 = Content.Load<Texture2D>("coeur");
            _textureCoeur4 = Content.Load<Texture2D>("coeur");
            _textureCoeur5 = Content.Load<Texture2D>("coeur");

            _textureCase1 = Content.Load<Texture2D>("case1");
            _textureCase2 = Content.Load<Texture2D>("case2");
            _textureCase3 = Content.Load<Texture2D>("case3");
            _textureCase4 = Content.Load<Texture2D>("case4");
            _textureCase5 = Content.Load<Texture2D>("case5");
            _textureCase6 = Content.Load<Texture2D>("case6");

            _textureFioleShop = Content.Load<Texture2D>("fiole_shop");
            _textureArbreShop = Content.Load<Texture2D>("arbre_shop");
            _textureChestKey = Content.Load<Texture2D>("key");

            _textureCoffre = Content.Load<Texture2D>("coffre_ferme");

            _textureTexteIntro = Content.Load<Texture2D>("parchemin");
            _textureEcranFin = Content.Load<Texture2D>("ecran_fin");
            _textureExitGameButton = Content.Load<Texture2D>("reset_button");
            _textureBoutonE = Content.Load<Texture2D>("toucheE");
            _textureBoutonConcocter = Content.Load<Texture2D>("toucheConcocter");
            _textureVictoire = Content.Load<Texture2D>("victoire");
            _textureMoneyBag = Content.Load<Texture2D>("money_bag");

            _police = Content.Load<SpriteFont>("Arial");

            _policePorteMonnaie = Content.Load<SpriteFont>("Arial");

            _texturetjd4 = Content.Load<Texture2D>("tjd4");
            _textureDialoguePnj = Content.Load<Texture2D>("dialogue_émeraude");

            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contraintes");

            //MENU
            _textureBackground = Content.Load<Texture2D>("background_menu"); //--jules--Chargement texture de fond
            _textureLogo = Content.Load<Texture2D>("logo"); //--jules--Chargement texture du logo
            _textureStartButton = Content.Load<Texture2D>("start_button"); //--jules--Chargement texture du bouton de démarrage
            _textureExitButton = Content.Load<Texture2D>("exit_button"); //--jules--Chargement texture du bouton de sortie
            _backMusic = Content.Load<Song>("back_music"); //--jules--Chargement du fichier audio de la musique de fond du menu
            _clickButton = Content.Load<SoundEffect>("click_button"); //--jules--Chargement du fichier audio de la musique de fond du menu
            MediaPlayer.Play(_backMusic); //--jules--Démarrage de la musique de fond
            MediaPlayer.IsRepeating = true; //--jules--Répétition de la musique
            _textureCloseButton = Content.Load<Texture2D>("close"); //--jules--Chargement du bouton fermer
            _textureOngletMenuQuitter = Content.Load<Texture2D>("back_onglet_menu"); //--jules--Chargement du bouton fermer
            _textureOui = Content.Load<Texture2D>("Yes"); //--jules--Chargement du bouton fermer
            _textureNon = Content.Load<Texture2D>("No"); //--jules--Chargement du bouton fermer
        }

        private void MoveCamera(GameTime gameTime)
        {
            _cameraPosition = _persoPosition;
        }

        private bool IsCollision(ushort x, ushort y)
        {
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
        public void LoadScreenMap()
        {
            _screenManager.LoadScreen(new Map(this));
            _persoPosition = new Vector2(120, 164);
        }
        public void LoadScreenSmallHouse()
        {
            _screenManager.LoadScreen(new MapSmallHouse(this));
            _persoPosition = new Vector2(120, 154);
        }
        public void LoadScreenTallHouse()
        {
            _screenManager.LoadScreen(new MapTallHouse(this));
            _persoPosition = new Vector2(168, 203);
        }

        public void LoadScreenBedroom()
        {
            _screenManager.LoadScreen(new MapBedroom(this));
            _persoPosition = new Vector2(120, 200);
        }
        public void LoadScreenGrotte()
        {
            _screenManager.LoadScreen(new Grotte(this));
            _persoPosition = new Vector2(407, 1086);
        }
        public void LoadScreenFondGrotte()
        {
            _screenManager.LoadScreen(new FondGrotte(this));
            _persoPosition = new Vector2(50, 50);
        }

        protected override void Update(GameTime gameTime)
        {
            //MENU=============================================================
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();        

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;

            _positionBackground.X = _positionBackground.X - 1; //--jules--Déplacement du fond 
            if (_positionBackground.X < -3064)
                _positionBackground.X = 0;

            //--jules--Interception souris
            _mouseState = Mouse.GetState();

            Rectangle rBouton = new Rectangle((int)_positionStartButton.X, (int)_positionStartButton.Y, _textureStartButton.Width, _textureStartButton.Height); //--jules--HitBox bouton play
            Rectangle rBoutonQuitter = new Rectangle((int)_positionExitButton.X, (int)_positionExitButton.Y, _textureExitButton.Width, _textureExitButton.Height); //--jules--HitBox bouton quitter

            Rectangle rBoutonClose = new Rectangle((int)_positionCloseButton.X, (int)_positionCloseButton.Y, _textureCloseButton.Width, _textureCloseButton.Height); //--jules--HitBox bouton fermer
            if (rBouton.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed) //--jules--config bouton play
            {
                _playButtonClicked = true;
                _clickButton.Play();

                _positionBackground = new Vector2(-10000, -10000); //--jules--Position du fond
                _positionLogo = new Vector2(-10000, -10000); //--jules--Position du logo
                _positionStartButton = new Vector2(-10000, -10000); //--jules--Position du bouton de démarrage
                _positionExitButton = new Vector2(-10000, -10000); //--jules--Position du bouton de sortie

                _positionCloseButton = new Vector2(980, 150); //--jules--Position du bouton fermer
                _positionOngletMenuQuitter = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                _positionOui = new Vector2(10000, 10000); //--jules--Position du bouton fermer
                _positionNon = new Vector2(10000, 10000); //--jules--Position du bouton fermer           
            }
            
            

            else if (_mouseState.LeftButton == ButtonState.Pressed && _playButtonClicked != true)
            {
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
                }
            }

            
            else if (rBoutonClose.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed) //--jules--config bouton fermer
            {
                _clickButton.Play();
                _positionTexteIntro = new Vector2(-10000, -10000);
                _positionCloseButton = new Vector2(-10000, -10000);
            }
            
            else if (_playButtonClicked == true)
            {
                //GAME=============================================================

                if (_chronoGame >= 30)
                {
                    animation = "face";                    

                    KeyboardState keyboardState = Keyboard.GetState();
                    if (keyboardState.IsKeyDown(Keys.Z))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 0.2); //la tuile au-dessus en y
                        animation = "walkNorth";
                        if (!IsCollision(tx, ty))
                        {
                            _persoPosition.Y -= walkSpeed; // _persoPosition vecteur position du sprite
                        }
                    }
                    if (keyboardState.IsKeyDown(Keys.S))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1.2); //la tuile au-dessus en y
                        animation = "walkSouth";
                        if (!IsCollision(tx, ty))
                            _persoPosition.Y += walkSpeed; // _persoPosition vecteur position du sprite
                    }
                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth - 0.5);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1); //la tuile au-dessus en y
                        animation = "walkWest";
                        if (!IsCollision(tx, ty))
                            _persoPosition.X -= walkSpeed; // _persoPosition vecteur position du sprite
                    }
                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth + 0.6);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1); //la tuile au-dessus en y
                        animation = "walkEast";
                        if (!IsCollision(tx, ty))
                            _persoPosition.X += walkSpeed; // _persoPosition vecteur position du sprite
                    }
                }

                else if (_chronoGame < 30)
                {
                    _vitessePerso = 40;
                    animation = "faceZombie";

                    KeyboardState keyboardState = Keyboard.GetState();

                    if (keyboardState.IsKeyDown(Keys.Z))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 0.2); //la tuile au-dessus en y
                        animation = "walkNorthZombie";
                        if (!IsCollision(tx, ty))
                            _persoPosition.Y -= walkSpeed; // _persoPosition vecteur position du sprite
                    }
                    if (keyboardState.IsKeyDown(Keys.S))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1.2); //la tuile au-dessus en y
                        animation = "walkSouthZombie";
                        if (!IsCollision(tx, ty))
                            _persoPosition.Y += walkSpeed; // _persoPosition vecteur position du sprite
                    }
                    if (keyboardState.IsKeyDown(Keys.Q))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth - 0.5);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1); //la tuile au-dessus en y
                        animation = "walkWestZombie";
                        if (!IsCollision(tx, ty))
                            _persoPosition.X -= walkSpeed; // _persoPosition vecteur position du sprite
                    }
                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth + 0.6);
                        ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight + 1); //la tuile au-dessus en y
                        animation = "walkEastZombie";
                        if (!IsCollision(tx, ty))
                            _persoPosition.X += walkSpeed; // _persoPosition vecteur position du sprite
                    }
                }               

                MoveCamera(gameTime);
                _camera.LookAt(_cameraPosition);
                ushort x = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                ushort y = (ushort)(_persoPosition.Y / _tiledMap.TileHeight);
                Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);

                KeyboardState _keyboardState = Keyboard.GetState();
                if (mapLayer.GetTile(x, y).GlobalIdentifier == 566 && _cleMaisonRamasse == true)
                {
                    LoadScreenSmallHouse();
                }

                else if (mapLayer.GetTile(x, y).GlobalIdentifier == 564)
                {
                    LoadScreenGrotte();
                }

                else if (mapLayer.GetTile(x, y).GlobalIdentifier == 2423)
                {
                    LoadScreenTallHouse();
                }

                else if (mapLayer.GetTile(x, y).GlobalIdentifier == 881 && _positionPanneau.X < -66)
                {
                    verifPanneau = true;
                    _positionPanneau.X = _positionPanneau.X + 40;
                }
                else if (mapLayer.GetTile(x, y).GlobalIdentifier == 881)
                    verifPanneau = true;
                else if (mapLayer.GetTile(x, y).GlobalIdentifier != 881 && verifPanneau == true)
                    verifPanneau = false;
                else if (mapLayer.GetTile(x, y).GlobalIdentifier == 3137 && _achatMagasin == false)
                {
                    _positionFioleShop = new Vector2(250, 300);
                    _positionArbreShop = new Vector2(1000, 300);
                }
                else if (mapLayer.GetTile(x, y).GlobalIdentifier != 3137)
                {
                    _positionFioleShop = new Vector2(-10000, -10000);
                    _positionArbreShop = new Vector2(-10000, -10000);
                }

                if (_positionPanneau.X > -626 && verifPanneau == false)
                {
                    _positionPanneau.X = _positionPanneau.X - 40;
                }

                _chronoGame -= deltaSeconds;


                //Déplacement coeur
                if (_chronoGame < 400)
                    _positionCoeur5 = new Vector2(-10000, -10000);
                if (_chronoGame < 300)
                    _positionCoeur4 = new Vector2(-10000, -10000);
                if (_chronoGame < 200)
                    _positionCoeur3 = new Vector2(-10000, -10000);
                if (_chronoGame < 100)
                    _positionCoeur2 = new Vector2(-10000, -10000);
                

                if (_chronoGame < 0)
                {
                    _positionEcranFin = new Vector2(0, 0);
                    _positionExitGameButton = new Vector2(630, 750);
                }

                if(_persoPosition.X >= 407 && _persoPosition.X <= 430 && _persoPosition.Y >= 1190 && _persoPosition.Y <= 1200)
                {
                    _positiontjd4 = new Vector2(394, 1145);
                }
                else
                    _positiontjd4 = new Vector2(-10000, -10000);



                //emeraude
                if (_persoPosition.X >= 1174 && _persoPosition.X <= 1191 && _persoPosition.Y >= 1084 && _persoPosition.Y <= 1122 && _emeraudeRamassee == true && _argentRecu == false)
                {
                    _argentRecu = true;
                    _valeurPorteMonnaie = _valeurPorteMonnaie + 100;
                    
                    _positionDialoguePnj = new Vector2(-10000, -10000);
                }

                else if (_persoPosition.X >= 1174 && _persoPosition.X <= 1191 && _persoPosition.Y >= 1084 && _persoPosition.Y <= 1122 && _argentRecu == false)
                {
                    _positionDialoguePnj = new Vector2(1158, 990);
                }

                else if (_argentRecu == true)
                {
                    _textureCase4 = Content.Load<Texture2D>("case4");
                }

                else
                {
                    _positionDialoguePnj = new Vector2(-10000, -10000);
                }

                //clé coffre
                if (_cleCoffreRamasse == true)
                {
                    _textureCase5 = Content.Load<Texture2D>("case5ramasse");
                    _positionChestKey = new Vector2(-10000, -10000);
                }
                else if (_persoPosition.X >= 454 && _persoPosition.X <= 472 && _persoPosition.Y >= 778 && _persoPosition.Y <= 788 && _cleCoffreRamasse == false)
                {
                    _cleCoffreRamasse = true;
                    _positionChestKey = new Vector2(-10000, -10000);
                    _textureCase5 = Content.Load<Texture2D>("case5ramasse");
                }

                else
                {
                    _positionChestKey = new Vector2(457, 783);
                    _textureCase5 = Content.Load<Texture2D>("case5");
                }

                

                //coffre
                if (_cleMaisonRamasse == true)
                {
                    _positionBoutonE = new Vector2(-10000, -10000);
                    _textureCase6 = Content.Load<Texture2D>("case6ramasse");
                    _textureCoffre = Content.Load<Texture2D>("coffre_ouvert");
                }
                else if (_persoPosition.X >= 944 && _persoPosition.X <= 970 && _persoPosition.Y >= 1284 && _persoPosition.Y <= 1311 && _cleCoffreRamasse == true && _keyboardState.IsKeyDown(Keys.E))
                {
                    _cleMaisonRamasse = true;
                    _textureCase6 = Content.Load<Texture2D>("case6ramasse");
                    _textureCoffre = Content.Load<Texture2D>("coffre_ouvert");
                }

                else if (_persoPosition.X >= 944 && _persoPosition.X <= 970 && _persoPosition.Y >= 1284 && _persoPosition.Y <= 1311 && _cleCoffreRamasse == true)
                {
                    _positionBoutonE = new Vector2(960, 1279);
                }               

                else
                {
                    _positionBoutonE = new Vector2(-10000, -10000);
                    _textureCoffre = Content.Load<Texture2D>("coffre_ferme");
                    _textureCase6 = Content.Load<Texture2D>("case6");
                }


                //shop
                Rectangle rShopFiole = new Rectangle((int)_positionFioleShop.X, (int)_positionFioleShop.Y, _textureFioleShop.Width, _textureFioleShop.Height); //--jules--HitBox bouton fermer
                if (rShopFiole.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed && _valeurPorteMonnaie == 100)
                {
                    _achatFiole = true;
                    _textureCase4 = Content.Load<Texture2D>("case3ramasse");
                    _valeurPorteMonnaie = _valeurPorteMonnaie - 100;
                    _positionFioleShop = new Vector2(-10000, -10000);
                    _positionArbreShop = new Vector2(-10000, -10000);
                    _achatMagasin = true;
                    _clickButton.Play();
                }
                Rectangle rShopArbre = new Rectangle((int)_positionArbreShop.X, (int)_positionArbreShop.Y, _textureArbreShop.Width, _textureArbreShop.Height); //--jules--HitBox bouton fermer
                if (rShopArbre.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed && _valeurPorteMonnaie == 100)
                {
                    _achatArbre = true;
                    _valeurPorteMonnaie = _valeurPorteMonnaie - 100;
                    _positionFioleShop = new Vector2(-10000, -10000);
                    _positionArbreShop = new Vector2(-10000, -10000);
                    _achatMagasin = true;
                    _clickButton.Play();
                }

                if (_achatMagasin == true)
                {
                    _textureCase3 = Content.Load<Texture2D>("case3ramasse");
                }

                //victoire

                if (_persoPosition.X >= 1089 && _persoPosition.X <= 1105 && _persoPosition.Y >= 1092 && _persoPosition.Y <= 1108 && _planteRamassee == true && _pommeBleuRamassee == true && _achatFiole == true && _keyboardState.IsKeyDown(Keys.E))
                {
                    _victoireClique = true;
                    _positionVictoire = new Vector2(350, 200);
                    _positionCoeur1 = new Vector2(-10000, -10000);
                    _positionCoeur2 = new Vector2(-10000, -10000);
                    _positionCoeur3 = new Vector2(-10000, -10000);
                    _positionCoeur4 = new Vector2(-10000, -10000);
                    _positionCoeur5 = new Vector2(-10000, -10000);
                    _positionCase1 = new Vector2(-10000, -10000);
                    _positionCase2 = new Vector2(-10000, -10000);
                    _positionCase3 = new Vector2(-10000, -10000);
                    _positionCase4 = new Vector2(-10000, -10000);
                    _positionCase5 = new Vector2(-10000, -10000);
                    _positionCase6 = new Vector2(-10000, -10000);
                    _positionTexte = new Vector2(-10000, -10000);
                    _positionMoneyBag = new Vector2(-10000, -10000);
                    _positionPorteMonnaie = new Vector2(-10000, -10000);
                    _vitessePerso = 0;
                }

                else if (_persoPosition.X >= 1089 && _persoPosition.X <= 1105 && _persoPosition.Y >= 1092 && _persoPosition.Y <= 1108 && _planteRamassee == true && _pommeBleuRamassee == true && _achatFiole == true)
                {
                    _positionBoutonConcocter = new Vector2(1087, 1062);
                }

                else if (_victoireClique == false)
                {
                    _positionBoutonConcocter = new Vector2(-10000, -10000);
                }

                _perso.Play(animation);
                _perso.Update(deltaSeconds);
                _tiledMapRenderer.Update(gameTime);

                Rectangle rReset = new Rectangle((int)_positionExitGameButton.X, (int)_positionExitGameButton.Y, _textureExitGameButton.Width, _textureExitGameButton.Height); //--jules--HitBox bouton fermer
                if (rReset.Contains(_mouseState.Position) && _mouseState.LeftButton == ButtonState.Pressed)
                {
                    _clickButton.Play();
                    Exit();
                }
                Console.WriteLine("Personage Position Position : " + _persoPosition.X + "," + _persoPosition.Y);

                
                //GAME=============================================================

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var transformMatrix = _camera.GetViewMatrix();
            SpriteBatch.Begin(transformMatrix: transformMatrix);
            SpriteBatch.Draw(_textureChestKey, _positionChestKey, Color.White);
            SpriteBatch.Draw(_textureBoutonE, _positionBoutonE, Color.White);
            SpriteBatch.Draw(_textureBoutonConcocter, _positionBoutonConcocter, Color.White);
            SpriteBatch.Draw(_textureCoffre, _positionCoffre, Color.White);
            SpriteBatch.Draw(_texturetjd4, _positiontjd4, Color.White);
            SpriteBatch.Draw(_textureDialoguePnj, _positionDialoguePnj, Color.White);
            SpriteBatch.Draw(_perso, _persoPosition);
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            SpriteBatch.End();
            SpriteBatch.Begin();
            SpriteBatch.Draw(_textureVictoire, _positionVictoire, Color.White);
            SpriteBatch.Draw(_textureCoeur1, _positionCoeur1, Color.White);
            SpriteBatch.Draw(_textureCoeur2, _positionCoeur2, Color.White);
            SpriteBatch.Draw(_textureCoeur3, _positionCoeur3, Color.White);
            SpriteBatch.Draw(_textureCoeur4, _positionCoeur4, Color.White);
            SpriteBatch.Draw(_textureCoeur5, _positionCoeur5, Color.White);

            SpriteBatch.Draw(_textureCase1, _positionCase1, Color.White);
            SpriteBatch.Draw(_textureCase2, _positionCase2, Color.White);
            SpriteBatch.Draw(_textureCase3, _positionCase3, Color.White);
            SpriteBatch.Draw(_textureCase4, _positionCase4, Color.White);
            SpriteBatch.Draw(_textureCase5, _positionCase5, Color.White);
            SpriteBatch.Draw(_textureCase6, _positionCase6, Color.White);

            SpriteBatch.DrawString(_police, "" + Math.Round(_chronoGame) + "", _positionTexte, Color.White);
            SpriteBatch.DrawString(_policePorteMonnaie, "" + _valeurPorteMonnaie + "", _positionPorteMonnaie, Color.White);
            SpriteBatch.Draw(_textureTexteIntro, _positionTexteIntro, Color.White);
            SpriteBatch.Draw(_textureMoneyBag, _positionMoneyBag, Color.White);
            
            SpriteBatch.Draw(_textureEcranFin, _positionEcranFin, Color.White);
            SpriteBatch.Draw(_textureExitGameButton, _positionExitGameButton, Color.White);

            //MENU
            SpriteBatch.Draw(_textureBackground, _positionBackground, Color.White); //--jules--affichage fond du menu
            SpriteBatch.Draw(_textureLogo, _positionLogo, Color.White); //--jules--affichage du logo
            SpriteBatch.Draw(_textureStartButton, _positionStartButton, Color.White); //--jules--affichage du bouton jouer
            SpriteBatch.Draw(_textureExitButton, _positionExitButton, Color.White); //--jules--affichage du bouton quitter
            SpriteBatch.Draw(_textureCloseButton, _positionCloseButton, Color.White); //--jules--affichage du bouton fermer 
            SpriteBatch.Draw(_textureOngletMenuQuitter, _positionOngletMenuQuitter, Color.White); //--jules--affichage du bouton fermer 
            SpriteBatch.Draw(_textureOui, _positionOui, Color.White); //--jules--affichage du bouton fermer 
            SpriteBatch.Draw(_textureNon, _positionNon, Color.White); //--jules--affichage du bouton fermer 
            SpriteBatch.Draw(_texturePanneau, _positionPanneau, Color.White);
            SpriteBatch.Draw(_textureFioleShop, _positionFioleShop, Color.White);
            SpriteBatch.Draw(_textureArbreShop, _positionArbreShop, Color.White);
            
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
