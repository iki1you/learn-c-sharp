internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine(MergeSort(new int[] { 2, 7, 1, 3 }));
    }

    private static int[] Merge(int[] c, int[] d)
    {
        int[] b = new int[c.Length + d.Length];
        var i = 0;
        var j = 0;
        for (var k = 0; k < b.Length; k++)
        {
            if (i != c.Length && c[i] < d[j])
            {
                b[k] = c[i];
                i++;
            }
            else
            {
                b[k] = d[j];
                j ++;
            }
        }
        return c;
    }

    private static int[] MergeSort(int[] A)
    {
        if (A.Length == 0 || A.Length == 1) { 
            return A;
        }

        int[] C = MergeSort(A.Skip(A.Length / 2).ToArray());
        int[] D = MergeSort(A.Take(A.Length / 2).ToArray());

        return Merge(C, D);
    }
}