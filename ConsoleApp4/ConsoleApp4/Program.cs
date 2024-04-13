using static Program;

public class Program
{
    public static void Main()
    {
        Console.WriteLine(IsSubsequence(new int[] { 1, 2, 3, 4, 5}, 
            new int[] { 4, 5, 9, 1, 4, 3, 1, 2, 5, 6, 7, 3, 4 }));
    }

    public static bool IsSubsequence(int[] array1, int[] array2)
    {
        int i = 0;
        foreach (int item in array2)
        {
            if (item == array1[i])
                i++;
            if (i == array1.Length)
                return true;
        }
        return false;
    }
}