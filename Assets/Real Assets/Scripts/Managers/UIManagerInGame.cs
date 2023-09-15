using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject leaderboardPopup;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private LeaderboardDatabase leaderboardData;
    private List<GameObject> leaderboardTexts = new List<GameObject>();

    private void Start()
    {
        InitializeLeaderboardText();
    }

    public void StartGame()
    {
        Messenger.Broadcast(GameEvent.START_GAME);
        holder.SetActive(false);
    }

    public void GameOver()
    {
        holder.SetActive(true);
    }

    public void InitializeLeaderboardText()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject pre = Instantiate(prefab,parent.transform);
            pre.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100 * i);
            leaderboardTexts.Add(pre);
        }

        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, leaderboardTexts.Count*100);
    }
    
    
    public void SetLeaderboardText()
    {
        for (int i = 0; i < 20; i++)
        {
            leaderboardTexts[i].transform.GetChild(0).GetComponent<TMP_Text>().text =(i+1)+ "." + leaderboardData.leaderboardInfo[i].name+":";
            leaderboardTexts[i].transform.GetChild(1).GetComponent<TMP_Text>().text =
                leaderboardData.leaderboardInfo[i].score.ToString();
        }
       
    }

    public void LogoutButton()
    {
        Messenger.Broadcast(GameEvent.LOG_OUT);
        SceneManager.LoadScene(0);
    }
    
    

    public void PressedLeaderboardButton()
    {
        leaderboardPopup.SetActive(true);
    }

    public void PressedLeaderboardExitButton()
    {
        leaderboardPopup.SetActive(false);
    }
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.GAME_OVER,GameOver);
        Messenger.AddListener(GameEvent.SET_LEADERBOARD_TEXT,SetLeaderboardText);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER,GameOver);
        Messenger.RemoveListener(GameEvent.SET_LEADERBOARD_TEXT,SetLeaderboardText);
    }
}