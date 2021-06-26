namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            while (!robot.Finished)
            {
				Move(robot, Direction.Right, width - robot.X - 2);
				Move(robot, Direction.Down, 2);
				Move(robot, Direction.Left, robot.X - 1);
				if (!robot.Finished)
					Move(robot, Direction.Down, 2);
			}
		}
		
		private static void Move(Robot robot, Direction direction, int steps)
		{
			for (int step = 0; step < steps; step++)
				robot.MoveTo(direction);
		}
	}
}