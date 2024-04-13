using System;
using System.Reflection;
using System.Xml.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            double[,] heat = new double[30, 12];
            foreach (var item in names)
            {
                if (item.BirthDate.Day != 1)
                    heat[item.BirthDate.Day - 2, item.BirthDate.Month - 1]++;
            }
            string[] arrayLable = InitializeArray(30, 2);
            string[] arrayValues = InitializeArray(12, 1);
            return new HeatmapData(
                "Пример карты интенсивностей",
                heat,
                arrayLable,
                arrayValues);
        }

        public static string[] InitializeArray(int lenth, int k)
        {
            string[] arrayLable;
            arrayLable = new string[lenth];
            for (int i = 0; i < lenth; i++)
                arrayLable[i] = (i + k).ToString();
            return arrayLable;
        }
    }
}