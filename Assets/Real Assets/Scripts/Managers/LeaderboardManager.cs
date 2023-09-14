using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private DatabaseReference leaderboardReference;
    private DatabaseReference usernameReference;
    [SerializeField] private LeaderboardDatabase leaderboard;
    public List<PlayerData> LeaderboardData;
    private FirebaseUser user;
    private string username;
    private void Start()
    {
        leaderboard.ClearOldData();
        leaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        leaderboardReference.ValueChanged += HandleValueChange;
        GetLeaderboard();
        Messenger.Broadcast(GameEvent.REQUEST_USER);
        
        
        

    }

    public void HandleValueChange(object a,ValueChangedEventArgs b)
    {
     GetLeaderboard();   
    }
    
    public void GetLeaderboard()
    {
        leaderboardReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                leaderboard.ClearOldData();
                LeaderboardData.Clear();
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < 20; i++)
                {
                    string name = snapshot.Child("names").Child(i.ToString()).Value.ToString();
                    int score = int.Parse(snapshot.Child("scores").Child(i.ToString()).Value.ToString());
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
        usernameReference.Child("users").Child(user.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot.Value);
                
            }
        });

    }

     private void CheckLeaderboard(int scr)
    {
    //     int index = -1;
    //     for (int i = 0; i < LeaderboardData.Count; i++)
    //     {
    //         if (LeaderboardData[i].score < scr )
    //         {
    //             index = i;
    //             break;
    //         }
    //     }
    //
    //     if (index>0)
    //     {
    //         string tmpName = LeaderboardData[index].name;
    //         int tmpScore = LeaderboardData[index].score;
    //         LeaderboardData[index].name = 
    //         
    //         for (int i = 0; i < UPPER; i++)
    //         {
    //             
    //         }
    //         
    //
    //
    //     }
    //     
    //     
     }

    private void SetUser(FirebaseUser u)
    {
        user = u;
        GetUsername();
    }
    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.SEND_SCORE_TO_LEADERBOARD,CheckLeaderboard);
        Messenger<FirebaseUser>.AddListener(GameEvent.SENDING_USER,SetUser);

    }

    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.SEND_SCORE_TO_LEADERBOARD,CheckLeaderboard);
        Messenger<FirebaseUser>.RemoveListener(GameEvent.SENDING_USER,SetUser);
    }
}
