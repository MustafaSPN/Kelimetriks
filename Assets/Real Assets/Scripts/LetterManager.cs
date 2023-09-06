using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;

public class LetterManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] public Grid grid;
    [SerializeField] public PastelColors randomColor;
    
    [Header("Prefabs")]
    [SerializeField] public GameObject[] prefab;
    private void Start()
    {

        StartCoroutine(FirstLettersGenerate());
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.GENERATE_LETTER,GenerateRandomLetterObject);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GENERATE_LETTER,GenerateRandomLetterObject);
    }

    public void GenerateRandomLetterObject()
    {
        GameObject newObject = Instantiate(prefab[Random.Range(0, 3)]);
        newObject.GetComponent<SpriteRenderer>().color = randomColor.GenerateRandomColor();
        

    }


    public IEnumerator FirstLettersGenerate()
    {
        for (int i = 0; i < 12; i++)
        {
            GenerateRandomLetterObject();
            yield return new WaitForSeconds(0.1f);

        }
    }
}
