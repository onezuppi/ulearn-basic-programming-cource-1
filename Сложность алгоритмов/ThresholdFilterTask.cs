using System;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var (width, height) = (original.GetLength(0), original.GetLength(1));
            var blackAndWhiteImage = new double[width, height];
            var threshold = GetThreshold(original, (int)(whitePixelsFraction * original.Length)); 
            
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    blackAndWhiteImage[x, y] = original[x, y] >= threshold ? 1 : 0;
                }
            }
            return blackAndWhiteImage;
        }

        private static double GetThreshold(double[,] original, int n)
        {
            var flatArray = GetFlatArray(original);
            Array.Sort(flatArray);
            return n > 0 ? flatArray[flatArray.Length - n] : double.MaxValue;
        }

        private static double[] GetFlatArray(double[,] array)
        {
            var (width, height) = (array.GetLength(0), array.GetLength(1));
            var flatArray = new double[width * height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    flatArray[(height * x) + y] = array[x, y];
                }
            }
            return flatArray;
        }
    }
}