using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);
            var filtered = new double[lengthX, lengthY];
            for (int i = 0; i < lengthX; i++)
                for (int j = 0; j < lengthY; j++)
                {
                    var borders = SetBordersPixels(i, j, lengthX, lengthY, original);
                    SetMedian(filtered, borders, i, j);
                }
            return filtered;
        }

        private static void SetMedian(double[,] filtered, List<double> borders, int i, int j)
        {
            borders.Sort();
            int count = borders.Count();
            if (count % 2 != 0)
                filtered[i, j] = borders[count / 2];
            else
                filtered[i, j] = (borders[count / 2 - 1] + borders[count / 2]) / 2;
        }

        private static List<double> SetBordersPixels(int i, int j, 
            int lengthX, int lengthY, 
            double[,] original
            )
        {
            var borders = new List<double>();
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    if (i + x >= 0 && i + x < lengthX && j + y >= 0 && j + y < lengthY)
                        borders.Add(original[i + x, j + y]);
            return borders;
        }
    }
}