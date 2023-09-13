using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    public void StartGame()
    {
        Messenger.Broadcast(GameEvent.START_GAME);
        holder.SetActive(false);
    }

    public void GameOver()
    {
        holder.SetActive(true);
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.GAME_OVER,GameOver);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER,GameOver);
    }
}
