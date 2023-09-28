using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderboardData",menuName = "ScriptableObjects/LeaderboardDatabase")]

public class LeaderboardDatabase : ScriptableObject
{
    public List<PlayerData> leaderboardInfo;

    public void ClearOldData()
    {
        leaderboardInfo.Clear();
    }
    
    public void AddUserData(List<PlayerData> userData)
    {
        leaderboardInfo = userData;
    }

    public List<PlayerData> GetLeaderboardInfo()
    {
        return leaderboardInfo;
    }
    }

    [Serializable]
    public class PlayerData
    {
        public string name;
        public int score;

        public PlayerData(string n, int s)
        {
            name = n;
            score = s;
        }
    }


