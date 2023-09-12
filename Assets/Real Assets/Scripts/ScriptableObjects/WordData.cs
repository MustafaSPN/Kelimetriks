using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "WordAssets",menuName = "ScriptableObjects/WordsAsset")]

public class WordData : ScriptableObject
{
    [SerializeField] public TextAsset textFile;
   // public List<string> allWords = new List<string>();
    public HashSet<string> allWordsSet = new();

    public void InitializeWords()
    {
        if (textFile != null)
        {
            string allText = textFile.text;
            string[] lines = allText.Split('\n');

            foreach (string line in lines)
            {
                allWordsSet.Add(line);

            }
        }

    }

    public bool SearchWord(string word)
    {
        return allWordsSet.Contains(word);
    }
}
