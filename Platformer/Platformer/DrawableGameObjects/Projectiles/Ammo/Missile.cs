using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Missile : Projectile
    {
        EnemyShip _target;
        private const float _MAXSPEED = 0.8f;
        private const float _MAXSTEER = 0.0015f;
        private float _acceleration = 0.001f;
        private static int _count = 500;

        public static int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public Missile(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, Vector2 direction, EnemyShip target)
            : base(texture, spriteHeight, spriteWidth, position, direction)
        {
            _target = target;
            _velocity = 0.001f;
        }

        public override void Update(GameTime gameTime)
        {
            int deltaTime = gameTime.ElapsedGameTime.Milliseconds;

            Vector2 targetDirection = _target.Position - this.Position;
            targetDirection.Normalize();
            _direction.Normalize();
            float difx = MathHelper.Clamp(targetDirection.X - _direction.X, -_MAXSTEER, _MAXSTEER);
            float dify = MathHelper.Clamp(targetDirection.Y - _direction.Y, -_MAXSTEER, _MAXSTEER);

            _direction = new Vector2(_direction.X + (difx * deltaTime), _direction.Y + (dify * deltaTime));
            _direction.Normalize();
            _acceleration *= 1.05f;
            _velocity = MathHelper.Clamp(_velocity + (_acceleration * deltaTime), 0f, _MAXSPEED);
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
