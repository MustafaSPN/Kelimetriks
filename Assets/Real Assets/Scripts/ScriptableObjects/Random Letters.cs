    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [CreateAssetMenu(fileName = "LettersObject", menuName = "ScriptableObjects/RandomLetters")]
public class RandomLetters : ScriptableObject
{
    [SerializeField] public char[] letters = new char[29];
    [SerializeField] private int[] letterCounts = {
        25427, 2743, 2181, 806, 4907, 10977, 1645, 1317, 412, 2123, 9599, 3571, 265, 7433,
        7873, 7109, 9280, 5280, 552, 2419, 7844, 7112, 1030, 6701, 8026, 255, 1694, 4829, 3319
    };
    public float[] letterProbabilities;
    public int totalLetterCount = 0;
    public Dictionary<char, int> Score = new Dictionary<char, int>();
    public Words words;
    public string wordWouldGenerate;
    
    public void Initiliaze()
    {
        totalLetterCount = 0;
        foreach (var t in letterCounts)
        {
            totalLetterCount += t;
        }
        // Harf olasılıklarını hesapla ve toplamı 1 yap
        letterProbabilities = new float[letters.Length];
        for (int i = 0; i < letterCounts.Length; i++)
        {
            letterProbabilities[i] = (float)letterCounts[i] / totalLetterCount;
        }
        Score.Add('A', 20); Score.Add('B', 100); Score.Add('C', 150); Score.Add('Ç', 200);
        Score.Add('D', 50); Score.Add('E', 20); Score.Add('F', 200); Score.Add('G', 200);
        Score.Add('Ğ', 200); Score.Add('H', 150); Score.Add('I', 100); Score.Add('İ', 20);
        Score.Add('J', 200); Score.Add('K', 50); Score.Add('L', 20); Score.Add('M', 50);
        Score.Add('N', 20); Score.Add('O', 100); Score.Add('Ö', 200); Score.Add('P', 150);
        Score.Add('R', 50); Score.Add('S', 50); Score.Add('Ş', 150); Score.Add('T', 50);
        Score.Add('U', 100); Score.Add('Ü', 150); Score.Add('V', 150); Score.Add('Y', 100);
        Score.Add('Z', 100);
    }

    public char GenerateRandomLetter()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        float cumulativeProbability = 0.0f;
        for (int i = 0; i < letterProbabilities.Length; i++)
        {
            cumulativeProbability += letterProbabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return letters[i];
            }
        }
        return letters[0];
    }

    public char GenerateLetter()
    {
        if (wordWouldGenerate==string.Empty)
        {
            int randomLetterIndex = Random.Range(0, 29);
            int randomLength = Random.Range(3, 6);
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