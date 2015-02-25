using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IndependentResolutionRendering;

namespace SpaceShooter
{
    class Projectile : Sprite
    {
        protected float _velocity = 0.5f;
        protected Vector2 _direction;
        private bool offscreen = false;

        public static TimeSpan lastFiredTime = new TimeSpan(0, 0, 0, 0, 0);

        public Projectile(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, Vector2 direction)
            : base(texture, spriteHeight, spriteWidth, position)
        {
            direction.Normalize();
            _direction = direction;
        }

        public bool Offscreen
        {
            get { return offscreen; }
        }

        public override void Update(GameTime gameTime)
        {
            this.Position += _direction * _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Position.X < -16 || this.Position.Y < -16 || this.Position.X > Resolution.VirtualWidth + 16 || this.Position.Y > Resolution.VirtualHeight + 16)
                offscreen = true;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteTexture, this.Position, this.SourceRect, Color.White, MathExtension.Vec2toRad(_direction), this.Origin, _scale, SpriteEffects.None, 0f);
        }

    }
}
