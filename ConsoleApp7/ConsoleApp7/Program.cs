using System;

public class Program
{
    public static void Main()
    {
        string[] input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int sum = int.Parse(input[1]) / 2;

        long[,] dp = new long[n + 1, sum + 1];

        for (int i = 0; i <= n; i++) 
            dp[i, 0] = 1;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= sum; j++)
            {
                for (int k = 0; k <= 9; k++)
                {
                    if (j - k >= 0)
                    {
                        dp[i, j] += dp[i - 1, j - k];
                    }
                }
            }
        }

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= sum; j++)
                Console.Write(dp[i, j] + " ");
            Console.Write("\n");
        }
            

        Console.WriteLine(dp[n, sum] * dp[n, sum]);
    }
}


// { MakeTest(2, 2, "4"); }
// { MakeTest(2, 5, "0"); }
// { MakeTest(10, 10, "4008004"); }
// { MakeTest(20, 20, "401200499400100"); }