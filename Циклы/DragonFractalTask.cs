using System;

namespace Fractals
	{
		internal static class DragonFractalTask
		{
			private const double Deg45 = Math.PI / 4;
			private const double Deg135 = Math.PI / 4 * 3;
			
			public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
			{
				var startX = 1.0;
				var startY = 0.0;
				var (nextX, nextY) = (startX, startY);

				var random = new Random(seed);

				for (int iteration = 0; iteration < iterationsCount; iteration++)
				{
					if (random.Next(0, 2) == 1)
					{
						(nextX, nextY) = CalculateNextPoint(startX, startY, Deg45);
					}
					else
					{
						(nextX, nextY) = CalculateNextPoint(startX, startY, Deg135);
						nextX += 1.0;
					}
					(startX, startY) = (nextX, nextY);
					pixels.SetPixel(startX, startY);
				}
			}

			private static (double x, double y) CalculateNextPoint(double x, double y, double deg)
			{
				var newX = (x * Math.Cos(deg) - y * Math.Sin(deg)) / Math.Sqrt(2);
				var newY = (x * Math.Sin(deg) + y * Math.Cos(deg)) / Math.Sqrt(2);
				return (newX, newY);
			}
		}
	}