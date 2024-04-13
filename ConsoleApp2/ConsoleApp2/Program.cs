using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public static void Main()
    {
        Console.WriteLine(Min(new[] { 3, 6, 2, 4 }));
        Console.WriteLine(Min(new[] { "B", "A", "C", "D" }));
        Console.WriteLine(Min(new[] { '4', '2', '7' }));
    }

    static IComparable Min(IComparable a, IComparable b, IComparable c)
    {
        foreach (var e in array)
            if (min.CompareTo(e) == 1)
                min = (IComparable)e;
    }
}