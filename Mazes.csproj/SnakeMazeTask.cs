namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                MoveSnake(robot, width, Direction.Right);
                MoveSnake(robot, 5, Direction.Down);
                MoveSnake(robot, width, Direction.Left);
                if (!robot.Finished)
                    MoveSnake(robot, 5, Direction.Down);
            }
        }

        public static void MoveSnake(Robot robot, int width, Direction direction)
        {
            for (int i = 0; i < width - 3; i++)
                robot.MoveTo(direction);
        }
    }
}