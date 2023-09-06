using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomLetter : MonoBehaviour
{
    [SerializeField] private Letters letters;
    void Start()
    {
        char harf = letters.GenerateRandomLetter();
        GetComponent<TMP_Text>().text =harf.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
