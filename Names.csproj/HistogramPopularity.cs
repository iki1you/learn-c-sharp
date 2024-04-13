using System.Collections.Generic;
using System.Linq;

namespace Names
{
    internal class HistogramPopularity
    {
        public static HistogramData NamesPopularity(NameData[] names, int year, int count)
        {
            Dictionary<string, int> needNames = new Dictionary<string, int>();
            foreach (var item in names)
            {
                if (item.BirthDate.Year == year)
                    if (!needNames.ContainsKey(item.Name))
                        needNames.Add(item.Name, 1);
                    else
                        needNames[item.Name]++;
            }
            double[] values;
            string[] topNames;
            values = new double[needNames.Count];
            topNames = new string[needNames.Count];
            int i = 0;
            int countOther = 0;
            foreach (var item in needNames.OrderBy(item => item.Value).Reverse())
            {
                if (i < count)
                {
                    topNames[i] = item.Key;
                    values[i] = item.Value;
                    i++;
                }
                else
                    countOther++;
            }
            if (countOther > 0)
            {
                topNames[count] = "остальные";
                values[count] = countOther;
            }
            return new HistogramData(
                string.Format($"Популярность имён в {year}."),
                topNames,
                values);
        }
    }
}
