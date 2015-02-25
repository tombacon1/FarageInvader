using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IndependentResolutionRendering;

namespace SpaceShooter
{
    class Ship : Sprite
    {
        protected float _velocity;
        protected bool isRotating = false;
        protected bool isAccelerating = false;
        protected int deltaTime;

        public Circle CollisionCircle
        {
            get { return new Circle(Position.X, Position.Y, this.SpriteWidth); }
        }

        public float Rotation
        {
            get { return -_rotation + MathExtension.DegToRad(90); }
        }

        public float Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        protected Ship(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, float scale = 1f) 
            : base(texture, spriteHeight, spriteWidth, position, scale)
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            isRotating = false;
            isAccelerating = false;
        }

        public void MoveShip(int deltaTime)
        {
            Position += MathExtension.RadToVec2(-_rotation + MathExtension.DegToRad(90)) * _velocity * deltaTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteTexture, this.Position, this.SourceRect, Color.White, _rotation, this.Origin, _scale, SpriteEffects.None, 0f);
        }
    }
}
