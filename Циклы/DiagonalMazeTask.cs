using System.Text.RegularExpressions;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (!robot.Finished)
            {
                if (height > width)
                    MoveAcrossRoom(robot, width, height, Direction.Down, Direction.Right);
                else
                    MoveAcrossRoom(robot, height, width, Direction.Right, Direction.Down);
            }
        }

        private static void Move(Robot robot, Direction direction, int steps)
        {
            for (int step = 0; step < steps; step++)
                robot.MoveTo(direction);
        }

        private static void MoveAcrossRoom(Robot robot, int width, int height, Direction firstDirection,
            Direction secondDirection)
        {
            Move(robot, firstDirection, (height - 2) / (width - 2));
            if (!robot.Finished)
                Move(robot, secondDirection, (height - 2) % (width - 2));
        }
    }
}