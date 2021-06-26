using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			var aC = CalculateSegmentLength(x, y,ax,  ay);
			var bC = CalculateSegmentLength(x, y, bx , by);
			var cC = CalculateSegmentLength(ax, ay, bx, by);
			
			var square = GetSquare(aC, bC, cC);
			var height = (2 * square) / cC;
			var longestSide = Math.Max(aC, bC);

			if ((longestSide * longestSide - height * height > cC * cC) 
			    || double.IsNaN(height))  
            {
				return Math.Min(bC, aC);
			}
			return Math.Min(bC, Math.Min(aC, height)); 
		}
		
		private static double CalculateSegmentLength(double x, double y, double bx, double by)
		{
			return  Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
		}
		
		private static double GetSquare(double aC, double bC, double cC)
		{
			var semiperimeter = (aC + bC + cC) / 2.0;
			var square = (semiperimeter - aC) * (semiperimeter - bC) * (semiperimeter - cC);
			return Math.Sqrt(semiperimeter * square);
		}
	}
}