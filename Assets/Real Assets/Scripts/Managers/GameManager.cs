using System.Collections;
using Firebase.Analytics;
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
    [SerializeField] private AdsInitializer initializerAds;
    int width = 6;
    int height = 10;
    public int clickCount = 0; 
    public float timer = 4f;
    public bool isGameContinue;
    public int wrongAnswers = 0;
    public int score = 0;
    public int correctWordCount = 0;
    public int wrongWordCount = 0;
    public int letterCount = 0;
    public int jokerCount = 0;
    public int crossLetterCount = 0;
    private bool isAdsShowed = false;
    
    private void Awake()
    {
        Initializer();
        isGameContinue = false;
    }
    
    private void Update()
    {
        if (!isGameContinue) return;
        timer -= Time.deltaTime;
        if (!(timer < 0)) return;
        timer = score switch
        {
            < 1000 => 4f,
            < 2500 => 3f,
            < 5000 => 2f,
            < 10000 => 1f,
            > 10000 => 0.5f,
            _ => timer
        };
        Messenger.Broadcast(GameEvent.GENERATE_LETTER);
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
        if (!isGameContinue) return;
        if (clickCount < 6)
        {
            Messenger<char>.Broadcast(GameEvent.ADD_LETTER_TO_WORD, ch);
            Messenger<GameObject>.Broadcast(GameEvent.MOVE_CLICKED_LETTER_HIDE, obj);
            letterCount++;
            clickCount++;
        }
        else
        {
            Messenger<GameObject>.Broadcast(GameEvent.SHAKE_LETTERS, obj);
        }
    }

    public void CrossButtonPressed()
    {
        if (!isGameContinue) return;
        Messenger.Broadcast(GameEvent.EMPTY_WORD);
        Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
        clickCount = 0;
    }

    public void CheckWordIsExist(string word)
    {
        if (!isGameContinue) return;
        bool isExist = wordAsset.SearchWord(word);
        if (isExist)
        {
            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.DESTROY_CORRECT_LETTER);
            Messenger.Broadcast(GameEvent.PLAY_CORRECT_ANSWER);
            Messenger<string>.Broadcast(GameEvent.CORRECT_WORD,word);
            correctWordCount++;
            wrongAnswers++;
            switch (wrongAnswers)
            {
                case 4:
                    Messenger.Broadcast(GameEvent.JOKER_LETTER_GENERATE);
                    jokerCount++;
                    wrongAnswers = 0;
                    break;
                case < 0:
                    wrongAnswers = 0;
                    break;
            }
        }
        else
        {
            Messenger.Broadcast(GameEvent.PLAY_WRONG_ANSWER);
            Messenger.Broadcast(GameEvent.EMPTY_WORD);
            Messenger.Broadcast(GameEvent.MOVE_CLICKED_LETTER_BACK);
            wrongWordCount++;
            switch (wrongAnswers)
            {
                case > 0:
                    wrongAnswers = 0;
                    break;
                case <= 0:
                    wrongAnswers--;
                    break;
            }
            if (wrongAnswers == -4)
            {
                wrongAnswers = 0;
                Messenger.Broadcast(GameEvent.CROSS_LETTER_GENERATE);
                crossLetterCount++;
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
    
    public void CheckButtonPressed()
    {
        if (!isGameContinue) return;
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

    public void GameOver()
    {
        isGameContinue = false;
        SendAnalytics();
        initializerAds.InitializeAds();
    }
    
    public void SendAnalytics() {
        string eventName = "Gameplay_Info";
        Parameter[] parameters ={
            new Parameter("Score", score),
            new Parameter("Wrong_Answers", wrongWordCount),
            new Parameter("Correct_Answers", correctWordCount),
            new Parameter("Letters", letterCount),
            new Parameter("Joker_Letter", jokerCount),
            new Parameter("Cross_Letters", crossLetterCount)
        };
        FirebaseAnalytics.LogEvent(eventName, parameters);
    }
    
    public void StartGame()
    {
        isGameContinue = true;
        isAdsShowed = false;
        clickCount = 0;
        wrongAnswers = 0;
        timer = 5f;
        score = 0;
        correctWordCount = 0;
        wrongWordCount = 0;
        jokerCount = 0;
        letterCount = 0;
        crossLetterCount = 0;
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
        switch (score)
        {
            case < 1000:
                wordAsset.SetSegment(0);
                break;
            case < 2500:
                wordAsset.SetSegment(1);
                break;
            case < 5000:
                wordAsset.SetSegment(2);
                break;
            default:
                wordAsset.SetSegment(3);
                break;
        }
    }

    private void ShowAd()
    {
        if (!isAdsShowed)
        {
            Messenger.Broadcast(GameEvent.SHOW_ADS_BUTTON);
            isAdsShowed = true;
            PauseGame();
        }
        else
        {
            Messenger.Broadcast(GameEvent.GAME_OVER);
        }
    }

    public void PauseGame()
    {
        isGameContinue = false;
    }

    public void Rewarded()
    {
        isGameContinue = true;
        clickCount = 0;
        wrongAnswers = 0;
        redCircle1.SetActive(false);
        redCircle2.SetActive(false);
        redCircle3.SetActive(false);
        greenCircle1.SetActive(false);
        greenCircle2.SetActive(false);
        greenCircle3.SetActive(false);
        StartCoroutine(GenerateFirstLetters());
    }
    
    public void StartContinueGame()
    {
        StartCoroutine(ContinueGame());
    }

    public IEnumerator ContinueGame()
    {
        yield return new WaitForSeconds(1f);
        isGameContinue = true;
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
       Messenger.AddListener(GameEvent.WAIT_FOR_ADS,ShowAd);
       Messenger.AddListener(GameEvent.REWARDED_ADS,Rewarded);
       Messenger.AddListener(GameEvent.PAUSE_GAME,PauseGame);
       Messenger.AddListener(GameEvent.CONTINUE_GAME,StartContinueGame);
    }

    private void OnDisable()
    {
        Messenger<char,GameObject>.RemoveListener(GameEvent.CLICKED_LETTER,ClickedLetter);
        Messenger.RemoveListener(GameEvent.CROSS_BUTTON_PRESSED,CrossButtonPressed);
        Messenger.RemoveListener(GameEvent.CHECK_BUTTON_PRESSED,CheckButtonPressed);
        Messenger<string>.RemoveListener(GameEvent.RETURN_WORD,CheckWordIsExist);
        Messenger.RemoveListener(GameEvent.GAME_OVER,GameOver);
        Messenger.RemoveListener(GameEvent.START_GAME,StartGame);
        Messenger<int>.RemoveListener(GameEvent.SEND_SCORE,SetScore);
        Messenger.RemoveListener(GameEvent.WAIT_FOR_ADS,ShowAd);
        Messenger.RemoveListener(GameEvent.REWARDED_ADS,Rewarded);
        Messenger.RemoveListener(GameEvent.PAUSE_GAME,PauseGame);
        Messenger.RemoveListener(GameEvent.CONTINUE_GAME,StartContinueGame);
    }
}
