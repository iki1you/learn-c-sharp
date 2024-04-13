using System;

namespace Mazes
{
	public static class PyramidMazeTask
	{
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveDown(robot, 2);
        }
                
        public static void MoveRight(Robot robot, int stepCounts)
        {
            for (int i = 0; i < stepCounts; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveDown(Robot robot, int stepCounts)
        {
            for (int i = 0; i < stepCounts; i++)
                robot.MoveTo(Direction.Down);
        }
    }
}