using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioSource source;

    public void PlayMusic()
    {
        source.PlayOneShot(backgroundMusic);
    }

    public void StopMusic()
    {
        source.Stop();
    }
    
    
    
    
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PLAY_BACKGROUND_MUSIC,PlayMusic);
        Messenger.AddListener(GameEvent.STOP_BACKGROUND_MUSIC,StopMusic);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PLAY_BACKGROUND_MUSIC,PlayMusic);
        Messenger.RemoveListener(GameEvent.STOP_BACKGROUND_MUSIC, StopMusic);
    }
}
