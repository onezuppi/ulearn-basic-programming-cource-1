using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
	static class SentencesParserTask
	{
		private static readonly char[] SentenceSeparators = new char[] {'.', '!', '?', ';', ':', '(', ')' };

		public static List<List<string>> ParseSentences(string text)
		{
			var sentencesList = new List<List<string>>();
			var sentences = text.Split(SentenceSeparators);
			foreach (var sentence in sentences)
			{
				var words = ParseSentence(sentence);
				if (words.Count > 0)
					sentencesList.Add(words);
			}
			return sentencesList;
		}

		private static List<string> ParseSentence(string text)
		{
			var word = new StringBuilder();
			var sentence = new List<string>();
			foreach (var ch in text)
			{
				if (char.IsLetter(ch) || ch == '\'')
					word.Append(ch);
				else
				{
					CheckAndAddWord(sentence, word.ToString());
					word.Clear();
				}
			}
			CheckAndAddWord(sentence, word.ToString());
            
			return sentence;
		}

		private static void CheckAndAddWord(List<string> sentence, string word)
		{
			if (word != string.Empty)
				sentence.Add(word.ToLower());   
		}
	}
}