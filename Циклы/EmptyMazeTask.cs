namespace Mazes
{
	public static class EmptyMazeTask
	{	
		public static void MoveOut(Robot robot, int width, int height)
		{
			Move(robot, Direction.Right, width - robot.X - 2);
			Move(robot, Direction.Down, height - robot.Y - 2);
		}
		
		private static void Move(Robot robot, Direction direction, int steps)
		{
			for (int step = 0; step < steps; step++)
				robot.MoveTo(direction);
		}
	}
}