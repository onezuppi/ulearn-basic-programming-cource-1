using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var (width, height) = (original.GetLength(0), original.GetLength(1));
            var medianFilteredOriginal = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    medianFilteredOriginal[x, y] = CalculateMedian(original, x, y);
                }
            }
            return medianFilteredOriginal;
        }

        private static double CalculateMedian(double[,] original, int x, int y)
        {
            var (width, height) = (original.GetLength(0), original.GetLength(1));
            var medianClaims = new List<double>();
            for (var dx = -1; dx < 2; dx++)
            {
                for (var dy = -1; dy < 2; dy++)
                {
                    var (newX, newY)  = (x + dx, y + dy);
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                        medianClaims.Add(original[newX, newY]);
                }
            }
            return GetMedianValue(medianClaims);
        }

        private static double GetMedianValue(List<double> array)
        {
            array.Sort();
            var middle = array.Count / 2; 
            
            if (array.Count % 2 == 0)
                return (array[middle] + array[middle - 1]) / 2;

            return array[middle];
        }
    }
}