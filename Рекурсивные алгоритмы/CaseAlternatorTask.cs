using System.Collections.Generic;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            var word = lowercaseWord.ToCharArray();
            AlternateCharCases(word, 0, result);
            return result;
        }

        private static void AlternateCharCases(char[] word, int position, List<string> result)
        {
            if (position == word.Length)
            {
                result.Add(new string(word));
                return;
            }

            var (lowerChar, upperChar) = (char.ToLower(word[position]), char.ToUpper(word[position]));
            word[position] = lowerChar;
            AlternateCharCases(word, position + 1, result);
            if (lowerChar == upperChar || !char.IsLetter(word[position]))
                return;
            word[position] = upperChar;
            AlternateCharCases(word, position + 1, result);
        }
    }
}