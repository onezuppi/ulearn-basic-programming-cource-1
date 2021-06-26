using System;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestPath = new int[checkpoints.Length];
            var currentPath = Enumerable.Range(0, checkpoints.Length).ToArray();
            GetShortestPath(checkpoints, currentPath, bestPath, 1, 0.0, double.MaxValue);
            
            return bestPath;
        }

        private static double GetShortestPath(Point[] checkpoints, int[] currentPath, int[] bestPath,
            int position, double currentPathLength, double bestPathLength)
        {
            if (position == currentPath.Length)
            {
                Array.Copy(currentPath, bestPath, currentPath.Length);

                return currentPathLength;
            }

            for (var i = 1; i < currentPath.Length; i++)
            {
                var newCurrentPathLength =
                    currentPathLength + checkpoints[currentPath[position - 1]].DistanceTo(checkpoints[i]);
                if (Array.IndexOf(currentPath, i, 0, position) != -1)
                    continue;

                if (newCurrentPathLength >= bestPathLength)
                    return bestPathLength;

                currentPath[position] = i;
                bestPathLength = GetShortestPath(checkpoints, currentPath, bestPath, position + 1,
                    newCurrentPathLength, bestPathLength);
            }

            return bestPathLength;
        }
    }
}