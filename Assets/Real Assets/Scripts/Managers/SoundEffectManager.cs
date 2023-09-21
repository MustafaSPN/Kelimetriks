using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public bool isPlayEffects = true;
    [SerializeField] private AudioClip correctAnswer;
    [SerializeField] private AudioClip crossLetter;
    [SerializeField] private AudioClip generateLetter;
    [SerializeField] private AudioClip harfBingildarken;
    [SerializeField] private AudioClip jokerCalisirken;
    [SerializeField] private AudioClip jokerDusunce;
    [SerializeField] private AudioClip scoreRising;
    [SerializeField] private AudioClip wrongAnswer;
    [SerializeField] private AudioSource source;





    private void SetIsPlayEffects(bool bl)
    {
        isPlayEffects = bl;
    }

    public void PlayCrossLetters()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(crossLetter);

        }
    }

    public void PlayGenerateLetters()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(generateLetter);

        }
    }

    public void PlayCorrectAnswer()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(correctAnswer);

        }
    }

    public void PlayHarflerBingildarken()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(harfBingildarken);

        }
    }

    public void PlayJokerCalisirken()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(jokerCalisirken);

        }
    }

    public void PlayJokerDuserken()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(jokerDusunce);

        }
    }

    public void PlayScoreRising()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(scoreRising);

        }
    }

    public void PlayWrongAnswer()
    {
        if (isPlayEffects)
        {
            source.PlayOneShot(wrongAnswer);

        }
    }
    private void OnEnable()
    {
        Messenger<bool>.AddListener(GameEvent.PLAY_SOUND_EFFECTS,SetIsPlayEffects);
        
            Messenger.AddListener(GameEvent.PLAY_CROSS_LETTERS,PlayCrossLetters);
            Messenger.AddListener(GameEvent.PLAY_GENERATE_LETTERS,PlayGenerateLetters);
            Messenger.AddListener(GameEvent.PLAY_CORRECT_ANSWER,PlayCorrectAnswer);
            Messenger.AddListener(GameEvent.PLAY_HARFLER_BINGILDARKEN,PlayHarflerBingildarken);
            Messenger.AddListener(GameEvent.PLAY_JOKER_CALISIRKEN,PlayJokerCalisirken);
            Messenger.AddListener(GameEvent.PLAY_JOKER_DUSERKEN,PlayJokerDuserken);
            Messenger.AddListener(GameEvent.PLAY_SCORE_RISING,PlayScoreRising);
            Messenger.AddListener(GameEvent.PLAY_WRONG_ANSWER,PlayWrongAnswer);
        
     
    }

    private void OnDisable()
    {
        Messenger<bool>.RemoveListener(GameEvent.PLAY_SOUND_EFFECTS,SetIsPlayEffects);
        
            Messenger.RemoveListener(GameEvent.PLAY_CROSS_LETTERS,PlayCrossLetters);
            Messenger.RemoveListener(GameEvent.PLAY_GENERATE_LETTERS,PlayGenerateLetters);
            Messenger.RemoveListener(GameEvent.PLAY_CORRECT_ANSWER,PlayCorrectAnswer);
            Messenger.RemoveListener(GameEvent.PLAY_HARFLER_BINGILDARKEN,PlayHarflerBingildarken);
            Messenger.RemoveListener(GameEvent.PLAY_JOKER_CALISIRKEN,PlayJokerCalisirken);
            Messenger.RemoveListener(GameEvent.PLAY_JOKER_DUSERKEN,PlayJokerDuserken);
            Messenger.RemoveListener(GameEvent.PLAY_SCORE_RISING,PlayScoreRising);
            Messenger.RemoveListener(GameEvent.PLAY_WRONG_ANSWER,PlayWrongAnswer); 
        
        
    }
}
