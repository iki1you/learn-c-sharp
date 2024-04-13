using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var phrase = new List<string>();
            phrase.AddRange(phraseBeginning.Split());
            for (int i = 0; i < wordsCount; i++)
            {
                if (phrase.Count >= 2 && 
                nextWords.ContainsKey(string.Join(" ", phrase.GetRange(phrase.Count - 2, 2))))
                {
                    phrase.Add(nextWords[string.Join(" ", phrase.GetRange(phrase.Count - 2, 2))]);
                }
                else
                {
                    if (nextWords.ContainsKey(phrase[phrase.Count - 1]))
                        phrase.Add(nextWords[phrase[phrase.Count - 1]]);
                    else
                        break;
                }
                    
            }
            return string.Join(" ", phrase);
        }
    }
}