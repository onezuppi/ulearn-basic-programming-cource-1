using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private static readonly char[] WordSeparators = {' ', '.', ',', '!', '?', ':', '-', '\r', '\n'};

        private Dictionary<string, Dictionary<int, List<int>>> wordDocumentsOccurrences =
            new Dictionary<string, Dictionary<int, List<int>>>();

        private Dictionary<int, List<string>> documentsWords = new Dictionary<int, List<string>>();

        public void Add(int id, string documentText)
        {
            documentsWords[id] = new List<string>();
            foreach (var (word, index) in GetWords(documentText))
            {
                if (!wordDocumentsOccurrences.ContainsKey(word))
                    wordDocumentsOccurrences[word] = new Dictionary<int, List<int>>();
                if (!wordDocumentsOccurrences[word].ContainsKey(id))
                    wordDocumentsOccurrences[word][id] = new List<int>();
                wordDocumentsOccurrences[word][id].Add(index);
                documentsWords[id].Add(word);
            }
        }

        public List<int> GetIds(string word)
        {
            return wordDocumentsOccurrences.ContainsKey(word)
                ? wordDocumentsOccurrences[word].Keys.ToList()
                : new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (wordDocumentsOccurrences.ContainsKey(word) && wordDocumentsOccurrences[word].ContainsKey(id))
                return wordDocumentsOccurrences[word][id];
            return new List<int>();
        }

        public void Remove(int id)
        {
            foreach (var word in documentsWords[id])
                wordDocumentsOccurrences[word].Remove(id);
            documentsWords.Remove(id);
        }

        private IEnumerable<(string Word, int Index)> GetWords(string text)
        {
            var word = new StringBuilder();
            for (var i = 0; i < text.Length; i++)
            {
                if (WordSeparators.Contains(text[i]))
                {
                    if (word.Length > 0)
                        yield return (Word: word.ToString(), Index: i - word.Length);
                    word.Clear();
                    continue;
                }

                word.Append(text[i]);
            }

            if (word.Length > 0)
                yield return (Word: word.ToString(), Index: text.Length - word.Length);
        }
    }
}