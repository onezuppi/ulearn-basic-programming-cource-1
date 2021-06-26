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
            var sentence = new StringBuilder();
            sentence.Append(phraseBeginning);
            var lastWords = GetLastWords(phraseBeginning);
            for (var i = 0; i < wordsCount; i++)
            {
                var startWord = $"{lastWords[0]} {lastWords[1]}".Trim();
                if (!nextWords.ContainsKey(startWord))
                {
                    if (nextWords.ContainsKey(lastWords[1]))
                        startWord = lastWords[1];
                    else
                        break;
                }

                sentence.Append($" {nextWords[startWord]}");
                (lastWords[0], lastWords[1]) = (lastWords[1], nextWords[startWord]);
            }

            return sentence.ToString();
        }

        private static string[] GetLastWords(string phraseBeginning)
        {
            var words = phraseBeginning.Split(' ');
            if (words.Length > 1)
                return new[] {words[words.Length - 2], words[words.Length - 1]};
            return new[] {string.Empty, words[0]};
        }
    }
}