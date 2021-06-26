using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public Vector()
        {
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public Segment()
        {
        }

        public Segment(Vector begin, Vector end)
        {
            Begin = begin;
            End = end;
        }

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(new Vector(segment.End.X - segment.Begin.X, segment.End.Y - segment.Begin.Y));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var serment1 = new Segment(segment.Begin, vector);
            var segment2 = new Segment(vector, segment.End);
            return Math.Abs(segment.GetLength() - serment1.GetLength() - segment2.GetLength()) < 1e-6;
        }
    }
}