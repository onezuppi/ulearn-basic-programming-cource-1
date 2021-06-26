using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] image, double[,] sx)
        {
            var (width, height) = (image.GetLength(0), image.GetLength(1));
            var (maxDx, maxDy) = (sx.GetLength(0) / 2, sx.GetLength(1) / 2);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            
            for (var x = maxDx; x < width - maxDx; x++)
            {
                for (var y = maxDy; y < height - maxDy; y++)
                {
                    result[x, y] = ApplySobelFilter(image, sx, sy, x, y);
                }
            }
            return result;
        }

        private static double[,] TransposeMatrix(double[,] matrix)
        {
            var (width, height) = (matrix.GetLength(0), matrix.GetLength(1));
            var transposedMatrix = new double[height, width];
            
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    transposedMatrix[y, x] = matrix[x, y];
                }
            }
            return transposedMatrix;
        }

        private static double ApplySobelFilter(double[,] image, double[,] sx, double[,] sy, int x, int y)
        {
            var (width, height) = (sx.GetLength(0), sx.GetLength(1));
            var n = GeMatrixSegment(image, x, y, width, height);
            var gx = MultiplyElementwiseMatrices(sx, n);
            var gy = MultiplyElementwiseMatrices(sy, n);

            return Math.Sqrt(gx * gx + gy * gy);
        }

        private static double[,] GeMatrixSegment(double[,] matrix, int pointX, int pointY, int segmentWidth,
            int segmentHeight)
        {
            var result = new double[segmentWidth, segmentHeight];
            var (maxDx, maxDy) = (segmentWidth / 2, segmentHeight / 2);
            
            for (var dx = -maxDx; dx <= maxDx; dx++)
            {
                for (var dy = -maxDy; dy <= maxDy; dy++)
                {
                    result[dx + maxDx, dy + maxDy] = matrix[pointX + dx, pointY + dy];
                }
            }
            return result;
        }

        private static double MultiplyElementwiseMatrices(double[,] m1, double[,] m2)
        {
            var (width, height) = (m1.GetLength(0), m1.GetLength(1));
            var result = 0.0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    result += m1[x, y] * m2[x, y];
                }
            }
            return result;
        }
    }
}