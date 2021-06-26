using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var angle = shoulder;
            var elbowPos = GetPoint(new PointF {X = 0, Y = 0}, Manipulator.UpperArm, angle);
            angle = GetNewAngle(elbow, angle);
            var wristPos = GetPoint(elbowPos, Manipulator.Forearm, angle);
            angle = GetNewAngle(wrist, angle);
            var palmEndPos = GetPoint(wristPos, Manipulator.Palm, angle);
            return new[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static PointF GetPoint(PointF start, double length, double angle)
        {
            return new PointF(start.X + (float) (length * Math.Cos(angle)),
                start.Y + (float) (length * Math.Sin(angle)));
        }

        private static double GetNewAngle(double currentAngle, double oldAngle)
        {
            return currentAngle - Math.PI + oldAngle;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, 120, 90)]
        [TestCase(Math.PI / 2, Math.PI / 2, -Math.PI / 2, 120, 210)]
        [TestCase(Math.PI / 2, Math.PI / 2, 0, 60, 150)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 180, 150)]
        [TestCase(Math.PI / 2, -Math.PI / 2, Math.PI / 2, -120, 210)]
        [TestCase(Math.PI / 2, -Math.PI / 2, -Math.PI / 2, -120, 90)]
        [TestCase(Math.PI / 2, -Math.PI / 2, 0, -60, 150)]
        [TestCase(Math.PI / 2, -Math.PI / 2, Math.PI, -180, 150)]
        [TestCase(Math.PI / 2, 0, Math.PI / 2, -60, 30)]
        [TestCase(Math.PI / 2, 0, -Math.PI / 2, 60, 30)]
        [TestCase(Math.PI / 2, 0, 0, 5.510729E-15, 90)]
        [TestCase(Math.PI / 2, 0, Math.PI, 2.0206E-14, -30)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI / 2, 60, 270)]
        [TestCase(Math.PI / 2, Math.PI, -Math.PI / 2, -60, 270)]
        [TestCase(Math.PI / 2, Math.PI, 0, 2.0206E-14, 210)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 2.0206E-14, 330)]
        [TestCase(-Math.PI / 2, Math.PI / 2, Math.PI / 2, -120, -90)]
        [TestCase(-Math.PI / 2, Math.PI / 2, -Math.PI / 2, -120, -210)]
        [TestCase(-Math.PI / 2, Math.PI / 2, 0, -60, -150)]
        [TestCase(-Math.PI / 2, Math.PI / 2, Math.PI, -180, -150)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, Math.PI / 2, 120, -210)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, -Math.PI / 2, 120, -90)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, 0, 60, -150)]
        [TestCase(-Math.PI / 2, -Math.PI / 2, Math.PI, 180, -150)]
        [TestCase(-Math.PI / 2, 0, Math.PI / 2, 60, -30)]
        [TestCase(-Math.PI / 2, 0, -Math.PI / 2, -60, -30)]
        [TestCase(-Math.PI / 2, 0, 0, 5.510729E-15, -90)]
        [TestCase(-Math.PI / 2, 0, Math.PI, -2.387982E-14, 30)]
        [TestCase(-Math.PI / 2, Math.PI, Math.PI / 2, -60, -270)]
        [TestCase(-Math.PI / 2, Math.PI, -Math.PI / 2, 60, -270)]
        [TestCase(-Math.PI / 2, Math.PI, 0, 5.510729E-15, -210)]
        [TestCase(-Math.PI / 2, Math.PI, Math.PI, 2.0206E-14, -330)]
        [TestCase(0, Math.PI / 2, Math.PI / 2, 90, -120)]
        [TestCase(0, Math.PI / 2, -Math.PI / 2, 210, -120)]
        [TestCase(0, Math.PI / 2, 0, 150, -60)]
        [TestCase(0, Math.PI / 2, Math.PI, 150, -180)]
        [TestCase(0, -Math.PI / 2, Math.PI / 2, 210, 120)]
        [TestCase(0, -Math.PI / 2, -Math.PI / 2, 90, 120)]
        [TestCase(0, -Math.PI / 2, 0, 150, 60)]
        [TestCase(0, -Math.PI / 2, Math.PI, 150, 180)]
        [TestCase(0, 0, Math.PI / 2, 30, 60)]
        [TestCase(0, 0, -Math.PI / 2, 30, -60)]
        [TestCase(0, 0, 0, 90, 0)]
        [TestCase(0, 0, Math.PI, -30, -2.204291E-14)]
        [TestCase(0, Math.PI, Math.PI / 2, 270, -60)]
        [TestCase(0, Math.PI, -Math.PI / 2, 270, 60)]
        [TestCase(0, Math.PI, 0, 210, -7.347638E-15)]
        [TestCase(0, Math.PI, Math.PI, 330, 0)]
        [TestCase(Math.PI, Math.PI / 2, Math.PI / 2, -90, 120)]
        [TestCase(Math.PI, Math.PI / 2, -Math.PI / 2, -210, 120)]
        [TestCase(Math.PI, Math.PI / 2, 0, -150, 60)]
        [TestCase(Math.PI, Math.PI / 2, Math.PI, -150, 180)]
        [TestCase(Math.PI, -Math.PI / 2, Math.PI / 2, -210, -120)]
        [TestCase(Math.PI, -Math.PI / 2, -Math.PI / 2, -90, -120)]
        [TestCase(Math.PI, -Math.PI / 2, 0, -150, -60)]
        [TestCase(Math.PI, -Math.PI / 2, Math.PI, -150, -180)]
        [TestCase(Math.PI, 0, Math.PI / 2, -30, -60)]
        [TestCase(Math.PI, 0, -Math.PI / 2, -30, 60)]
        [TestCase(Math.PI, 0, 0, -90, 1.102146E-14)]
        [TestCase(Math.PI, 0, Math.PI, 30, 1.83691E-14)]
        [TestCase(Math.PI, Math.PI, Math.PI / 2, -270, 60)]
        [TestCase(Math.PI, Math.PI, -Math.PI / 2, -270, -60)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX,
            double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.UpperArm, CalculateDistance(new PointF {X = 0, Y = 0}, joints[0]), 1e-5,
                "Shoulder");
            Assert.AreEqual(Manipulator.Forearm, CalculateDistance(joints[0], joints[1]), 1e-5, "Elbow");
            Assert.AreEqual(Manipulator.Palm, CalculateDistance(joints[1], joints[2]), 1e-5, "Wrist");
        }

        private static double CalculateDistance(PointF a, PointF b)
        {
            var dx = b.X - a.X;
            var dy = b.Y - a.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}