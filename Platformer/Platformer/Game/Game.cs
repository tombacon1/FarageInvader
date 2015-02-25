using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using IndependentResolutionRendering;

namespace SpaceShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerShip _playerShip;
        List<EnemyShip> _enemyShips = new List<EnemyShip>();
        Background _background;
        Texture2D _laserTexture;
        List<Projectile> projectiles = new List<Projectile>();
        List<Explosion> explosions = new List<Explosion>();
        Texture2D _explosionTexture;
        SoundEffect _explosionSound;
        SoundEffect _laserFireSound;
        SoundEffectInstance soundEffectInstance;
        float _volume = 0.3f;
        KeyboardState previousKBState;
        private int fullScreenToggleTimeSeconds = -2;
        List<TextSprite> _text = new List<TextSprite>();
        SpriteFont _stencilFont;
        private Texture2D _missileTexture;
        private SoundEffect _missileSound;
        Player player;
        private int healthDelay;
        private SpriteFont _arialFont;
        
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Resolution.SetVirtualResolution(1280, 720);
            Resolution.SetResolution(1280, 720, false);
            player = new Player();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // load sprites
            _playerShip = new PlayerShip(Content.Load<Texture2D>(@"Sprites\Ship1"), 32, 32, new Vector2(400, 300));
            _background = new Background(Content.Load<Texture2D>(@"Sprites\background"), 1200, 1920, new Vector2(0, 0));
            _laserTexture = Content.Load<Texture2D>(@"Sprites\singlelaser");
            _missileTexture = Content.Load<Texture2D>(@"Sprites\HomingRocket_5x20");
            _enemyShips.Add(new EnemyShip(Content.Load<Texture2D>(@"Sprites\farage"), 220, 151, new Vector2(250, 250), 0.5f));
            _explosionTexture = Content.Load<Texture2D>(@"Sprites\Sheets\ExplosionSheet_48x48");
            
            // load sounds
            _laserFireSound = Content.Load<SoundEffect>(@"Sounds\science_fiction_laser_006");
            _explosionSound = Content.Load<SoundEffect>(@"Sounds\explosion");
            _missileSound = Content.Load<SoundEffect>(@"Sounds\Missile_Launch");
            
            // load fonts
            _stencilFont = Content.Load<SpriteFont>(@"Fonts\Stencil");
            _arialFont = Content.Load<SpriteFont>(@"Fonts\Arial");

            // load player health
            player.LoadContent(Content);

            _text.Add(new TextSprite("score", _stencilFont, "Score: ", Color.PapayaWhip, new Vector2(0, 20), new Vector2(1, 1), float.PositiveInfinity, 0f));
            _text.Add(new TextSprite("missilecount", _stencilFont, "Missiles: ", Color.PapayaWhip, new Vector2(0, 20), new Vector2(1, 1), float.PositiveInfinity, 0f));
            _text.Add(new TextSprite("gameover", _arialFont, "Game Over", Color.WhiteSmoke, new Vector2(0,0), new Vector2(1,1), float.PositiveInfinity, 0f, false));
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();

            // Allows the game to exit
            if (currentKBState.IsKeyDown(Keys.Escape))
                this.Exit();

            // changes sound volume
            if (currentKBState.IsKeyDown(Keys.Add))
                _volume = MathHelper.Clamp(_volume + 0.01f, 0f, 1f);
            if (currentKBState.IsKeyDown(Keys.Subtract))
                _volume = MathHelper.Clamp(_volume - 0.01f, 0f, 1f);

            // fires weapon
            if (currentKBState.IsKeyDown(Keys.Space))
            {
                if (gameTime.TotalGameTime.TotalMilliseconds - Projectile.lastFiredTime.TotalMilliseconds > SingleLaser.FireInterval)
                {
                    projectiles.Add(new SingleLaser(_laserTexture, 6, 10, _playerShip.Position, MathExtension.RadToVec2(_playerShip.Rotation)));
                    soundEffectInstance = _laserFireSound.CreateInstance();
                    soundEffectInstance.Volume = _volume;
                    soundEffectInstance.Play();
                    Projectile.lastFiredTime = gameTime.TotalGameTime;
                }
            }

            // trigger explosion animation
            if (currentKBState.IsKeyDown(Keys.Back))
            {
                explosions.Add(new Explosion(_explosionTexture, 0, 48, 48, 0, 75, _playerShip.Position));
                soundEffectInstance = _explosionSound.CreateInstance();
                soundEffectInstance.Volume = _volume;
                soundEffectInstance.Play();
            }

            // toggle fullscreen
            if ((currentKBState.IsKeyDown(Keys.RightControl) && currentKBState.IsKeyDown(Keys.Enter)) && 
                !(previousKBState.IsKeyDown(Keys.RightControl) && previousKBState.IsKeyDown(Keys.Enter)))
            {
                int timeChange = gameTime.TotalGameTime.Seconds - fullScreenToggleTimeSeconds;
                if (gameTime.TotalGameTime.Seconds - fullScreenToggleTimeSeconds > 1)
                {
                    fullScreenToggleTimeSeconds = gameTime.TotalGameTime.Seconds;
                    if(Resolution.Device.IsFullScreen)
                        Resolution.SetResolution(1280, 720, false);
                    else
                        Resolution.SetResolution(Resolution.ScreenWidth, Resolution.ScreenHeight, true);
                }
            }

            if (currentKBState.IsKeyDown(Keys.LeftControl) && !previousKBState.IsKeyDown(Keys.LeftControl))
            {
                if (Missile.Count > 0 && _enemyShips.Count > 0)
                {
                    // acquire target
                    EnemyShip target = _enemyShips[0];
                    projectiles.Add(new Missile(_missileTexture, 20, 5, _playerShip.Position, MathExtension.RadToVec2(_playerShip.Rotation), target));
                    soundEffectInstance = _missileSound.CreateInstance();
                    soundEffectInstance.Volume = _volume;
                    soundEffectInstance.Play();
                    Missile.Count--;
                }
            }

            previousKBState = currentKBState;

            // UPDATE LOGIC HERE

            if (_enemyShips.Count == 0 && explosions.Count == 0)
            {
                player.Level++;
                Missile.Count += player.Level;
                for (int i = 1; i <= player.Level; i++)
                    _enemyShips.Add(new EnemyShip(Content.Load<Texture2D>(@"Sprites\farage"), 220, 151, EnemyShip.GetRandomPosition(151,220), 0.5f));
            }

            foreach (Explosion e in explosions.AsEnumerable().Reverse())
            {
                if (e.CurrentFrame == 0 && e.isEnded)
                    explosions.Remove(e);
                else
                    e.Update(gameTime);
            }

            foreach (EnemyShip es in _enemyShips.AsEnumerable().Reverse())
            {
                if (es.Dead)
                {
                    _enemyShips.Remove(es);
                    continue;
                }
            }
            
            healthDelay--;
            foreach (EnemyShip es in _enemyShips)
            {
                if (MathExtension.CheckCollision(es.CollisionBox, _playerShip.CollisionCircle) && healthDelay <= 0)
                {
                    _playerShip.Velocity = 0;
                    player.Health -= 15;
                    healthDelay = 20;
                }
            }

            if (player.Health <= 0)
            {
                player.IsDead = true;
                foreach (TextSprite txt in _text)
                {
                    if (txt.Name == "gameover")
                        txt.IsVisible = true;
                }
            }

            foreach (Projectile p in projectiles.AsEnumerable().Reverse())
            {
                foreach (EnemyShip es in _enemyShips.AsEnumerable().Reverse())
                {
                    if (MathExtension.CheckCollision(p.CollisionBox, es.CollisionBox))
                    {
                        string type = p.GetType().Name;

                        switch (type)
                        {
                            case "SingleLaser":
                                player.Score += player.Level * 10;
                                es.HitCount++;
                                break;
                            case "Missile":
                                player.Score += player.Level * 50;
                                es.HitCount += 5;
                                break;
                        }
                        
                        explosions.Add(new Explosion(_explosionTexture, 0, 48, 48, 0, 75, p.Position));
                        soundEffectInstance = _explosionSound.CreateInstance();
                        soundEffectInstance.Volume = _volume;
                        soundEffectInstance.Play();
                        projectiles.Remove(p);
                        
                        continue;
                    }
                }

                if (p.Offscreen)
                    projectiles.Remove(p);
                else
                    p.Update(gameTime);
            }

            foreach (EnemyShip es in _enemyShips)
                es.Update(gameTime);

            _playerShip.Update(gameTime);

            foreach (TextSprite txt in _text)
                txt.Update(gameTime, player);

            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

            _background.Draw(spriteBatch);

            if (!player.IsDead)
            {
                foreach (Projectile p in projectiles)
                    p.Draw(spriteBatch);

                _playerShip.Draw(spriteBatch);

                foreach (EnemyShip es in _enemyShips)
                    es.Draw(spriteBatch);

                foreach (Explosion e in explosions)
                    e.Draw(spriteBatch);
            }

            foreach (TextSprite t in _text)
            {
                if(t.IsVisible)
                    t.Draw(spriteBatch);
            }
                        
            player.Draw(spriteBatch);

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
