using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        private const string Space = @"\s+";

        public string GetWordFrequency(string inputStr)
        {
            string[] words = Regex.Split(inputStr, Space);
            List<string> strList = words.GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key + " " + g.Count())
                .ToList();
            return string.Join("\n", strList.ToArray());
        }

        private Dictionary<string, List<WordCount>> GetListMap(List<WordCount> inputList)
        {
            Dictionary<string, List<WordCount>> word2WordCount = new Dictionary<string, List<WordCount>>();
            foreach (var word in inputList)
            {
                if (!word2WordCount.ContainsKey(word.Value))
                {
                    List<WordCount> wordList = new List<WordCount>();
                    wordList.Add(word);
                    word2WordCount.Add(word.Value, wordList);
                }
                else
                {
                    word2WordCount[word.Value].Add(word);
                }
            }

            return word2WordCount;
        }
    }
}
