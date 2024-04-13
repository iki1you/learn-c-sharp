using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var frequency = MakeDictionary(text);
            foreach (var firstWord in frequency)
            {
                var maxValue = 0;
                string mostFrequentlyWord = null;
                foreach (var word in firstWord.Value)
                {
                    if (word.Value == maxValue)
                        if (string.CompareOrdinal(mostFrequentlyWord, word.Key) > 0)
                            mostFrequentlyWord = word.Key;

                    if (word.Value > maxValue)
                    {
                        mostFrequentlyWord = word.Key;
                        maxValue = word.Value;
                    }
                }
                result.Add(firstWord.Key, mostFrequentlyWord);
            }

            return result;
        }

        public static Dictionary<string, Dictionary<string, int>> MakeDictionary(List<List<string>> text)
        {
            var frequency = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    WriteInDictionary(frequency, sentence[i + 1], sentence[i]);
                    if (i < sentence.Count - 2)
                    {
                        WriteInDictionary(frequency, sentence[i + 2], sentence[i] + " " + sentence[i + 1]);
                    }
                }
            }
            return frequency;
        }

        private static void WriteInDictionary(
            Dictionary<string, Dictionary<string, int>> frequency,
            string end,
            string start
            )
        {
            if (!frequency.ContainsKey(start))
                frequency[start] = new Dictionary<string, int> { [end] = 1 };
            else
                if (frequency[start].ContainsKey(end))
                    frequency[start][end] += 1;
            else
                frequency[start][end] = 1;
        }
    }
}