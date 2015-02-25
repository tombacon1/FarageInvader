using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IndependentResolutionRendering;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class PlayerShip : Ship
    {
        const float _ACCELERATION = 0.02f;
        const float _ROTATIONACCELERATION = 0.005f;
        const float _MAXROTATIONSPEED = 0.3f;
        const float _MAXSPEED = 0.3f;
        const float _FRICTION = 0.0005f;

        private float _rotationSpeed = 0f;

        public PlayerShip(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position, float scale = 1f) 
            : base(texture, spriteHeight, spriteWidth, position, scale)
        { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyState = Keyboard.GetState();

            // read keyboard state and adjust velocity and rotation accordingly
            if (keyState.IsKeyDown(Keys.Left))
            {
                _rotationSpeed += _ROTATIONACCELERATION;
                _rotation -= Math.Min(_rotationSpeed, _MAXROTATIONSPEED);
                isRotating = true;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                _rotationSpeed += _ROTATIONACCELERATION;
                _rotation += Math.Min(_rotationSpeed, _MAXROTATIONSPEED);
                isRotating = true;
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                _velocity = Math.Min(_velocity + (_ACCELERATION * deltaTime), _MAXSPEED);
                isAccelerating = true;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                _velocity = Math.Max(_velocity - (_ACCELERATION * deltaTime), -_MAXSPEED);
                isAccelerating = true;
            }
            
            // stop rotating if not turning
            if (!isRotating)
                _rotationSpeed = 0;

            // update the ships position
            MoveShip(deltaTime);
            
            // keep ship within display bounds
            Vector2 pos;
            pos.X = MathHelper.Clamp(Position.X, SpriteTexture.Width / 2, (float)Resolution.VirtualWidth - SpriteTexture.Width / 2);
            pos.Y = MathHelper.Clamp(Position.Y, SpriteTexture.Height / 2, (float)Resolution.VirtualHeight - SpriteTexture.Height / 2);
            Position = pos;

            // apply friction if not accelerating
            if (!isAccelerating)
            {
                if (_velocity > 0)
                    _velocity = MathHelper.Clamp(_velocity - (_FRICTION * deltaTime), 0f, _MAXSPEED);
                else
                    _velocity = MathHelper.Clamp(_velocity + (_FRICTION * deltaTime), -_MAXSPEED, 0f);
            }
        }
    }
}
