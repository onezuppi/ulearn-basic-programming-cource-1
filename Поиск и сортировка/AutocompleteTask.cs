using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal static class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];

            return null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            count = Math.Min(count, phrases.Count - leftBorder - 1);
            var prefixPhrases = new List<string>();
            for (var i = 1; i <= count; i++)
            {
                if (phrases[leftBorder + i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    prefixPhrases.Add(phrases[leftBorder + i]);
                else
                    break;
            }

            return prefixPhrases.ToArray();
        }

        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return (rightBorder - leftBorder - 1) % (phrases.Count + 1);
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "z", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "cz", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "bz", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "az", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "aa", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "cb", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "", 7)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "6", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "d", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "c", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "b", 0)]
        [TestCase(new[] {"aa", " ab", " bc", " bd", " be", " ca", " cb"}, "a", 0)]
        public static void CountByPrefixTest(string[] phrases, string prefix, int expectedIndex)
        {
            var actualIndex = AutocompleteTask.GetCountByPrefix(phrases.ToList().AsReadOnly(), prefix);
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(new[] {"a", "b", "c", "c", "d", "e"}, "z", 2, new string[0])]
        [TestCase(new[] {"a", "b", "c", "c", "d", "e"}, "c", 1, new[] {"c"})]
        [TestCase(new[] {"a", "b", "c", "c", "d", "e"}, "c", 10, new[] {"c", "c"})]
        [TestCase(new[] {"a", "bcdef"}, "", 2, new[] {"a", "bcdef"})]
        [TestCase(new[] {"a", "bcdef"}, "b", 2, new[] {"bcdef"})]
        [TestCase(new[] {"a", "bcdef"}, "bc", 2, new[] {"bcdef"})]
        [TestCase(new[] {"a", "bcdef"}, "bcd", 2, new[] {"bcdef"})]
        [TestCase(new[] {"a", "bcdef"}, "bcde", 2, new[] {"bcdef"})]
        [TestCase(new string[0], "b", 2, new string[0])]
        [TestCase(new string[0], "", 0, new string[0])]
        public static void TopByPrefixTest(string[] phrases, string prefix, int count, string[] expectedTopByPrefix)
        {
            var actualTopByPrefix = AutocompleteTask.GetTopByPrefix(phrases.ToList().AsReadOnly(), prefix, count);
            Assert.AreEqual(expectedTopByPrefix, actualTopByPrefix);
        }
    }
}