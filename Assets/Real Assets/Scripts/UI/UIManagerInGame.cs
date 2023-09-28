using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject leaderboardPopup;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private LeaderboardDatabase leaderboardData;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle soundEffectToggle;
    [SerializeField] private Toggle backgroundMusicToggle;
    [SerializeField] private TMP_Text username;
    [SerializeField] private AuthUser user;
    [SerializeField] private GameObject adsPanel;
    private List<GameObject> leaderboardTexts = new List<GameObject>();
    public bool isInGame = false;
   
    private void Start()
    {
        InitializeLeaderboardText();
    }

    public void StartGame()
    {
        Messenger.Broadcast(GameEvent.START_GAME);
        holder.SetActive(false);
        isInGame = true;
    }

    public void GameOver()
    {
        holder.SetActive(true);
        isInGame = false;
    }

    public void InitializeLeaderboardText()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject pre = Instantiate(prefab,parent.transform);
            pre.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100 * i);
            leaderboardTexts.Add(pre);
        }
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, leaderboardTexts.Count*100 + 100);
    }
    
    public void SetLeaderboardText()
    {
        for (int i = 0; i < 20; i++)
        {
            leaderboardTexts[i].transform.GetChild(0).GetComponent<TMP_Text>().text =(i+1)+ "." + leaderboardData.leaderboardInfo[i].name+":";
            leaderboardTexts[i].transform.GetChild(1).GetComponent<TMP_Text>().text =
                leaderboardData.leaderboardInfo[i].score.ToString();
        }
        username.SetText(user.getUsername());
    }

    public void OnValueChangedBackgroundMusicToggle()
    {
        Messenger.Broadcast(backgroundMusicToggle.isOn
            ? GameEvent.PLAY_BACKGROUND_MUSIC
            : GameEvent.STOP_BACKGROUND_MUSIC);
    }
    
    public void OnValueChangedSoundEffectToggle()
    {
        Messenger<bool>.Broadcast(GameEvent.PLAY_SOUND_EFFECTS, soundEffectToggle.isOn);
    }

    public void LogoutButton()
    {
        Messenger.Broadcast(GameEvent.LOG_OUT);
        SceneManager.LoadScene(0);
    }
    
    public void OpenSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(false);
    }
    
    public void OpenPausePanel()
    {
        if (isInGame)
        {
            pausePanel.gameObject.SetActive(true);    
        }
    }

    public void ClosePausePanel()
    {
        holder.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        Messenger.Broadcast(GameEvent.CONTINUE_GAME);
    }

    public void ExitGame()
    {
        Messenger.Broadcast(GameEvent.GAME_OVER);
        pausePanel.SetActive(false);
        isInGame = false;
    }
    
    public void PressedLeaderboardButton()
    {
        leaderboardPopup.SetActive(true);
    }

    public void PressedLeaderboardExitButton()
    {
        leaderboardPopup.SetActive(false);
    }

    public void ShowAdsPanel()
    {
        adsPanel.SetActive(true);
    }

    public void AdButtonClicked()
    {
        adsPanel.SetActive(false);
        Messenger.Broadcast(GameEvent.SHOW_ADS);
    }

    public void LoseButtonClicked()
    {
     Messenger.Broadcast(GameEvent.GAME_OVER);
     adsPanel.SetActive(false);
    }
    
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PAUSE_GAME,OpenPausePanel);
        Messenger.AddListener(GameEvent.GAME_OVER,GameOver);
        Messenger.AddListener(GameEvent.SET_LEADERBOARD_TEXT,SetLeaderboardText);
        Messenger.AddListener(GameEvent.SHOW_ADS_BUTTON,ShowAdsPanel);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PAUSE_GAME,OpenPausePanel);
        Messenger.RemoveListener(GameEvent.GAME_OVER,GameOver);
        Messenger.RemoveListener(GameEvent.SET_LEADERBOARD_TEXT,SetLeaderboardText);
        Messenger.RemoveListener(GameEvent.SHOW_ADS_BUTTON,ShowAdsPanel);
    }
}
