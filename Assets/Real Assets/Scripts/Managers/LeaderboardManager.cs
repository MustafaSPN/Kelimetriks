using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    private DatabaseReference leaderboardReference;
    private DatabaseReference usernameReference;
    [SerializeField] private LeaderboardDatabase leaderboard;
    public List<PlayerData> LeaderboardData;
    private string username;
    [SerializeField]private AuthUser scriptableUser;
    private void Start()
    {
        leaderboard.ClearOldData();
        leaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        leaderboardReference.ValueChanged += HandleValueChange;
        GetLeaderboard();
       GetUsername();
    }

    public void HandleValueChange(object a,ValueChangedEventArgs b)
    {
     GetLeaderboard();   
    }

    public void GetLeaderboard()
    {
        leaderboardReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                leaderboard.ClearOldData();
                LeaderboardData.Clear();
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < 20; i++)
                {
                    string name = snapshot.Child(i.ToString()).Child("name").Value.ToString();
                    int score = int.Parse(snapshot.Child(i.ToString()).Child("score").Value.ToString());
                    PlayerData obj = new PlayerData(name, score);
                    LeaderboardData.Add(obj);
                }

                leaderboard.AddUserData(LeaderboardData);
                Messenger.Broadcast(GameEvent.SET_LEADERBOARD_TEXT);
            }
        });


        }
    

    public void GetUsername()
    {
        usernameReference = FirebaseDatabase.DefaultInstance.RootReference;
        usernameReference.Child("users").Child(scriptableUser.GetUserId()).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                username = snapshot.Value.ToString();
            }
        });
    
    }

    private void CheckLeaderboard(int scr)
    {
        int index = -1;
        for (int i = 0; i < LeaderboardData.Count; i++)
        {
            if (LeaderboardData[i].score < scr)
            {
                index = i;
                break;
            }
        }
    
        if (index>-1)
        
        {
            for (int i = 1; i < LeaderboardData.Count-index; i++)
        {
            LeaderboardData[LeaderboardData.Count - i ].name = LeaderboardData[LeaderboardData.Count - i-1].name;
            LeaderboardData[LeaderboardData.Count - i ].score = LeaderboardData[LeaderboardData.Count - i-1].score;
    
        }
        LeaderboardData[index].name = username;
        LeaderboardData[index].score = scr;

        WriteToFirebaseLeaderboardData();
        }
    }

    private void WriteToFirebaseLeaderboardData()
    {
        for (int i = 0; i < 20; i++)
        {
            string json = JsonUtility.ToJson(LeaderboardData[i]);
            leaderboardReference.Child(i.ToString()).SetRawJsonValueAsync(json);
        }
    }


    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.SEND_SCORE_TO_LEADERBOARD,CheckLeaderboard);

    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.SEND_SCORE_TO_LEADERBOARD,CheckLeaderboard);
    }
}
