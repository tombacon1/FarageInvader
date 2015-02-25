using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Sprite
    {
        protected Texture2D _spriteTexture;
        protected Vector2 _position;
        protected int _spriteHeight;
        protected int _spriteWidth;
        protected Rectangle _sourceRect;
        protected Vector2 _origin;
        protected float _scale;
        protected float _rotation = MathExtension.DegToRad(0);

        public Texture2D SpriteTexture { get { return _spriteTexture; } set { _spriteTexture = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }
        public int SpriteHeight { get { return _spriteHeight; } set { _spriteHeight = value; } }
        public int SpriteWidth { get { return _spriteWidth; } set { _spriteWidth = value; } }
        public Rectangle SourceRect { get { return _sourceRect; } set { _sourceRect = value; } }
        public Vector2 Origin { get { return _origin; } set { _origin = value; } }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X - (_spriteWidth * _scale) / 2),
                                     (int)(Position.Y - (_spriteHeight * _scale) / 2),
                                     (int)(SpriteWidth * _scale),
                                     (int)(SpriteHeight * _scale));
            }
        }

        public Sprite(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, float scale = 1f)
        {
            _spriteTexture = texture;
            _spriteHeight = spriteHeight;
            _spriteWidth = spriteWidth;
            _position = position;
            _scale = scale;
        }

        public virtual void Update(GameTime gameTime)
        {
            _sourceRect = new Rectangle(0,0,_spriteWidth, _spriteHeight);
            _origin = new Vector2(_sourceRect.Width / 2, _sourceRect.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, SourceRect, Color.White, _rotation, Origin, _scale, SpriteEffects.None, 0f);
        }
    }
}
