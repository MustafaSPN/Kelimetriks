using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] private TMP_Text wordText;
    void Start()
    {
        wordText.text = "";
    }

    private void OnEnable()
    {
        Messenger<string>.AddListener(GameEvent.TOUCHED_LETTER,AddLetterToText);
    }

    private void OnDisable()
    {
        Messenger<string>.RemoveListener(GameEvent.TOUCHED_LETTER,AddLetterToText);
    }

    public void AddLetterToText(string letter)
    {
        wordText.text += letter;
    }
}
