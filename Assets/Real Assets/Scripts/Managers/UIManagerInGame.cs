using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private TMP_Text leaderboardText;
    [SerializeField] private LeaderboardDatabase leaderboardData;
    [SerializeField] private GameObject leaderboardPopup;
    public void StartGame()
    {
        Messenger.Broadcast(GameEvent.START_GAME);
        holder.SetActive(false);
    }

    public void GameOver()
    {
        holder.SetActive(true);
    }

    public void SetLeaderboardText()
    {
        string text = string.Empty;
        for (int i = 0; i < leaderboardData.leaderboardInfo.Count; i++)
        {
            text += $"{i+1}. {leaderboardData.leaderboardInfo[i].name} ---> {leaderboardData.leaderboardInfo[i].score} \n";
        }

        leaderboardText.text = text;
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
