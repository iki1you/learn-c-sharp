using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);
            var filtered = new double[lengthX, lengthY];
            var t = GetGradient(original, whitePixelsFraction, lengthX, lengthY);
            for (int i = 0; i < lengthX; i++)
                for (int j = 0; j < lengthY; j++)
                {
                    if (original[i, j] >= t)
                        filtered[i, j] = 1.0;
                    else
                        filtered[i, j] = 0.0;
                }
            return filtered;
        }

        private static double GetGradient(
            double[,] original,
            double whitePixelsFraction,
            int lengthX, int lengthY
            )
        {
            var forSort = new List<double>();
            var t = 257.0;
            for (int i = 0; i < lengthX; i++)
                for (int j = 0; j < lengthY; j++)
                    forSort.Add(original[i, j]);
            forSort.Sort();
            forSort.Reverse();
            if (whitePixelsFraction * forSort.Count >= 1)
                t = forSort[(int)(whitePixelsFraction * forSort.Count - 1)];
            return t;
        }
    }
}