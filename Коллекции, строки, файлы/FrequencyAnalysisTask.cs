using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var grams = new Dictionary<string, string>();
            var continuations = GenerateAllContinuation(text);
            foreach (var kvp in continuations)
            {
                grams.Add(kvp.Key, GetBestContinuation(kvp.Value));
            }

            return grams;
        }

        private static Dictionary<string, Dictionary<string, int>> GenerateAllContinuation(List<List<string>> text)
        {
            var continuations = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
                ParseSentence(sentence, continuations);

            return continuations;
        }

        private static void ParseSentence(
            List<string> sentence,
            Dictionary<string, Dictionary<string, int>> continuations)
        {
            for (var wordNumber = 0; wordNumber < sentence.Count - 1; wordNumber++)
            {
                for (int shift = 1; shift <= 2; shift++)
                {
                    if (wordNumber == sentence.Count - 2 && shift == 2)
                        continue;
                    var startWord = shift == 1
                        ? sentence[wordNumber]
                        : $"{sentence[wordNumber]} {sentence[wordNumber + 1]}";
                    var wordContinuation = sentence[wordNumber + shift];

                    UpdateOrAddContinuation(continuations, startWord, wordContinuation);
                }
            }
        }

        private static void UpdateOrAddContinuation(
            Dictionary<string, Dictionary<string, int>> continuations,
            string startWord, 
            string wordContinuation)
        {
            if (!continuations.ContainsKey(startWord))
                continuations.Add(startWord, new Dictionary<string, int>());

            if (continuations[startWord].ContainsKey(wordContinuation))
                continuations[startWord][wordContinuation]++;
            else
                continuations[startWord].Add(wordContinuation, 1);
        }

        private static string GetBestContinuation(Dictionary<string, int> continuations)
        {
            var (bestContinuation, frequency) = ("", 0);
            foreach (var continuation in continuations)
            {
                if (continuation.Value > frequency)
                    (bestContinuation, frequency) = (continuation.Key, continuation.Value);
                else if (frequency == continuation.Value)
                    bestContinuation = GetLexicographicallyLesser(bestContinuation, continuation.Key);
            }

            return bestContinuation;
        }

        private static string GetLexicographicallyLesser(string strA, string strB)
        {
            return string.CompareOrdinal(strA, strB) < 0 ? strA : strB;
        }
    }
}