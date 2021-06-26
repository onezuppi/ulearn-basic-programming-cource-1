using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristPoint = (
                X: x - Manipulator.Palm * Math.Cos(-alpha),
                Y: y - Manipulator.Palm * Math.Sin(-alpha));
            var distanceToWrist = Math.Sqrt(wristPoint.X * wristPoint.X + wristPoint.Y * wristPoint.Y);
            var elbow = TriangleTask.GetABAngle(Manipulator.Forearm, Manipulator.UpperArm, distanceToWrist);
            var shoulder = Math.Atan2(wristPoint.Y, wristPoint.X) +
                           TriangleTask.GetABAngle(Manipulator.UpperArm, distanceToWrist, Manipulator.Forearm);
            return new[] {shoulder, elbow, -alpha - shoulder - elbow};
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        private int testCount = 100000;
        private Random generator = new Random();

        [Test]
        public void TestMoveManipulatorTo()
        {
            for (var i = 0; i < testCount; i++)
            {
                var x = GetRandom(Manipulator.Forearm + Manipulator.UpperArm + Manipulator.Palm);
                var y = GetRandom(Manipulator.Forearm + Manipulator.UpperArm + Manipulator.Palm);
                var alpha = GetRandom(Math.PI * 2);
                var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                var palmEndPos = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2])[2];
                var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
                var rmax = Manipulator.UpperArm + Manipulator.Forearm;
                var center = (X: Manipulator.Palm * Math.Cos(-alpha),
                    Y: Manipulator.Palm * Math.Sin(-alpha));
                var distance = CalculateDistant(center.X, center.Y, x, y);
                if (distance > rmax || distance < rmin)
                    (x, y) = (double.NaN, double.NaN);
                Assert.AreEqual(palmEndPos.X, x, 1e-3);
                Assert.AreEqual(palmEndPos.Y, y, 1e-3);
            }
        }
        
        private double GetRandom(double maxMin)
        {
            var sign = generator.Next(100) > 50 ? 1 : -1;
            return generator.NextDouble() * maxMin * sign;
        }

        private double CalculateDistant(double x1, double y1, double x2, double y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}