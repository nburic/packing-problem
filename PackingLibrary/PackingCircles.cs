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

        static int r;
        static int d;
        static int x;
        static int y;

        public static List<Circle> Calculate(int radius, int circleDistance, int borderDistance, int width, int height)
        {
            r = radius;
            d = circleDistance;
            x = width - borderDistance * 2;
            y = height - borderDistance * 2;

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

            Console.WriteLine(circlesCount);

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

            Console.WriteLine("---------------------------------------------");
            foreach (Circle c in row)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();

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

            Console.WriteLine("---------------------------------------------");
            foreach (Circle c in row)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            return row;
        }

        private static Circle GetNextCircle(Circle prevCircle, double yCoord = 0, bool checkSpace = true)
        {
            if (prevCircle == null)
            {
                Circle c;

                if (yCoord == 0)
                {
                    c = new Circle(r, d, r, r);
                }
                else
                {
                    c = new Circle(r, d, r, yCoord);
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
                    prevCircle.x + prevCircle.r + d + r,
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

        private static bool IsSpace(Circle circle)
        {
            double right = circle.x + circle.r;
            double top = circle.y + circle.r;

            if (right > x || top > y)
            {
                return false;
            }
            return true;
        }

        private static double GetTriangleHeight(double distance)
        {
            return distance * Math.Sqrt(3) / 2;
        }

        private static Circle CalculateTopCircle(Circle firstCircle, bool checkSpace = true)
        {
            Circle secondCircle = GetNextCircle(firstCircle, 0, false);

            if (!secondCircle.initialized) return null;

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


        public static bool StartsWithUpper(this String str)
        {
            Console.WriteLine("Checking if starts with upper letter...");

            if (String.IsNullOrWhiteSpace(str))
                return false;

            Char ch = str[0];
            return Char.IsUpper(ch);

        }


    }
}
