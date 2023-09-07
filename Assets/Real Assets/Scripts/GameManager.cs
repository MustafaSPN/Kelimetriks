using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Grid grid;
    [SerializeField] public PastelColors randomColor;
    
    int width = 6;
    int height = 10;
    public int clickCount = 0;
    private void Awake()
    {
        Initializer();
    }

    void Start()
    {
        StartCoroutine(GenerateFirstLetters());
    }

   

    public void Initializer()
    { 
        grid.InitializeGrid(width,height);
        randomColor.InitializeColors();
        

    }

    public IEnumerator GenerateFirstLetters()
    {
        for (int i = 0; i < 12; i++)
        {
            Messenger.Broadcast(GameEvent.GENERATE_LETTER);
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void DestroyLetters()
    {
        Messenger.Broadcast(GameEvent.DESTROY_LETTER);
    }
    public void ClickedLetter(char ch,GameObject obj)
    {
        if (clickCount < 10)
        {
            Messenger<char>.Broadcast(GameEvent.ADD_LETTER_TO_WORD,ch);
            Messenger<GameObject>.Broadcast(GameEvent.MOVE_CLICKED_LETTER_HIDE,obj);
            clickCount++;
        }
        else
        {
            Messenger<GameObject>.Broadcast(GameEvent.SHAKE_LETTERS,obj);
        }
    }

    public void CrossButtonPressed()
    {
        Messenger.Broadcast(GameEvent.EMPTY_WORD);
        Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
        clickCount = 0;
    }

    public void CheckButtonPressed()
    {
        Debug.Log(clickCount);
        if (clickCount > 2)
        {
            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.DESTROY_LETTER);
            clickCount = 0;
        }
        else
        {
            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
            clickCount = 0;
        }

    }
    private void OnEnable()
    {
       Messenger<char,GameObject>.AddListener(GameEvent.CLICKED_LETTER,ClickedLetter);
       Messenger.AddListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
       Messenger.AddListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
    }

    private void OnDisable()
    {
        Messenger<char,GameObject>.AddListener(GameEvent.CLICKED_LETTER,ClickedLetter);
        Messenger.RemoveListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
        Messenger.RemoveListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
    }
}
