using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] public TMP_Text wordText;

    private void Start()
    {
        wordText.text = "";
    }

    private void OnEnable()
    {
        Messenger<char>.AddListener(GameEvent.ADD_LETTER_TO_WORD,AddLetterToWorld);
        Messenger.AddListener(GameEvent.EMPTY_WORD,CancelWord);
    }

    private void OnDisable()
    {
        Messenger<char>.RemoveListener(GameEvent.ADD_LETTER_TO_WORD,AddLetterToWorld);
        Messenger.AddListener(GameEvent.EMPTY_WORD,CancelWord);
    }

    public void CancelWord()
    {
        wordText.text = "";
    }

    public void AddLetterToWorld(char ch)
    {
        wordText.text += ch.ToString();
    }
}
