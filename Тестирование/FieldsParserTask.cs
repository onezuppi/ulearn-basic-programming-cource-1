using System.Collections.Generic;
using System.Text;
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

        [TestCase("text", new[] {"text"})]
        [TestCase("hello world", new[] {"hello", "world"})]
        [TestCase("''", new[] {""})]
        [TestCase(@"'\''", new[] {@"'"})]
        [TestCase(@"'\\\''", new[] {@"\'"})]
        [TestCase("'", new[] {""})]
        [TestCase("'h'   'h", new[] {"h", "h"})]
        [TestCase("h 'h'", new[] {"h", "h"})]
        [TestCase(@"'h\\'", new[] {@"h\"})]
        [TestCase(@"'h \'h'", new[] {@"h 'h"})]
        [TestCase("'h' h", new[] {"h", "h"})]
        [TestCase("' '' '", new[] {" ", " "})]
        [TestCase("' \" '", new[] {" \" "})]
        [TestCase("\"\\\"\"", new[] {"\""})]
        [TestCase("' ", new[] {" "})]
        [TestCase(" ''  ''", new[] {"", ""})]
        [TestCase("\"''\"", new[] {"''"})]
        [TestCase("", new string[0] )]
        [TestCase("aa ", new[] {"aa"} )]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var tokens = new List<Token>();
            var index = 0;
            while (true)
            {
                index = GetIndexWithSkipSpaces(line, index);
                if (index >= line.Length)
                    break;
                var token = IsQuote(line[index]) ? ReadQuotedField(line, index) : ReadField(line, index);
                tokens.Add(token);
                index = token.GetIndexNextToToken();
            }
            return tokens;
        }
        
        private static Token ReadField(string line, int startIndex)
        {
            var value = new StringBuilder();
            var position = startIndex;
            while (position < line.Length && !IsQuote(line[position]) && line[position] != ' ') 
            {
                value.Append(line[position]);
                position++;
            }
            return new Token(value.ToString(), startIndex, position - startIndex);
        }
        
        private static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
        
        private static int GetIndexWithSkipSpaces(string line, int startIndex)
        {
            while (startIndex < line.Length && line[startIndex] == ' ')
            {
                startIndex++;
            }
            return startIndex;
        }
    
        private static bool IsQuote(char ch)
        {
            return ch == '"' || ch == '\'';
        }
    }
}