namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            Move(robot, width - 3, Direction.Right);
            Move(robot, height - 3, Direction.Down);
        }

        public static void Move(Robot robot, int stepCounts, Direction direction)
        {
            for (int i = 0; i < stepCounts; i++)
                robot.MoveTo(direction);
        }
    }
}