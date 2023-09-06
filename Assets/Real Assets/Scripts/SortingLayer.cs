using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    [SerializeField] private Letters letters;
    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerName = "Letter";
        char harf = letters.GenerateRandomLetter();
        GetComponent<TMP_Text>().text =harf.ToString();
    }

   
}
