using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var separators = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            var wordSeparators = new char[] { '\t', '\n', '\r', ' ' };
            var sentencesList = new List<List<string>>();
            var textSplitted = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < textSplitted.Length; i++)
            {
                var sentence = ParseSentence(textSplitted[i]);
                if (sentence.Count != 0)
                    sentencesList.Add(sentence);
            }
            return sentencesList;
        }

        public static List<string> ParseSentence(string rawSentence)
        {
            List<string> sentence = new List<string>();
            var builder = new StringBuilder();
            foreach (var letter in rawSentence)
            {
                if (char.IsLetter(letter) || letter == '\'')
                    builder.Append(char.ToLower(letter));
                else
                {
                    if (builder.Length != 0)
                        sentence.Add(builder.ToString());
                    builder.Clear();
                }
            }
            if (builder.Length != 0)
                sentence.Add(builder.ToString());
            return sentence;
        }
    }
}