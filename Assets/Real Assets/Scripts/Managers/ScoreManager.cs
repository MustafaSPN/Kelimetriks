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
    }
    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.ADD_SCORE,AddScore);
        Messenger.AddListener(GameEvent.START_GAME,ResetScore);
    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.ADD_SCORE,AddScore);
        Messenger.AddListener(GameEvent.START_GAME,ResetScore);
    }
}
