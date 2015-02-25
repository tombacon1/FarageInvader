using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceShooter
{
    class Circle
    {
        private float _x;
        private float _y;
        private float _r;

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float Radius
        {
            get { return _r; }
        }

        public Circle(float centroidx, float centroidy, float radius)
        {
            _x = centroidx;
            _y = centroidy;
            _r = radius;
        }
    }
}
