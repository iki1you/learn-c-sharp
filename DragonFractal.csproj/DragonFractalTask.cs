using System;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        const double AngleFisrt = Math.PI / 4;
        const double AngleSecond = Math.PI / 4 * 3;

        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            var random = new Random(seed);
            double x = 1, y = 0, x1 = 1, y1 = 0;
            for (int i = 0; i < iterationsCount; i++)
            {
                (x, y) = GeneratePixel(random, x, y, x1, y1);
                pixels.SetPixel(x, y);
            }
        }

        public static (double, double) GeneratePixel(Random random, double x, double y, double x1, double y1)
        {
            if (random.Next(1) == 0)
            {
                x1 = (x * Math.Cos(AngleFisrt) - y * Math.Sin(AngleFisrt)) / Math.Sqrt(2);
                y1 = (x * Math.Sin(AngleFisrt) + y * Math.Cos(AngleFisrt)) / Math.Sqrt(2);
            }
            else
            {
                x1 = (x * Math.Cos(AngleSecond) - y * Math.Sin(AngleSecond)) / Math.Sqrt(2) + 1;
                y1 = (x * Math.Sin(AngleSecond) + y * Math.Cos(AngleSecond)) / Math.Sqrt(2);
            }
            return (x1, y1);
        }
    }
}