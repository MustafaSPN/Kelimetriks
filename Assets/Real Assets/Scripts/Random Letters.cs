using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LettersObject", menuName = "ScriptableObjects/RandomLetters")]
public class RandomLetters : ScriptableObject
{
    [SerializeField] public char[] letters = new char[29];

    private int[] letterCounts =
    {
        269131, 34477, 27259, 15299, 97218, 242808, 17243, 20723, 12415, 21985,
        73170, 224346, 2956, 102693, 156027, 119295, 161329, 51005, 11069, 24539,
        144308, 95234, 24754, 94266, 85568, 22222, 20873, 75652, 52811
    };

    public float[] letterProbabilities;
    public int totalLetterCount = 0;
    public Dictionary<char, int> Score = new Dictionary<char, int>();

    public void Initiliaze()
    {
        totalLetterCount = 0;
        // Toplam harf sayısını hesapla
        for (int i = 0; i < letterCounts.Length; i++)
        {
            totalLetterCount += letterCounts[i];
        }

        // Harf olasılıklarını hesapla ve toplamı 1 yap
        letterProbabilities = new float[letters.Length];
        for (int i = 0; i < letterCounts.Length; i++)
        {
            letterProbabilities[i] = (float)letterCounts[i] / totalLetterCount;
        }
        
        //TODO: WTF! Add then with loop! Why should I set them in every run?
        Score.Add('A', 120);
        Score.Add('B', 120);
        Score.Add('C', 120);
        Score.Add('Ç', 120);
        Score.Add('D', 120);
        Score.Add('E', 120);
        Score.Add('F', 120);
        Score.Add('G', 120);
        Score.Add('Ğ', 120);
        Score.Add('H', 120);
        Score.Add('I', 120);
        Score.Add('İ', 120);
        Score.Add('J', 120);
        Score.Add('K', 120);
        Score.Add('L', 120);
        Score.Add('M', 120);
        Score.Add('N', 120);
        Score.Add('O', 120);
        Score.Add('Ö', 120);
        Score.Add('P', 120);
        Score.Add('R', 120);
        Score.Add('S', 120);
        Score.Add('Ş', 120);
        Score.Add('T', 120);
        Score.Add('U', 120);
        Score.Add('Ü', 120);
        Score.Add('V', 120);
        Score.Add('Y', 120);
        Score.Add('Z', 120);
    }

    public char GenerateRandomLetter()
    {
        // Rastgele bir sayı seç ve hangi harfin geldiğini hesapla
        float randomValue = Random.Range(0.0f, 1.0f);
        float cumulativeProbability = 0.0f;
        // Debug.Log($"{randomValue}");
        for (int i = 0; i < letterProbabilities.Length; i++)
        {
            cumulativeProbability += letterProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return letters[i];
            }
        }

        // Herhangi bir harf seçilmezse varsayılan olarak ilk harfi dönün
        return letters[0];
    }

    public int GetScore(char ch)
    {
        return Score[ch];
    }
}

// public class Words
// {
//     private Dictionary<int, Word> WordList;
// }
//
// public class Word
// {
//     public string word;
//     public int length;
//     public char firstLetter;
// }