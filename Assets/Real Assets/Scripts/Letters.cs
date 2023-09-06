using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LettersObject",menuName = "ScriptableObjcets/Letters")]

public class Letters : ScriptableObject
{

    [SerializeField]    public char[] letters = new char[29];


    public char GenerateRandomLetter()
    {
        return letters[Random.Range(0, 29)];
    }
}
