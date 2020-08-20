using System;
using System.Collections.Generic;
using System.Drawing;

namespace PackingLibrary
{
    public static partial class PackingCircles
    {
        public static int radius;
        public static int distanceBetweenCircles;
        public static int width;
        public static int height;
        public static int borderDistance;

        public static List<Circle> Calculate(int radius, int distanceBetweenCircles, int borderDistance, int width, int height)
        {
            PackingCircles.radius = radius;
            PackingCircles.distanceBetweenCircles = distanceBetweenCircles;
            PackingCircles.width = width;
            PackingCircles.height = height;
            PackingCircles.borderDistance = borderDistance;

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
                    currentCircle = GetNextCircle(null, topCircle.coords.Y);
                }
            }

            if (currentCircle != null)
            {
                row.Add(currentCircle);
            }

            return AddCirclesToRow(row, currentCircle);
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

            return AddCirclesToRow(row, currentCircle);
        }

        private static List<Circle> AddCirclesToRow(List<Circle> row, Circle currentCircle)
        {
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

        public static Circle GetNextCircle(Circle prevCircle, float yCoord = 0, bool checkSpace = true)
        {
            // first circle in row
            if (prevCircle == null)
            {
                Circle c;
                switch(yCoord)
                {
                    case 0:
                        c = new Circle(radius, distanceBetweenCircles, new PointF(radius + borderDistance, radius + borderDistance));
                        break;
                    default:
                        c = new Circle(radius, distanceBetweenCircles, new PointF(radius + borderDistance, yCoord));
                        break;
                }

                if (IsSpace(c)) return c;

                return null;
            }
            else
            {
                Circle c = new Circle(
                    prevCircle.r,
                    prevCircle.d,
                    new PointF(prevCircle.coords.X + prevCircle.r + prevCircle.d + prevCircle.r, prevCircle.coords.Y));

                if (!checkSpace || IsSpace(c)) return c;

                return null;
            }
        }

        public static bool IsSpace(Circle circle)
        {
            double right = circle.coords.X + circle.r;
            double left = circle.coords.X - circle.r;
            double top = circle.coords.Y + circle.r;
            double bottom = circle.coords.Y - circle.r;

            if (left < borderDistance || right > width - borderDistance || bottom < borderDistance || top > height - borderDistance)
            {
                return false;
            }
            return true;
        }

        public static double GetTriangleHeight(double distance)
        {
            if (distance < 0) return 0;

            return Math.Floor(distance * Math.Sqrt(3) / 2);
        }

        private static Circle CalculateTopCircle(Circle firstCircle, bool checkSpace = true)
        {
            // get possible adjacent circle without checking for actual space
            Circle secondCircle = GetNextCircle(firstCircle, 0, false);

            if (secondCircle == null) return null;

            // side of the triangle
            // sqrt((x2 - x1)2 + (y2 - y1)2)
            double a = Math.Sqrt(Math.Pow(secondCircle.coords.X - firstCircle.coords.X, 2) + Math.Pow(secondCircle.coords.Y - firstCircle.coords.Y, 2));

            // v = a * sqrt(3) / 2
            float v = (float)GetTriangleHeight(a);

            PointF midPoint = new PointF(
                Convert.ToSingle((firstCircle.coords.X + secondCircle.coords.X) / 2),
                Convert.ToSingle((firstCircle.coords.Y + secondCircle.coords.Y) / 2)
                );

            Circle currentCircle = new Circle(
                firstCircle.r,
                firstCircle.d,
                new PointF(midPoint.X, midPoint.Y + v)
                );

            if (!checkSpace || IsSpace(currentCircle)) return currentCircle;

            return null;
        }
    }
}
