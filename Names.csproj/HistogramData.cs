using System.Linq;
using System.Security.Cryptography;

namespace Names
{
    public class HistogramData
    {
        public HistogramData(string title, string[] names, double[] values)
        {
            Title = title;
            Names = names;
            Values = values;
        }

        public string Title { get; }
        public string[] Names { get; }
        public double[] Values { get; }

        public bool Equals(HistogramData other)
        {
            return other.Names.SequenceEqual(Names) && other.Values.SequenceEqual(Values);
        }
    }
}