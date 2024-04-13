using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceBetweenPoints(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double a = GetDistanceBetweenPoints(ax, bx, ay, by);
            double b = GetDistanceBetweenPoints(x, ax, y, ay);
            double c = GetDistanceBetweenPoints(x, bx, y, by);
            if (ax == bx && ay == by)
                return GetDistanceBetweenPoints(ax, x, ay, y);
            if (b * b > a * a + c * c || c * c > a * a + b * b)
                return Math.Min(b, c);
            return Math.Abs((by - ay) * x - (bx - ax) * y + bx * ay - by * ax) /
                Math.Sqrt((by - ay) * (by - ay) + (bx - ax) * (bx - ax));
        }
    }
}