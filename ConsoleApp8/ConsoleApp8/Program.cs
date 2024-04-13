using System;

namespace GeometryTasks
{
    private static void Main()
    {
        var array = new[]
        {
        new Point { X = 1, Y = 0 },
        new Point { X = -1, Y = 0 },
        new Point { X = 0, Y = 1 },
        new Point { X = 0, Y = -1 },
        new Point { X = 0.01, Y = 1 }
        };
        Array.Sort(array, new ClockwiseComparer());
        foreach (Point e in array)
            Console.WriteLine("{0} {1}", e.X, e.Y);
    }

    public class Point
    {
        public double X;
        public double Y;
    }

    public class ClockwiseComparer : IComparer
    {
        public int Compare(object x, object y)
        {

        }
    }
}