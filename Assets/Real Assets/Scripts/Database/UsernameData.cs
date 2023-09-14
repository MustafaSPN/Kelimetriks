using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class UsernameData : MonoBehaviour
{
    private DatabaseReference reference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            Debug.Log("Setting up Firebase Auth.");
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });

    }

    private string username;

    private void SetUsername(string usern)
    {
        username = usern;
        Debug.Log($"{username}");
    }

    private void SendUserToDatabase(FirebaseUser user)
    {
        Debug.Log($"{user.UserId}");
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
