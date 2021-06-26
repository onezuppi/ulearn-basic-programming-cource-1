using System;
using System.Collections.Generic;
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
        [TestCase("'afdg'", 0, "afdg", 6)]
        [TestCase("'afdg\\'", 0, "afdg'", 7)]
        [TestCase("' '", 0, " ", 3)]
        [TestCase("' \\' '", 0, " ' ", 6)]
        [TestCase(@"'\\ s'", 0, @"\ s", 6)]
        [TestCase(@"hh ' \'' ", 3, @" '", 5)]
        [TestCase(@"hh ' \' ", 3, @" ' ", 5)]
        [TestCase(@"'\\\\\\\\", 0, @"\\\\", 9)]
        [TestCase("a'\\'a\"b'", 2, "'a\"b'", 6)]
        [TestCase(@"hhh'\\ \' \' \\", 3, @"\ ' ' \", 12)]
        [TestCase("Hello, 'World'!", 7, "World", 7)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
    }

    static class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var value = new StringBuilder();
            var position = 1 + startIndex;
            while (position < line.Length && line[position] != line[startIndex])
            {
                value.Append(line[position] == '\\' ? line[++position] : line[position]);
                position++;
            }
            return new Token(
                value.ToString(), 
                startIndex, 
                position - startIndex + (line.Length > position ? 1 : 0));
        }
    }
}