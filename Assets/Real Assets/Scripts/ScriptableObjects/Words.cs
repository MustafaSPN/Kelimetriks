    using System;
    using System.Collections.Generic;
    using UnityEditor.Localization.Plugins.XLIFF.V12;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [CreateAssetMenu(fileName = "WordAssets", menuName = "ScriptableObjects/WordsAssetClassBase")]
    public class Words : ScriptableObject
    {
        [SerializeField] public TextAsset textFile;
        public Dictionary<char, Dictionary<int, HashSet<string>>> WordList;
        public List<List<string>> WordCountLists;
        public List<string> WordCountList0;
        public List<string> WordCountList1;
        public List<string> WordCountList2;
        public List<string> WordCountList3;
        public int segment = 0;
        public int count = 0;



        public void InitializeWords()
        {
            WordList = new Dictionary<char, Dictionary<int, HashSet<string>>>();
            WordCountList0 = new List<string>();
            WordCountList1 = new List<string>();
            WordCountList2 = new List<string>();
            WordCountList3 = new List<string>();
            WordCountLists = new List<List<string>>();
            WordCountLists.Add(WordCountList0);
            WordCountLists.Add(WordCountList1);
            WordCountLists.Add(WordCountList2);
            WordCountLists.Add(WordCountList3);

            
            
            
            if (textFile != null)
            {
                string allText = textFile.text;
                string[] lines = allText.Split('\n');


                foreach (string line in lines)
                {

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
                
                WordList['Ğ'] = new Dictionary<int, HashSet<string>>();
                for (int i = 0; i < 6; i++)
                {
                    WordList['Ğ'][i] = new HashSet<string>();
                }
            }

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
            string random;
            HashSet<string> hashWord = WordList[a][b];
            string[] randomWords = new string[hashWord.Count];
            hashWord.CopyTo(randomWords);
            random = randomWords[Random.Range(0, randomWords.Length)];


                if (count % 2 == 0)
                {
                    
                  
                }
                else  if (WordCountLists[segment].Count != 0)
                {
                    switch (segment)
                    {
                        case 0:
                            random = WordCountList0[Random.Range(0, WordCountList0.Count)];
                            WordCountList0.Remove(random);
                            break;
                        case 1:
                            random = WordCountList1[Random.Range(0, WordCountList1.Count)];
                            WordCountList1.Remove(random);
                            break;
                        case 2:
                            random = WordCountList2[Random.Range(0, WordCountList2.Count)];
                            WordCountList2.Remove(random);
                            break;
                        case 3:
                            random = WordCountList3[Random.Range(0, WordCountList3.Count)];
                            WordCountList3.Remove(random);
                            break;

                    }
                }
            
            count++;
            return random;
        }

        public void AddWordCountDataSegment1(string word)
        {
            WordCountList0.Add(word);
        }
        public void AddWordCountDataSegment2(string word)
        {
            WordCountList1.Add(word);
        }
        
        public void AddWordCountDataSegment3(string word)
        {
            WordCountList2.Add(word);
        }
        
        public void AddWordCountDataSegment4(string word)
        {
            WordCountList3.Add(word);
        }

        public void SetSegment(int seg)
        {
            segment = seg;
        }

        public void ResetWordsObject()
        {
            WordCountList0.Clear();
            WordCountList1.Clear();
            WordCountList2.Clear();
            WordCountList3.Clear();
            segment = 0;
            count = 0;
        }

        public void OnEnable()
        {
            Messenger.AddListener(GameEvent.START_GAME,ResetWordsObject);
        }

        public void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.START_GAME,ResetWordsObject);
        }
    }
