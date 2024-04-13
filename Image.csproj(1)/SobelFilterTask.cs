using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var size = sx.GetLength(0);
            var result = new double[width, height];
            var limit = size / 2;
            for (int x = limit; x < width - limit; x++)
                for (int y = limit; y < height - limit; y++)
                {
                    var local = GetLocal(g, x, y, size);
                    var gradientX = Convolute(local, sx, size);
                    var gradientY = Convolute(local, GetTransposed(sx, size), size);
                    result[x, y] = Math.Sqrt(gradientX * gradientX + gradientY * gradientY);
                }
            return result;
        }

        private static double[,] GetLocal(double[,] g, int x, int y, int size)
        {
            var local = new double[size, size];
            var limitLocal = size / 2;
            for (int i = -limitLocal; i <= limitLocal; i++)
                for (int j = -limitLocal; j <= limitLocal; j++)
                    local[i + limitLocal, j + limitLocal] = g[x + j, y + j];
            return local;
        }

        private static double Convolute(double[,] local, double[,] sx, int size)
        {
            double total = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    total += local[i, j] * sx[i, j];
            return total;
        }

        private static double[,] GetTransposed(double[,] sx, int size)
        {
            var transposed = new double[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    transposed[x, y] = sx[y, x];
            return transposed;
        }
    }
}