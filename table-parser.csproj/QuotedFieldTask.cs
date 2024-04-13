using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'abcdew", 0, "abcdew", 7)]
        [TestCase("â'abcdew", 1, "abcdew", 7)]
        [TestCase("'\"a''", 0, "\"a", 4)]
        [TestCase("w'\\asda'\\'sd", 1, "asda", 7)]
        [TestCase("c'\\\\\\", 1, "", 4)]
        [TestCase("'", 0, "", 1)]
        [TestCase(@"'\'a'", 0, "'a", 5)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            int end = startIndex;
            var workString = new StringBuilder();
            for (int i = startIndex + 1; i < line.Length; i++)
            {
                end++;
                if (line[i] == '\\')
                    continue;
                if (line[i] == line[startIndex] && line[i - 1] != '\\')
                    break;
                workString.Append(line[i]);
            }
            Console.WriteLine(line);
            return new Token(workString.ToString(), startIndex, end - startIndex + 1);
        }
    }
}