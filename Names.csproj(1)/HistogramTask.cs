using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            double[] arrayValues;
            string[] arrayLable = InitializeArray();
            arrayValues = new double[31];
            foreach (var item in names)
            {
                if (item.BirthDate.Day != 1 && item.Name == name)
                    arrayValues[item.BirthDate.Day - 1]++;
            }
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                arrayLable,
                arrayValues);
        }

        public static string[] InitializeArray()
        {
            string[] arrayLable;
            arrayLable = new string[31];
            for (int i = 0; i < 31; i++)
                arrayLable[i] = (i + 1).ToString();
            return arrayLable;
        }
    }
}