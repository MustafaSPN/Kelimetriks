using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;

[CreateAssetMenu(fileName = "AuthUser",menuName = "ScriptableObjects/AuthUser")]

public class AuthUser : ScriptableObject
{
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
        
    }

    public string GetUserId()
    {
        return user.UserId;
    }

}
