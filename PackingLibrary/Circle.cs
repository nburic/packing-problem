using System;
using System.Drawing;

namespace PackingLibrary
{
    public static partial class PackingCircles
    {
        public class Circle : Shape
        {
            public int r;
            public int d;

            public Circle(int _r, int _d, PointF _coords)
            {
                r = _r;
                d = _d;
                coords = _coords;
            }

            public override float Area()
            {
                return (float)Math.PI * (float)Math.Pow(r, 2);
            }

            public override bool Equals(object obj)
            {
                Circle circle = obj as Circle;

                return circle != null &&
                    circle.r == r &&
                    circle.d == d &&
                    circle.coords.X == coords.X &&
                    circle.coords.Y == coords.Y;                    
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(r, d, coords);
            }
        }
    }
}
