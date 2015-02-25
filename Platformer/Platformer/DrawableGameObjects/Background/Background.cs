using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IndependentResolutionRendering;

namespace SpaceShooter
{
    class Background : Sprite
    {
        public Background(Texture2D texture, int spriteHeight, int spriteWidth, Vector2 position) 
            : base(texture, spriteHeight, spriteWidth, position)
        {
            this.SourceRect = new Rectangle(0, 0, SpriteWidth, SpriteHeight);
            this.Origin = new Vector2(this.SourceRect.X / 2, this.SourceRect.Y / 2);
        }

        public new void Update(GameTime gameTime)
        { }
    }
}
