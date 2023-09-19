using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    public int score;
    void Start()
    {
       ResetScore();
    }

    private void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();

    }
    private void AddScore(int scr)
    {
        scoreText.text = score.ToString();
        scoreText.GetComponent<Transform>().DOScale(1.4f, 0.3f).OnComplete((() =>
        {
            scoreText.GetComponent<Transform>().DOScale(1, 0.7f);
        }));
        DOVirtual.Int(score, score + scr, 1f, score =>
        {
            scoreText.text = score.ToString();
        });
        score += scr;
        
        Messenger<int>.Broadcast(GameEvent.SEND_SCORE,score);
    }

    private void SendScoreToLeaderboard()
    {        Debug.Log("score sent to leaderboard");

        Messenger<int>.Broadcast(GameEvent.SEND_SCORE_TO_LEADERBOARD,score);
    }
    
    
    
    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.ADD_SCORE,AddScore);
        Messenger.AddListener(GameEvent.START_GAME,ResetScore);
        Messenger.AddListener(GameEvent.GAME_OVER,SendScoreToLeaderboard);
    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.ADD_SCORE,AddScore);
        Messenger.RemoveListener(GameEvent.START_GAME,ResetScore);
        Messenger.RemoveListener(GameEvent.GAME_OVER,SendScoreToLeaderboard);
    }
}
