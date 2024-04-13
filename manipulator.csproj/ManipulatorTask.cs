using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            double wristX = x + Math.Cos(Math.PI - alpha) * Manipulator.Palm;
            double wristY = y + Math.Sin(Math.PI - alpha) * Manipulator.Palm;
            var wristSide = Math.Sqrt(wristX * wristX + wristY * wristY);
            var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, wristSide);
            var angle = TriangleTask.GetABAngle(wristSide, Manipulator.UpperArm, Manipulator.Forearm);
            var shoulder = angle + Math.Atan2(wristY, wristX);
            var wrist = -alpha - shoulder - elbow;
            if (double.IsNaN(shoulder) || double.IsNaN(elbow) || double.IsNaN(wrist))
                return new[] { double.NaN, double.NaN, double.NaN };
            else
                return new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var x = rand.Next() + rand.NextDouble();
                var y = rand.Next() + rand.NextDouble();
                var alpha = rand.Next() + rand.NextDouble();
                var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                Assert.AreEqual(3, angles.Length);
                if (!double.IsNaN(angles[0]))
                {
                    var joints = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                    Assert.AreEqual(3, joints.Length);
                    Assert.AreEqual(joints[2].X, x, 1e-5);
                    Assert.AreEqual(joints[2].Y, y, 1e-5);
                }
            }
        }
    }
}