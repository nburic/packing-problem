using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using PackingLibrary;

namespace PackingLibraryTest
{
    [TestClass]
    public class PackingUnitTest
    {
        [TestMethod]
        public void TestGetNextCircleFirstRowFirstCircleHasSpace()
        {
            PackingCircles.radius = 10;
            PackingCircles.distanceBetweenCircles = 0;
            PackingCircles.width = 500;
            PackingCircles.height = 400;
            PackingCircles.borderDistance = 50;

            PackingCircles.Circle expected = new PackingCircles.Circle(
                PackingCircles.radius = 10,
                PackingCircles.distanceBetweenCircles = 0,
                new PointF(60, 60));

            PackingCircles.Circle result = PackingCircles.GetNextCircle(null);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetNextCircleFirstRowFirstCircleNoSpace()
        {
            PackingCircles.radius = 380;
            PackingCircles.distanceBetweenCircles = 0;
            PackingCircles.width = 500;
            PackingCircles.height = 400;
            PackingCircles.borderDistance = 50;

            PackingCircles.Circle result = PackingCircles.GetNextCircle(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestGetNextCircleFirstCircleHasSpace()
        {
            PackingCircles.radius = 10;
            PackingCircles.distanceBetweenCircles = 0;
            PackingCircles.width = 500;
            PackingCircles.height = 400;
            PackingCircles.borderDistance = 50;

            const float yCoord = 120;

            PackingCircles.Circle expected = new PackingCircles.Circle(
                PackingCircles.radius = 10,
                PackingCircles.distanceBetweenCircles = 0,
                new PointF(60, yCoord));

            PackingCircles.Circle result = PackingCircles.GetNextCircle(null, yCoord);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestIsSpaceTrue()
        {
            int[] radius = { 10, 0, 5, 100, 20, 40, 50 };
            int x = 500;
            int y = 300;

            PackingCircles.borderDistance = 50;

            PointF[] circleCenter = { 
                new PointF(60, 60), 
                new PointF(450, 250), 
                new PointF(445, 65), 
                new PointF(150, 150), 
                new PointF(430, 230),
                new PointF(410, 210), 
                new PointF(400, 200)
            };

            for (int i = 0; i < radius.Length; i++)
            {
                PackingCircles.radius = radius[i];
                PackingCircles.distanceBetweenCircles = 0;
                PackingCircles.width = x;
                PackingCircles.height = y;

                PackingCircles.Circle circle = new PackingCircles.Circle(
                    PackingCircles.radius,
                    PackingCircles.distanceBetweenCircles,
                    circleCenter[i]);

                bool result = PackingCircles.IsSpace(circle);

                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void TestIsSpaceFalse()
        {
            int[] radius = { 10, 0, 5, 100, 20, 40, 50 };
            int x = 500;
            int y = 300;
            PointF[] circleCenter = {
                new PointF(0, 0),
                new PointF(501, 301),
                new PointF(496, 15),
                new PointF(120, 220),
                new PointF(480, 300),
                new PointF(460, 261),
                new PointF(510, 250)
            };

            for (int i = 0; i < radius.Length; i++)
            {
                PackingCircles.radius = radius[i];
                PackingCircles.distanceBetweenCircles = 0;
                PackingCircles.width = x;
                PackingCircles.height = y;

                PackingCircles.Circle circle = new PackingCircles.Circle(
                    PackingCircles.radius,
                    PackingCircles.distanceBetweenCircles,
                    circleCenter[i]);

                bool result = PackingCircles.IsSpace(circle);

                Assert.IsFalse(result);
            }

        }

        [TestMethod]
        public void TestGetTriangleHeightInt()
        {
            int distanceInt = 10;

            double result = PackingCircles.GetTriangleHeight(distanceInt);

            Assert.IsNotNull(result);
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestGetTriangleHeightFloat()
        {
            float distanceFloat = 10.60F;

            double result = PackingCircles.GetTriangleHeight(distanceFloat);

            Assert.IsNotNull(result);
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void TestGetTriangleHeightDoubleNegativeAndZero()
        {
            double negativeDistance = -10;
            double zeroDistance = 0;

            double resultNegative = PackingCircles.GetTriangleHeight(negativeDistance);
            double resultZero = PackingCircles.GetTriangleHeight(zeroDistance);

            Assert.IsNotNull(resultNegative);
            Assert.IsNotNull(resultZero);

            Assert.AreEqual(0, resultNegative);
            Assert.AreEqual(0, resultZero);
        }
    }
}
