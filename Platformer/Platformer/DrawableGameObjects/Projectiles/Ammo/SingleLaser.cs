using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooter
{
    class SingleLaser : Projectile
    {
        public static int FireInterval = 100;

        public SingleLaser(Texture2D laserTexture, int spriteHeight, int spriteWidth, Vector2 position, Vector2 direction) 
            : base(laserTexture, spriteHeight, spriteWidth, position, direction)
        {
            // TODO: Complete member initialization
        }

    }
}
