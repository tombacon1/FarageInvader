using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpaceShooter
{
    class MathExtension
    {
        public static Vector2 RadToVec2(float radians)
        {
            Vector2 angleVector = new Vector2((float)Math.Cos(radians), -(float)Math.Sin(radians));

            return angleVector;
        }

        public static float Vec2toRad(Vector2 vector)
        {
            float angleFromVector = (float)Math.Atan2(vector.X, -vector.Y);

            return angleFromVector;
        }

        public static float RadToDeg(float radians)
        {
            return (float)((radians * 180) / Math.PI);
        }

        public static float DegToRad(float degrees)
        {
            return (float)((degrees * Math.PI) / 180);
        }

        public static bool CheckCollision(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }

        public static bool CheckCollision(Rectangle rect1, Circle circle1)
        {
            Vector2 circleDistance;
            circleDistance.X = Math.Abs(circle1.X - rect1.X);
            circleDistance.Y = Math.Abs(circle1.Y - rect1.Y);

            if (circleDistance.X > (rect1.Width / 2 + circle1.Radius)) { return false; }
            if (circleDistance.Y > (rect1.Height / 2 + circle1.Radius)) { return false; }

            if (circleDistance.X <= (rect1.Width / 2)) { return true; }
            if (circleDistance.Y <= (rect1.Height / 2)) { return true; }

            float cornerDistance_sq = (float)Math.Pow((circleDistance.X - rect1.Width / 2), 2) +
                                      (float)Math.Pow((circleDistance.Y - rect1.Height / 2), 2);

            return (cornerDistance_sq <= (Math.Pow(circle1.Radius,2)));
        }

        public static bool CheckCollision(Vector2 pos1, float radius1, Vector2 pos2, float radius2)
        {
            if (Distance(pos1, pos2) >= radius1 + radius2)
                return true;

            return false;
        }

        public static float Distance(Vector2 pos1, Vector2 pos2)
        {
            float xDif = pos1.X - pos2.X;
            float yDif = pos1.Y - pos2.Y;

            return (float)Math.Sqrt(Math.Pow(xDif,2) + Math.Pow(yDif,2));
        }

    }
}
