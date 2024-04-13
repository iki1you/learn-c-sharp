using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowX = Manipulator.UpperArm * Math.Cos(shoulder);
            var elbowY = Manipulator.UpperArm * Math.Sin(shoulder);
            var elbowPos = new PointF((float) elbowX, (float) elbowY);

            var toWristAngle = shoulder + elbow - Math.PI;
            var wristX = elbowX + Manipulator.Forearm * Math.Cos(toWristAngle);
            var wristY = elbowY + Manipulator.Forearm * Math.Sin(toWristAngle);
            var wristPos = new PointF((float) wristX, (float) wristY);

            var toEndPosAngle =  wrist + toWristAngle - Math.PI;
            var endPosX = wristX + Manipulator.Palm * Math.Cos(toEndPosAngle);
            var endPosY = wristY + Manipulator.Palm * Math.Sin(toEndPosAngle);
            var palmEndPos = new PointF((float) endPosX, (float) endPosY);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI / 2, Manipulator.Palm, Manipulator.UpperArm + Manipulator.Forearm)]
        [TestCase(3 * Math.PI / 2, Math.PI / 2, 3 * Math.PI / 2, - Manipulator.Forearm, -Manipulator.Palm - Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]
        [TestCase(Math.PI, Math.PI, Math.PI, -Manipulator.UpperArm - Manipulator.Forearm - Manipulator.Palm, 0)]
        [TestCase(Math.PI / 6, 5 * Math.PI / 6, 5 * Math.PI / 6, 
            Manipulator.UpperArm * 0.866025403 + Manipulator.Forearm + Manipulator.Palm * 0.866025403, 
            Manipulator.UpperArm / 2 - Manipulator.Palm / 2)]  // 0.866025403 = cos(pi/6)
        [TestCase(0, 0, 0, Manipulator.UpperArm - Manipulator.Forearm + Manipulator.Palm , 0)]
        [TestCase(2 * Math.PI, 2 * Math.PI, 2 * Math.PI, Manipulator.UpperArm - Manipulator.Forearm + Manipulator.Palm, 0)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(GetLength(new PointF(0, 0), joints[0]), Manipulator.UpperArm, 1e-5, "UpperArm");
            Assert.AreEqual(GetLength(joints[0], joints[1]), Manipulator.Forearm, 1e-5, "Forearm");
            Assert.AreEqual(GetLength(joints[1], joints[2]), Manipulator.Palm, 1e-5, "Palm");
        }

        public double GetLength(PointF point1, PointF point2)
        {
            return Math.Sqrt(Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.Y - point2.Y), 2));
        }
    }
}