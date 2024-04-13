using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public readonly char[] Separators = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };

        private readonly Dictionary<string, HashSet<int>> idByWord =
            new Dictionary<string, HashSet<int>>();

        private readonly Dictionary<int, Dictionary<string, List<int>>> indexedWords = 
            new Dictionary<int, Dictionary<string, List<int>>>();

        public void Add(int id, string documentText)
        {
            var words = new List<string>();
            var word = new StringBuilder();
            indexedWords.Add(id, new Dictionary<string, List<int>>());
            int currentPos = 0;
            foreach (var item in documentText)
            {
                if (!Separators.Contains(item))
                    word.Append(item);
                else
                {
                    currentPos = AddWord(id, currentPos, word.ToString());
                    word.Clear();
                }
            }
            if (word.ToString().Length > 0)
                currentPos = AddWord(id, currentPos, word.ToString());
        }

        private int AddWord(int id, int currentPos, string word)
        {
            if (!idByWord.ContainsKey(word))
                idByWord[word] = new HashSet<int>();

            if (!idByWord[word].Contains(id))
                idByWord[word].Add(id);

            if (!indexedWords[id].ContainsKey(word))
                indexedWords[id].Add(word, new List<int>());

            indexedWords[id][word].Add(currentPos);
            currentPos += word.Length + 1;

            return currentPos;
        }

        public List<int> GetIds(string word)
        {
            if (idByWord.ContainsKey(word))
                return idByWord[word].ToList();

            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            List<int> positions = new List<int>();

            if (indexedWords.ContainsKey(id) && indexedWords[id].ContainsKey(word))
                positions = indexedWords[id][word];

            return positions;
        }

        public void Remove(int id)
        {
            string[] words = indexedWords[id].Keys.ToArray();

            foreach (var word in words)
                idByWord[word].Remove(id);

            indexedWords.Remove(id);
        }
    }
}
