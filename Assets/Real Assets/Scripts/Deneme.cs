using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deneme : MonoBehaviour
{
    [SerializeField] public Grid grid;
    [SerializeField] public GameObject[] prefab;
    [SerializeField] public PastelColors randomColor;
    [SerializeField] public Letters letters;
    int width = 6;
    int height = 10;
    float timer = 1f;
    private void Start()
    {
        grid.InitializeGrid(width,height);
        randomColor.InitializeColors();
        GenerateRandomLetterObject();

    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<0)
        {
            GenerateRandomLetterObject();
            timer = 1f;
        }
        
    }

    private GameObject GenerateRandomLetterObject()
    {
        GameObject newObject = Instantiate(prefab[Random.Range(0, 3)]);
        newObject.GetComponent<SpriteRenderer>().color = randomColor.GenerateRandomColor();
        return newObject;

    }
}
