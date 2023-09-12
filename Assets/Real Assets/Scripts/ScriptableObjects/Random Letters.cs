    using System;
    using System.Collections.Generic;
using UnityEngine;
    using Random = UnityEngine.Random;

    [CreateAssetMenu(fileName = "LettersObject", menuName = "ScriptableObjects/RandomLetters")]
public class RandomLetters : ScriptableObject
{
    [SerializeField] public char[] letters = new char[29];

    [SerializeField] int[] letterCounts =
    {
        269131, 34477, 27259, 15299, 97218, 242808, 17243, 20723, 12415, 21985,
        73170, 224346, 2956, 102693, 156027, 119295, 161329, 51005, 11069, 24539,
        144308, 95234, 24754, 94266, 85568, 22222, 20873, 75652, 52811
    };

    public float[] letterProbabilities;
    public int totalLetterCount = 0;
    public Dictionary<char, int> Score = new Dictionary<char, int>();
    public Words words;
    public string wordWouldGenerate;
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
        Score.Add('A', 20);
        Score.Add('B', 100);
        Score.Add('C', 150);
        Score.Add('Ç', 200);
        Score.Add('D', 50);
        Score.Add('E', 20);
        Score.Add('F', 200);
        Score.Add('G', 200);
        Score.Add('Ğ', 200);
        Score.Add('H', 150);
        Score.Add('I', 100);
        Score.Add('İ', 20);
        Score.Add('J', 200);
        Score.Add('K', 50);
        Score.Add('L', 20);
        Score.Add('M', 50);
        Score.Add('N', 20);
        Score.Add('O', 100);
        Score.Add('Ö', 200);
        Score.Add('P', 150);
        Score.Add('R', 50);
        Score.Add('S', 50);
        Score.Add('Ş', 150);
        Score.Add('T', 50);
        Score.Add('U', 100);
        Score.Add('Ü', 150);
        Score.Add('V', 150);
        Score.Add('Y', 100);
        Score.Add('Z', 100);
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

    public char GenerateLetter()
    {
        if (wordWouldGenerate==string.Empty)
        {
            int randomLetterIndex = Random.Range(0, 29);
            int randomLength = Random.Range(3, 9);
            while (randomLetterIndex==8)
            {
                randomLetterIndex = Random.Range(0, 29);
            }
            char randomChar = letters[randomLetterIndex];

            wordWouldGenerate = words.RandomWord(randomChar, randomLength);   
            Debug.Log(wordWouldGenerate);
            ShuffleWord();
            
        }

        char returnChar = wordWouldGenerate[0];
        wordWouldGenerate = wordWouldGenerate.Remove(0, 1);
        return returnChar;
    }

    public void ShuffleWord()
    {
        char[] characters = wordWouldGenerate.ToCharArray();
        
        for (int i = 0; i < wordWouldGenerate.Length; i++)
        {
            int randomIndex = Random.Range(0,wordWouldGenerate.Length);
            char tmp;
            tmp = characters[i];
            characters[i] = characters[randomIndex];
            characters[randomIndex] = tmp;
        }

        wordWouldGenerate = new string(characters);
    }

    public int GetScore(char ch)
    {
        return Score[ch];
    }

    public void Reset()
    {
        wordWouldGenerate = string.Empty;
    }
}