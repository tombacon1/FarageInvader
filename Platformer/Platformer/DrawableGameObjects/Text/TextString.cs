using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using IndependentResolutionRendering;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter
{
    class TextSprite
    {
        private SpriteFont _spriteFont;
        protected Vector2 _position;
        private string _text;
        private Color _color;
        private float _rotation = MathExtension.DegToRad(0);
        private Vector2 _origin;
        private Vector2 _scale;
        public bool Destroy = false;
        private float _startMilliseconds;
        private float _lengthMilliseconds;
        private string _name;
        bool _isVisible;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public  Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public TextSprite(string name, SpriteFont spriteFont, string text, Color color, Vector2 position, Vector2 scale, float lengthMilliseconds, float startMilliseconds, bool isVisible = true)
        {
            _spriteFont = spriteFont;
            _text = text;
            _color = color;
            _position = position;
            _scale = scale;
            _lengthMilliseconds = lengthMilliseconds;
            _startMilliseconds = startMilliseconds;
            _name = name;
            _isVisible = isVisible;
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            _origin = _spriteFont.MeasureString(_text) / 2;

            switch (_name)
            {
                case "score":
                    _text = "Score: " + player.Score;
                    _position.X = _spriteFont.MeasureString(_text).X + 10;
                    break;
                case "missilecount":
                    _text = "Missiles: " + Missile.Count;
                    _position.X = Resolution.VirtualWidth - 10 - _spriteFont.MeasureString(_text).X;
                    break;
                case "gameover":
                    _text = "Game Over";
                    _position.X = Resolution.VirtualWidth / 2 - (_spriteFont.MeasureString(_text).X / 2);
                    _position.Y = Resolution.VirtualHeight / 2 - (_spriteFont.MeasureString(_text).Y / 2);
                    break;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds - _startMilliseconds > _lengthMilliseconds)
                Destroy = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _text, Position, _color, _rotation, _origin, _scale, SpriteEffects.None, 0f);
        }
    }
}