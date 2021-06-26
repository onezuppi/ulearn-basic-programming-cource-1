using System;

namespace ReadOnlyVectorTask 
{
    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;
		
		public ReadOnlyVector(double x, double y)
        {
            X = x;
            Y = y;
        }
		
		public ReadOnlyVector Add(ReadOnlyVector vector)
		{
			return new ReadOnlyVector(X + vector.X, Y + vector.Y);
		}
		
		public ReadOnlyVector WithX(double x)
		{
			return new ReadOnlyVector(x, Y);
		}
		
		public ReadOnlyVector WithY(double y)
		{
			return new ReadOnlyVector(X, y);
		}	
    }
}