using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
		/// <param name="v">Начальная скорость</param>
		/// <param name="distance">Расстояние до цели</param>
		/// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>
		public static double FindSightAngle(double v, double distance)
		{
			return Math.Asin((distance * 9.8) / (v * v)) / 2;
		} 
	}
}