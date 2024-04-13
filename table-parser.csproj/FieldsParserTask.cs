using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("'a b c'", new[] { "a b c" })]
        [TestCase("\"a 'b' c\"", new[] { "a 'b' c" })]
        [TestCase("\'a \"b\" c\'", new[] { "a \"b\" c" })]
        [TestCase("'' \"bcd ef\" 'x y'", new[] { "", "bcd ef", "x y" })]
        [TestCase("a\"b", new[] { "a", "b" })]
        [TestCase("\"a b\\\"", new[] { "a b\"" })]
        [TestCase("'a'\\", new[] { "a", @"\" })]
        [TestCase("\\'a'", new[] { @"\", "a" })]
        [TestCase("  a   ", new[] { "a" })]
        [TestCase(@"'a\''", new[] { "a'" })]
        [TestCase("", new string[] { })]
        [TestCase("\"\\\\\"", new[] { "\\" })]
        [TestCase("'a  ", new[] { "a  " })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        public static List<Token> ParseLine(string line)
        {
            var tokens = new List<Token>();
            var index = 0;
            while (index < line.Length)
            {
                var spaces = ReadSpaces(line, index);
                index = spaces.GetIndexNextToToken();
                if (index >= line.Length)
                    break;
                var field = ReadField(line, index);
                tokens.Add(field);
                index = field.GetIndexNextToToken();
            }
            return tokens;
        }

        private static Token ReadSpaces(string line, int startIndex)
        {
            var index = startIndex;
            while (index < line.Length && line[index] == ' ')
                index++;
            return new Token(string.Empty, startIndex, index - startIndex);
        }

        private static Token ReadField(string line, int startIndex)
        {
            var symbol = line[startIndex];
            if (symbol == '\'' || symbol == '"')
                return ReadQuotedField(line, startIndex);
            var index = startIndex;
            var builder = new StringBuilder();
            while (index < line.Length && line[index] != '"' && line[index] != '\'' && line[index] != ' ')
            {
                builder.Append(line[index]);
                index++;
            }
            return new Token(builder.ToString(), startIndex, index - startIndex);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}