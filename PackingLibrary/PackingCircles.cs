using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace PackingLibrary
{
    public static class PackingCircles
    {
        public class Circle
        {

            public int r;
            public int d;
            public double x;
            public double y;
            public bool initialized = false;

            public Circle() { }

            public Circle(int _r, int _d, double _x, double _y)
            {
                r = _r;
                d = _d;
                x = _x;
                y = _y;
                initialized = true;
            }

            public override string ToString()
            {
                return "Circle(" + x + ", " + y + ")";
            }
        }

        public static int r;
        public static int d;
        public static int x;
        public static int y;
        public static int bd;

        public static List<Circle> Calculate(int radius, int circleDistance, int borderDistance, int width, int height)
        {
            r = radius;
            d = circleDistance;
            x = width;
            y = height;
            bd = borderDistance;

            int rowCount = 0;
            int circlesCount = 0;
            List<Circle> allCircles = new List<Circle>();

            List<Circle> row = drawRow(0);

            if (row.Count > 0)
            {
                rowCount++;
                circlesCount += row.Count;
                allCircles.AddRange(row);
            }
            else
            {
                return allCircles;
            }

            bool isEmpty = row.Count == 0;

            List<Circle> prevRow = new List<Circle>();
            foreach (Circle c in row)
            {
                prevRow.Add(c);
            }

            while (!isEmpty)
            {
                row = drawRow(rowCount, prevRow);

                if (row.Count == 0)
                {
                    isEmpty = true;
                }
                else
                {
                    prevRow.Clear();
                    foreach (Circle c in row)
                    {
                        prevRow.Add(c);
                    }

                    rowCount++;
                    circlesCount += row.Count;
                    allCircles.AddRange(row);
                }
            }

            return allCircles;

        }

        private static List<Circle> drawRow(int count, List<Circle> prevRow = null)
        {

            if (count % 2 == 0)
            {
                return drawEvenRow(prevRow);
            }
            else
            {
                if (prevRow == null || prevRow.Count == 0)
                {
                    return new List<Circle>();
                }

                return drawOddRow(prevRow);
            }


        }

        private static List<Circle> drawEvenRow(List<Circle> prevRow)
        {
            List<Circle> row = new List<Circle>();

            Circle currentCircle = null;

            if (prevRow == null || prevRow.Count == 0)
            {
                currentCircle = GetNextCircle(null);
            }
            else
            {
                Circle topCircle = CalculateTopCircle(prevRow[0]);
                if (topCircle != null)
                {
                    currentCircle = GetNextCircle(null, topCircle.y);
                }
            }

            if (currentCircle != null)
            {
                row.Add(currentCircle);
            }

            while (currentCircle != null)
            {
                currentCircle = GetNextCircle(currentCircle);

                if (currentCircle != null)
                {
                    row.Add(currentCircle);
                }
            }

            return row;
        }

        private static List<Circle> drawOddRow(List<Circle> prevRow)
        {
            List<Circle> row = new List<Circle>();
            Circle firstCircle = prevRow[0];

            Circle currentCircle = CalculateTopCircle(firstCircle);
            if (currentCircle == null)
            {
                return new List<Circle>();
            }

            row.Add(currentCircle);

            while (currentCircle != null)
            {
                currentCircle = GetNextCircle(currentCircle);

                if (currentCircle != null)
                {
                    row.Add(currentCircle);
                }
            }

            return row;
        }

        public static Circle GetNextCircle(Circle prevCircle, double yCoord = 0, bool checkSpace = true)
        {
            if (prevCircle == null)
            {
                Circle c;

                if (yCoord == 0)
                {
                    c = new Circle(r, d, r + bd, r + bd);
                }
                else
                {
                    c = new Circle(r, d, r + bd, yCoord);
                }

                if (IsSpace(c))
                {
                    return c;
                }
                return null;
            }
            else
            {
                Circle c = new Circle(
                    prevCircle.r,
                    prevCircle.d,
                    prevCircle.x + prevCircle.r + prevCircle.d + prevCircle.r,
                    prevCircle.y
                    );

                if (!checkSpace)
                {
                    return c;
                }

                if (IsSpace(c))
                {
                    return c;
                }
                return null;
            }
        }

        public static bool IsSpace(Circle circle)
        {
            double right = circle.x + circle.r;
            double left = circle.x - circle.r;
            double top = circle.y + circle.r;
            double bottom = circle.y - circle.r;

            if (left < bd || right > x - bd || bottom < bd || top > y - bd)
            {
                return false;
            }
            return true;
        }

        public static double GetTriangleHeight(double distance)
        {
            return Math.Floor(distance * Math.Sqrt(3) / 2);
        }

        private static Circle CalculateTopCircle(Circle firstCircle, bool checkSpace = true)
        {
            Circle secondCircle = GetNextCircle(firstCircle, 0, false);

            if (secondCircle == null) return null;

            // v = a * sqrt(3) / 2

            double a = Math.Sqrt(Math.Pow(secondCircle.x - firstCircle.x, 2) + Math.Pow(secondCircle.y - firstCircle.y, 2));

            // (second.x - firstCircle.x).toDouble().pow(2.toDouble()) +
            // (second.y - firstCircle.y).toDouble().pow(2.toDouble()))

            //     println(a)

            double v = GetTriangleHeight(a);

            PointF midPoint = new PointF(
                Convert.ToSingle((firstCircle.x + secondCircle.x) / 2),
                Convert.ToSingle((firstCircle.y + secondCircle.y) / 2)
                );

            Circle currentCircle = new Circle(
                firstCircle.r,
                firstCircle.d,
                midPoint.X,
                midPoint.Y + v
                );

            if (!checkSpace) return currentCircle;

            if (IsSpace(currentCircle)) return currentCircle;
            else return null;
        }
    }
}
