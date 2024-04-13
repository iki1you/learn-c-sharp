using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.IO;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Encoding.UTF8.GetBytes("БЩФzw!").Length);
    }
}