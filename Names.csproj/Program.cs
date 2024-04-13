using System;
using System.IO;
using System.Linq;

namespace Names
{
    public static class Program
    {
        private static readonly string dataFilePath = "names.txt";

        private static void Main(string[] args)
        {
            var namesData = ReadData();
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var item in namesData)
            {
                if (item.BirthDate.Year < min)
                    min = item.BirthDate.Year;
                if (item.BirthDate.Year > max)
                    max = item.BirthDate.Year;
            }
            Console.WriteLine($"Пожалуйста, введите год от {min} до {max} включительно.");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите максимальное количество имён.");
            var count = int.Parse(Console.ReadLine());
            Charts.ShowHistogram(HistogramPopularity.NamesPopularity(namesData, year, count));
            Console.WriteLine();
        }


        private static NameData[] ReadData()
        {
            var lines = File.ReadAllLines(dataFilePath);
            var names = new NameData[lines.Length];
            for (var i = 0; i < lines.Length; i++)
                names[i] = NameData.ParseFrom(lines[i]);
            return names;
        }

        // А это более короткая версия ReadData(). Она использует механизм языка под названием Linq
        // Вы можете познакомиться с ней самостоятельно: https://ulearn.azurewebsites.net/Course/Linq
        // Освоив LINQ решать задачи подобные NamesTask становится гораздо проще и приятнее.
        // Но это уже совсем другая история.
        private static NameData[] ReadData2()
        {
            return File
                .ReadLines(dataFilePath)
                .Select(NameData.ParseFrom)
                .ToArray();
        }
    }
}