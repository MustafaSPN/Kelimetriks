using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;

[CreateAssetMenu(fileName = "AuthUser",menuName = "ScriptableObjects/AuthUser")]

public class AuthUser : ScriptableObject
{
    public string username;
    private FirebaseAuth auth;
    private FirebaseUser user;

    public void InitializeUser(FirebaseAuth au)
    {
        auth = au;
        user = au.CurrentUser;
    }

    public void ResetUser()
    {
        auth = null;
        user = null;
        username = null;

    }

    public string GetUserId()
    {
        return user.UserId;
    }

    public void setUsername(string name)
    {
        username = name;
    }

    public string getUsername()
    {
        return username;
    }
}
