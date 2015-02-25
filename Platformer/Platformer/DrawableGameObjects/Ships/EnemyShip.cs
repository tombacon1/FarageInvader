using System;
using IndependentResolutionRendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class EnemyShip : Ship
    {
        public int HitCount = 0;
        public bool Dead = false;
        
        private new float _velocity = 0.5f;
        private Vector2 _direction = new Vector2(1f, 1f);
        private static Random rnd = new Random(DateTime.Now.Millisecond);

        public EnemyShip(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, float scale = 1f) 
            : base(texture, spriteHeight, spriteWidth, position, scale)
        {
            _velocity = (float)(_velocity * rnd.NextDouble());
        }

        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (HitCount >= 50)
                Dead = true;

            if (Position.X <= ((_spriteWidth / 2) * Scale) || Position.X > (Resolution.VirtualWidth - ((_spriteWidth / 2) * Scale)))
                _direction.X = -_direction.X;
            if (Position.Y <= ((_spriteHeight / 2) * Scale) || Position.Y > (Resolution.VirtualHeight - ((_spriteHeight / 2) * Scale)))
                _direction.Y = -_direction.Y;

            Position = Position + (_direction * _velocity * gameTime.ElapsedGameTime.Milliseconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteTexture, this.Position, this.SourceRect, Color.White, _rotation, this.Origin, Scale, SpriteEffects.None, 0f);
        }

        internal static Vector2 GetRandomPosition(int width, int height)
        {
            Vector2 pos = new Vector2();

            pos.X = rnd.Next(width / 2, Resolution.VirtualWidth - width / 2);
            pos.Y = rnd.Next(height / 2, Resolution.VirtualHeight - height / 2);
            
            return pos;
        }
    }
}
