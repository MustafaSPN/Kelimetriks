using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Grid grid;
    [SerializeField] public PastelColors randomColor;
    [SerializeField] public WordData wordAsset;
    [SerializeField] public RandomLetters randomLetters;
    
    int width = 6;
    int height = 10;
    public int clickCount = 0; 
    public float timer = 5f;
    private void Awake()
    {
        Initializer();
    }

    void Start()
    {
        StartCoroutine(GenerateFirstLetters());
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <0)
        {
            Messenger.Broadcast(GameEvent.GENERATE_LETTER);
            timer = 3f;
        }
        
    }

    public void Initializer()
    { 
        grid.InitializeGrid(width,height);
        randomColor.InitializeColors();
        wordAsset.InitializeWords();
        randomLetters.Initiliaze();
    }

    public IEnumerator GenerateFirstLetters()
    {
        for (int i = 0; i < 12; i++)
        {
            Messenger.Broadcast(GameEvent.GENERATE_LETTER);
            yield return new WaitForSeconds(0.08f);
        }
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

    public void CheckWordIsExist(string word)
    {
        bool isExist = wordAsset.SearchWord(word);
        Debug.Log(word);
        if (isExist)
        {
            Debug.Log(isExist);

            Messenger.Broadcast(GameEvent.EMPTY_WORD);
             Messenger.Broadcast(GameEvent.DESTROY_CORRECT_LETTER);
        }
        else
        {
            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
        }

    }
    public void CheckButtonPressed()
    {
        if (clickCount > 2)
        {
            Messenger.Broadcast(GameEvent.REQUEST_WORD); 
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
       Messenger<string>.AddListener(GameEvent.RETURN_WORD,CheckWordIsExist);
    }

    private void OnDisable()
    {
        Messenger<char,GameObject>.RemoveListener(GameEvent.CLICKED_LETTER,ClickedLetter);
        Messenger.RemoveListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
        Messenger.RemoveListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
        Messenger<string>.RemoveListener(GameEvent.RETURN_WORD,CheckWordIsExist);
    }
}
