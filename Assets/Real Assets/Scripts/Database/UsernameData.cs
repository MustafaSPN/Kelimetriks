using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class UsernameData : MonoBehaviour
{
    private DatabaseReference reference;
    private string username;
    
    void Start()
    {
        Debug.Log($"(UsernameData) Start function called.");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var app = FirebaseApp.DefaultInstance;
            Debug.Log("Setting up Firebase Auth.");
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    private void SetUsername(string usern)
    {
        username = usern;
        Debug.Log($"{username}");
    }

    private void SendUserToDatabase(FirebaseUser user)
    {
        Debug.Log($"{user.UserId}");
        Debug.Log($"(UsernameData) username sent to database.");
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
