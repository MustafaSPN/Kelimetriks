using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class UsernameData : MonoBehaviour
{
    private DatabaseReference reference;
    private string username;
    
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void SetUsername(string usern)
    {
        username = usern;
    }

    private void SendUserToDatabase(FirebaseUser user)
    {
        reference.Child("users").Child(user.UserId).SetValueAsync(username);

    }
    
    
    private void OnEnable()
    {
        Messenger<string>.AddListener(GameEvent.SENDING_USERNAME,SetUsername);
        Messenger<FirebaseUser>.AddListener(GameEvent.SENDING_USER,SendUserToDatabase);
    }

    private void OnDisable()
    {
        Messenger<string>.RemoveListener(GameEvent.SENDING_USERNAME,SetUsername);
        Messenger<FirebaseUser>.RemoveListener(GameEvent.SENDING_USER,SendUserToDatabase);
    }
}
