    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [CreateAssetMenu(fileName = "WordAssets", menuName = "ScriptableObjects/WordsAssetClassBase")]
    public class Words : ScriptableObject
    {
        [SerializeField] public TextAsset textFile;
        private HashSet<string> allWords;
        private Dictionary<char, Dictionary<int, HashSet<string>>> WordList;

        public void InitializeWords()
        {
            int count = 0;
            WordList = new Dictionary<char, Dictionary<int, HashSet<string>>>();

            if (textFile != null)
            {
                string allText = textFile.text;
                string[] lines = allText.Split('\n');


                foreach (string line in lines)
                {

                    count++;
                    char firstLetter = line[0];
                    int wordLength = line.Length;

                    if (!WordList.ContainsKey(firstLetter))
                    {
                        WordList[firstLetter] = new Dictionary<int, HashSet<string>>();
                    }

                    if (!WordList[firstLetter].ContainsKey(wordLength))
                    {
                        WordList[firstLetter][wordLength] = new HashSet<string>();
                    }

                    WordList[firstLetter][wordLength].Add(line);
                }

            }

            Debug.Log(count);
        }

        public bool SearchWord(string word)
        {

            char f = word[0];
            int l = word.Length;
    bool isExist = WordList[f][l].Contains(word);
    return isExist;
        }

        public string RandomWord(char a,int b)
        {
            HashSet<string> hashWord = WordList[a][b];
            string[] randomWords = new string[hashWord.Count];
            hashWord.CopyTo(randomWords);
            return randomWords[Random.Range(0, randomWords.Length)];
        }
    }
