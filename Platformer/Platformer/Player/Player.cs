using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Player
    {
        Texture2D healthBarTexture;
        Texture2D healthBarBackTexture;
        Rectangle healthBox;
        Rectangle healthBackground;

        int _score = 0;
        int _level = 1;
        int _health;
        int _maxHealth;
        bool _isDead = false;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        public Player(int health = 300, int score = 0, int level = 1)
        {
            _maxHealth = health;
            _health = health;
            _score = score;
            _level = level;
        }

        public void LoadContent(ContentManager content)
        {
            healthBarTexture = content.Load<Texture2D>(@"Sprites\healthbar");
            healthBarBackTexture = content.Load<Texture2D>(@"Sprites\healthbar_background");
        }

        public void Update(GameTime gameTime)
        {
            healthBox = new Rectangle(50, 50, this._health, 15);
            healthBackground = new Rectangle(50, 50, _maxHealth, 15);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarBackTexture, healthBackground, Color.White);
            spriteBatch.Draw(healthBarTexture, healthBox, Color.White);
        }
    }
}
