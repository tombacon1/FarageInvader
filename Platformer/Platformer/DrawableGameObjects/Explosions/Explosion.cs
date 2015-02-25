using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class Explosion : AnimatedSprite
    {
        public bool isEnded = false;

        public Explosion(Texture2D texture, int currentFrame, int spriteHeight, int spriteWidth, int spriteSpeed, float updateIntervalMilliSecs, Vector2 position) 
            : base(texture, currentFrame, spriteHeight, spriteWidth, spriteSpeed, updateIntervalMilliSecs, position)
        { }

        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentFrame > 0 && CurrentFrame % 15 == 0)
                isEnded = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteTexture, this.Position, this.SourceRect, Color.White, 0f, this.Origin, 1f , SpriteEffects.None, 0f);
        }
    }
}
