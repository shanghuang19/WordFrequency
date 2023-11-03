using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        private const string Space = @"\s+";

        public string GetResult(string inputStr)
        {
            if (Regex.Split(inputStr, Space).Length == 1)
            {
                return inputStr + " 1";
            }
            else
            {
                //split the word string with 1 to n pieces of spaces
                string[] words = Regex.Split(inputStr, Space);
                List<WordCount> inputWordList = words.Select(word => new WordCount(word, 1)).ToList();

                //get the mapWords for the next step of sizing the same word
                Dictionary<string, List<WordCount>> mapWords = GetListMap(inputWordList);

                List<WordCount> list = mapWords
                    .Select(mapWord => new WordCount(mapWord.Key, mapWord.Value.Count))
                    .ToList();
                inputWordList = list;

                inputWordList.Sort((word1, word2) => word2.Count - word1.Count);

                List<string> finalWordList = (from WordCount word in inputWordList
                                        let s = word.Value + " " + word.Count
                                        select s).ToList();
                return string.Join("\n", finalWordList.ToArray());
            }
        }

        private Dictionary<string, List<WordCount>> GetListMap(List<WordCount> inputList)
        {
            Dictionary<string, List<WordCount>> map = new Dictionary<string, List<WordCount>>();
            foreach (var word in inputList)
            {
                if (!map.ContainsKey(word.Value))
                {
                    List<WordCount> wordList = new List<WordCount>();
                    wordList.Add(word);
                    map.Add(word.Value, wordList);
                }
                else
                {
                    map[word.Value].Add(word);
                }
            }

            return map;
        }
    }
}
