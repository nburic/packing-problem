using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PackingLibrary
{
    public abstract class Shape
    {
        public PointF coords;

        public abstract float Area();

        public override string ToString()
        {
            return coords.ToString();
        }

        public override bool Equals(object obj)
        {
            Shape shape = obj as Shape;

            return shape != null &&
                shape.coords.X == coords.X &&
                shape.coords.Y == coords.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(coords);
        }
    }
}
