using System;
using System.Collections.Generic;
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
                //split the input string with 1 to n pieces of spaces
                string[] words = Regex.Split(inputStr, Space);

                List<WordCount> inputList = new List<WordCount>();
                foreach (var s in words)
                {
                    WordCount input = new WordCount(s, 1);
                    inputList.Add(input);
                }

                //get the map for the next step of sizing the same word
                Dictionary<string, List<WordCount>> map = GetListMap(inputList);

                List<WordCount> list = new List<WordCount>();
                foreach (var entry in map)
                {
                    WordCount input = new WordCount(entry.Key, entry.Value.Count);
                    list.Add(input);
                }

                inputList = list;

                inputList.Sort((w1, w2) => w2.Count - w1.Count);

                List<string> strList = new List<string>();

                //stringJoiner joiner = new stringJoiner("\n");
                foreach (WordCount w in inputList)
                {
                    string s = w.Value + " " + w.Count;
                    strList.Add(s);
                }

                return string.Join("\n", strList.ToArray());
            }
        }

        private Dictionary<string, List<WordCount>> GetListMap(List<WordCount> inputList)
        {
            Dictionary<string, List<WordCount>> map = new Dictionary<string, List<WordCount>>();
            foreach (var input in inputList)
            {
                //       map.computeIfAbsent(input.getValue(), k -> new ArrayList<>()).add(input);
                if (!map.ContainsKey(input.Value))
                {
                    List<WordCount> arr = new List<WordCount>();
                    arr.Add(input);
                    map.Add(input.Value, arr);
                }
                else
                {
                    map[input.Value].Add(input);
                }
            }

            return map;
        }
    }
}
