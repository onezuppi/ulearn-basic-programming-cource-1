using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if ((a + b <= c && a + c <= b && b + c <= a) || a <= 0 || b <= 0 || c < 0)
                return Double.NaN;

            return Math.Acos((b * b + a * a - c * c) / (2 * b * a));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(-1, 1, 1, Double.NaN)]
        [TestCase(0, -1, -1, Double.NaN)]
        [TestCase(0, -1, 0, Double.NaN)]
        [TestCase(0, -1, 1, Double.NaN)]
        [TestCase(0, 0, -1, Double.NaN)]
        [TestCase(0, 0, 0, Double.NaN)]
        [TestCase(0, 0, 1, Double.NaN)]
        [TestCase(0, 1, -1, Double.NaN)]
        [TestCase(0, 1, 0, Double.NaN)]
        [TestCase(0, 1, 1, Double.NaN)]
        [TestCase(1, -1, -1, Double.NaN)]
        [TestCase(1, -1, 0, Double.NaN)]
        [TestCase(1, -1, 1, Double.NaN)]
        [TestCase(1, 0, -1, Double.NaN)]
        [TestCase(1, 0, 0, Double.NaN)]
        [TestCase(1, 0, 1, Double.NaN)]
        [TestCase(1, 1, -1, Double.NaN)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(1, 1, 1, 1.0471975511965979)]
        [TestCase(1, 2, 3, 3.141592653589793)]
        [TestCase(3, 4, 5, 1.5707963267948966)]
        [TestCase(5, 4, 3, 0.6435011087932843)]
        [TestCase(2, 3, 2, 0.7227342478134157)]
        [TestCase(3, 2, 2, 0.7227342478134157)]
        [TestCase(1, 1, 10, Double.NaN)]
        [TestCase(10, 1, 1, Double.NaN)]
        [TestCase(1, 10, 1, Double.NaN)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            Assert.AreEqual(expectedAngle, TriangleTask.GetABAngle(a, b, c), 1e-6);
        }
    }
}