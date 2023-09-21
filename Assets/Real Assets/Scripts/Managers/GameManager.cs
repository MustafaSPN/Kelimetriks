using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Grid grid;
    [SerializeField] public PastelColors randomColor;
    [SerializeField] public Words wordAsset;
    [SerializeField] public RandomLetters randomLetters;

    [SerializeField] private GameObject redCircle1;
    [SerializeField] private GameObject redCircle2;
    [SerializeField] private GameObject redCircle3;
    
    [SerializeField] private GameObject greenCircle1;
    [SerializeField] private GameObject greenCircle2;
    [SerializeField] private GameObject greenCircle3;
    
    
    int width = 6;
    int height = 10;
    public int clickCount = 0; 
    public float timer = 4f;
    public bool isGameContinue;
    public int wrongAnswers = 0;
    public int score = 0;
    private void Awake()
    {
        Initializer();
        isGameContinue = false;
    }


    private void Update()
    {
        if (isGameContinue)
        {
         
            timer -= Time.deltaTime;
            if (timer <0)
            {
                if (score < 1000)
                {
                    timer = 4f;    
                }else if (score < 2500)
                {
                    timer = 3f;
                }else if (score<5000)
                {
                    timer = 2f;
                }
                
                Messenger.Broadcast(GameEvent.GENERATE_LETTER);
                
            }   
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
        if (isGameContinue)
        {


            if (clickCount < 6)
            {
                Messenger<char>.Broadcast(GameEvent.ADD_LETTER_TO_WORD, ch);
                Messenger<GameObject>.Broadcast(GameEvent.MOVE_CLICKED_LETTER_HIDE, obj);
                clickCount++;
            }
            else
            {
                Messenger<GameObject>.Broadcast(GameEvent.SHAKE_LETTERS, obj);
            }
        }
    }

    public void CrossButtonPressed()
    {
        if (isGameContinue)
        {


            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
            clickCount = 0;
        }
    }

    public void CheckWordIsExist(string word)
    {
        if (isGameContinue)
        {


            bool isExist = wordAsset.SearchWord(word);
            if (isExist)
            {
                Messenger.Broadcast(GameEvent.EMPTY_WORD);
                Messenger.Broadcast(GameEvent.DESTROY_CORRECT_LETTER);
                Messenger.Broadcast(GameEvent.PLAY_CORRECT_ANSWER);
                wrongAnswers++;
                if (wrongAnswers==4)
                {
                    Messenger.Broadcast(GameEvent.JOKER_LETTER_GENERATE);
                    wrongAnswers = 0;
                }
                else if (wrongAnswers <0)
                {
                    wrongAnswers = 0;
                }
                
                
                
                
                
                
            }
            else
            {
                Messenger.Broadcast(GameEvent.PLAY_WRONG_ANSWER);
                Messenger.Broadcast(GameEvent.EMPTY_WORD);
                Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);

                if (wrongAnswers>0)
                {
                    wrongAnswers = 0;
                }
                
                else if (wrongAnswers<=0)
                {
                    wrongAnswers--;
                    
                }

                if (wrongAnswers == -4)
                {
                    wrongAnswers = 0;
                    Messenger.Broadcast(GameEvent.CROSS_LETTER_GENERATE);
                }
                
                
              
                
            }
            switch (wrongAnswers)
            {
                case -1 : 
                    redCircle1.SetActive(true);
                    break;
                case -2 :
                    redCircle2.SetActive(true);
                    break;
                case -3:
                    redCircle3.SetActive(true);
                    break;
                case 1 : 
                    greenCircle1.SetActive(true);
                    break;
                case 2 :
                    greenCircle2.SetActive(true);
                    break;
                case 3:
                    greenCircle3.SetActive(true);
                    break;
                case 0:
                    redCircle1.SetActive(false);
                    redCircle2.SetActive(false);
                    redCircle3.SetActive(false);
                    greenCircle1.SetActive(false);
                    greenCircle2.SetActive(false);
                    greenCircle3.SetActive(false);
                    break;
            }
        }
    }
    public void CheckButtonPressed()
    {
        if (isGameContinue)
        {


            if (clickCount > 2)
            {
                Messenger.Broadcast(GameEvent.REQUEST_WORD);
                clickCount = 0;
            }
            else if (clickCount != 0)
            {
                Messenger.Broadcast(GameEvent.EMPTY_WORD);
                Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
                
                clickCount = 0;
            }
        }
    }

    public void GameOver()
    {
        isGameContinue = false;
     
    }

    public void StartGame()
    {
        isGameContinue = true;
        clickCount = 0;
        wrongAnswers = 0;
        timer = 5f;
        score = 0;
        redCircle1.SetActive(false);
        redCircle2.SetActive(false);
        redCircle3.SetActive(false);
        greenCircle1.SetActive(false);
        greenCircle2.SetActive(false);
        greenCircle3.SetActive(false);
        StartCoroutine(GenerateFirstLetters());
    }

    private void SetScore(int scr)
    {
        score = scr;
    }
    private void OnEnable()
    {
       Messenger<char,GameObject>.AddListener(GameEvent.CLICKED_LETTER,ClickedLetter);
       Messenger.AddListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
       Messenger.AddListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
       Messenger<string>.AddListener(GameEvent.RETURN_WORD,CheckWordIsExist);
       Messenger.AddListener(GameEvent.GAME_OVER,GameOver);
       Messenger.AddListener(GameEvent.START_GAME,StartGame);
       Messenger<int>.AddListener(GameEvent.SEND_SCORE,SetScore);
    }

    private void OnDisable()
    {
        
        Messenger<char,GameObject>.RemoveListener(GameEvent.CLICKED_LETTER,ClickedLetter);
        Messenger.RemoveListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
        Messenger.RemoveListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
        Messenger<string>.RemoveListener(GameEvent.RETURN_WORD,CheckWordIsExist);
        Messenger.RemoveListener(GameEvent.GAME_OVER,GameOver);
        Messenger.RemoveListener(GameEvent.START_GAME,StartGame);
        Messenger<int>.AddListener(GameEvent.SEND_SCORE,SetScore);
    }
}
