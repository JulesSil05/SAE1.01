﻿using Microsoft.Xna.Framework;
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
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Vector2 _persoPosition;
        public AnimatedSprite _perso;
        public TiledMap _tiledMap;
        public TiledMapRenderer _tiledMapRenderer;
        private int _vitessePerso = 60;

        public OrthographicCamera _camera;
        public Vector2 _cameraPosition;

        public TiledMapTileLayer mapLayer;

        public bool verifPanneau = false;

        public readonly ScreenManager _screenManager;
        public SpriteBatch SpriteBatch { get; set; }

        public Texture2D _texturePanneau;
        public Vector2 _positionPanneau;



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
            _graphics.PreferredBackBufferHeight = _graphics.PreferredBackBufferHeight * 2; //--jules--Definition de la hauteur de l'écran
            _graphics.ApplyChanges(); //--jules--Application des changements de taille
            _persoPosition = new Vector2(400, 900);
            _cameraPosition = _persoPosition;
            _positionPanneau = new Vector2(-626, 50);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            
            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 600, 360);
            _camera = new OrthographicCamera(viewportAdapter);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("motw.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            _tiledMap = Content.Load<TiledMap>("map");
            _texturePanneau = Content.Load<Texture2D>("panneau_porte_ferme");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("contraintes");
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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso;

            string animation = "face";

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight+0.2); //la tuile au-dessus en y
                animation = "walkNorth";
                if (!IsCollision(tx, ty))
                {
                    _persoPosition.Y -= walkSpeed; // _persoPosition vecteur position du sprite
                }
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_persoPosition.Y / _tiledMap.TileHeight +1.2); //la tuile au-dessus en y
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

            _perso.Play(animation);
            _perso.Update(deltaSeconds);
            _tiledMapRenderer.Update(gameTime);

            MoveCamera(gameTime);
            _camera.LookAt(_cameraPosition);
            ushort x = (ushort)(_persoPosition.X / _tiledMap.TileWidth);
            ushort y = (ushort)(_persoPosition.Y / _tiledMap.TileHeight);
            Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);

            if (mapLayer.GetTile(x, y).GlobalIdentifier == 881)
                verifPanneau = true;
            else
                verifPanneau = false;
            if (_positionPanneau.X > -626 && verifPanneau == false)
            {
                _positionPanneau.X = _positionPanneau.X - 40;
            }

            else if (mapLayer.GetTile(x, y).GlobalIdentifier == 566)
            {
                LoadScreenSmallHouse();
            }

            else if (mapLayer.GetTile(x, y).GlobalIdentifier == 2423)
            {
                LoadScreenTallHouse();
            }

            else if (mapLayer.GetTile(x, y).GlobalIdentifier == 881 && _positionPanneau.X < -66)
            {
                _positionPanneau.X = _positionPanneau.X + 40;
            }
            

            Console.WriteLine("_positionPanneau Position : " + _positionPanneau.X + "," + _positionPanneau.Y);
            Console.WriteLine(_persoPosition.X + "," + _persoPosition.Y);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.Draw(_perso, _persoPosition);
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.End();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texturePanneau, _positionPanneau, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
