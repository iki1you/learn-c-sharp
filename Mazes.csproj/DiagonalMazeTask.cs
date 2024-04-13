using System;
using System.Net;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            if (height > width)
                MoveNext(robot, (height - 1), (width - 1), Direction.Down, Direction.Right);
            else
                MoveNext(robot, width, (height - 1), Direction.Right, Direction.Down);
        }

        public static void MoveNext(Robot robot, int side1, int side2, Direction dir1, Direction dir2)
        {
            for (int i = 0; i < side2 - 2; i++)
            {
                Move(robot, side1 / side2, dir1);
                Move(robot, 1, dir2);
            }
            Move(robot, side1 / side2, dir1);
        }


        public static void Move(Robot robot, int stepCounts, Direction direction)
        {
            for (int i = 0; i < stepCounts; i++)
                robot.MoveTo(direction);
        }
    }
}