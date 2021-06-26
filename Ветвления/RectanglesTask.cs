using System;
using System.Collections.Generic;

namespace Rectangles
{
	public static class RectanglesTask
	{
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			return (r1.Right >= r2.Left) && (r1.Bottom >= r2.Top) && (r1.Left <= r2.Right) && (r1.Top <= r2.Bottom); 
		}

		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			var left = Math.Max(r1.Left, r2.Left);
			var top = Math.Max(r1.Top, r2.Top);
			var right = Math.Min(r1.Right, r2.Right);
			var bottom = Math.Min(r1.Bottom, r2.Bottom);

			var height = bottom - top;
			var width = right - left;

			if (height > 0 && width > 0)
				return width * height;

			return 0;
		}

		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if ((r1.Left <= r2.Left) && (r1.Top <= r2.Top) && (r1.Right >= r2.Right) && (r1.Bottom >= r2.Bottom)) 
				return 1;

			if ((r2.Left <= r1.Left) && (r2.Top <= r1.Top) && (r2.Right >= r1.Right) && (r2.Bottom >= r1.Bottom))
				return 0;
                
			return -1;
		}
	}
}