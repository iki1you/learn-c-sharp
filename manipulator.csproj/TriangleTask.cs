using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if ((a <= 0 || b <= 0 || c < 0) || (a + b < c || a + c < b || b + c < a))
                return double.NaN;
            if (c == 0) 
                return 0;
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(-1, 1, 1, double.NaN)]
        [TestCase(1, -1, 1, double.NaN)]
        [TestCase(1, 2, 4, double.NaN)]
        [TestCase(4, 2, 1, double.NaN)]
        [TestCase(1, 4, 2, double.NaN)]
        [TestCase(12, 9, 15, Math.PI / 2)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var angle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(angle, expectedAngle, 1e-5);
        }
    }
}