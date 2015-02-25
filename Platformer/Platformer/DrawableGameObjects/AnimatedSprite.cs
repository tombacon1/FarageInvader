using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    class AnimatedSprite
    {
        Texture2D _spriteTexture;
        float _timer = 0f;
        float _interval;
        int _sheetRows;
        int _sheetColumns;
        int _spriteHeight;
        int _spriteWidth;
        int _spriteSpeed;
        int _currentFrame = 0;
        Rectangle _sourceRect;
        Vector2 _position;
        Vector2 _origin;

        public Texture2D SpriteTexture { get { return _spriteTexture; } set { _spriteTexture = value; } }
        public float Timer { get { return _timer; } set { _timer = value; } }
        public float Interval { get { return _interval; } set { _interval = value; } }
        public int SheetRows { get { return _sheetRows; } set { _sheetRows = value; } }
        public int SheetColumns { get { return _sheetColumns; } set { _sheetColumns = value; } }
        public int SpriteHeight { get { return _spriteHeight; } set { _spriteHeight = value; } }
        public int SpriteWidth { get { return _spriteWidth; } set { _spriteWidth = value; } }
        public int SpriteSpeed { get { return _spriteSpeed; } set { _spriteSpeed = value; } }
        public int CurrentFrame { get { return _currentFrame; } set { _currentFrame = value; } }
        public Rectangle SourceRect { get { return _sourceRect; } set { _sourceRect = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }
        public Vector2 Origin { get { return _origin; } set { _origin = value; } }

        public AnimatedSprite(Texture2D texture, int currentFrame, int spriteHeight, int spriteWidth, int spriteSpeed, float updateIntervalMilliSecs, Vector2 position)
        {
            _spriteTexture = texture;
            _currentFrame = currentFrame;
            _spriteHeight = spriteHeight;
            _spriteWidth = spriteWidth;
            _spriteSpeed = spriteSpeed;
            _position = position;
            _sheetRows = texture.Height / spriteHeight;
            _sheetColumns = texture.Width / spriteWidth;
            _interval = updateIntervalMilliSecs;
        }

        public void Update(GameTime gameTime)
        {
            _sourceRect = new Rectangle((_currentFrame % _sheetColumns) * _spriteWidth, _spriteHeight * (_currentFrame / _sheetColumns), _spriteWidth, _spriteHeight);
            _origin = new Vector2(_sourceRect.Width / 2, _sourceRect.Height / 2);

            // increment current frame if time > interval
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timer > Interval)
            {
                _currentFrame = (++_currentFrame % (_sheetRows * _sheetColumns));
                _timer = 0f;
            }
        }

        public void Draw()
        {

        }
    }
}
