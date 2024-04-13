using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PocketGoogle
{
    public class Indexer
    {
        private static readonly char[] charsForSplit = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };

        private static readonly Dictionary<int, Dictionary<string, List<int>>> wordsAndPosByIndex = new Dictionary<int, Dictionary<string, List<int>>>();
        private static readonly Dictionary<string, HashSet<int>> indexesByWord = new Dictionary<string, HashSet<int>>();

        public static void Main()
        {
            int id = 0;
            string documentText = "B C";
            var words = new List<string>();
            wordsAndPosByIndex.Add(id, new Dictionary<string, List<int>>());
            NewMethod(documentText, words);

            int currentPos = 0;
            foreach (string word in words)
            {
                if (!indexesByWord.ContainsKey(word))
                    indexesByWord[word] = new HashSet<int>();

                if (!indexesByWord[word].Contains(id))
                    indexesByWord[word].Add(id);


                if (!wordsAndPosByIndex[id].ContainsKey(word))
                    wordsAndPosByIndex[id].Add(word, new List<int>());

                wordsAndPosByIndex[id][word].Add(currentPos);
                currentPos += word.Length + 1;
            }

            foreach (var word in words)
                Console.WriteLine(word);
        }

        private static void NewMethod(string documentText, List<string> words)
        {
            var startIndex = 0;
            for (int i = 0; i < documentText.Length; i++)
            {
                if (charsForSplit.Contains(documentText[i]))
                {
                    if (documentText.Substring(startIndex, documentText.Length - startIndex).Trim() != "")
                        words.Add(documentText.Substring(startIndex, i - startIndex).Trim());
                    startIndex = ++i;
                }
            }
            if (documentText.Substring(startIndex, documentText.Length - startIndex).Trim() != "")
                words.Add(documentText.Substring(startIndex, documentText.Length - startIndex).Trim());
        }
    }
}