namespace Mazes
{
	public static class PyramidMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			var step = 2;
			while (true)
			{
				Move(robot, Direction.Right, width - step - robot.X);
				Move(robot, Direction.Up, 2);
				Move(robot, Direction.Left, robot.X - step - 1);
				if (robot.Finished)
					break;
				Move(robot, Direction.Up, 2);
				step += 2;
			}
		}

		private static void Move(Robot robot, Direction direction, int steps)
		{
			for (var step = 0; step < steps; step++)
				robot.MoveTo(direction);
		}
	}
}