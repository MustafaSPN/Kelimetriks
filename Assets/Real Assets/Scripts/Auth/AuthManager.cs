using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    private FirebaseAuth auth;
    private FirebaseUser user;
    private DatabaseReference reference;
    [SerializeField]private AuthUser scriptableUser;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.Log($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            auth.SignOut();
        });
       
    }

    



    private void Login(string _email, string _password)
    {
        auth.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            
            scriptableUser.InitializeUser(auth);
            GoGameScene();

        });



    }

    private void Register(string _email, string _password,string _username)
    {
        
            auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
                reference.Child("users").Child(auth.CurrentUser.UserId).SetValueAsync(_username);
                auth.SignOut();
                Messenger.Broadcast(GameEvent.REGISTER_COMPLATED);
            });
           
            
        
    }

    public void LogOut()
    {
        auth.SignOut();
        PlayerPrefs.SetString("email","");
        PlayerPrefs.SetString("password","");
        PlayerPrefs.SetInt("ExDate",0);
    }

    private void OnEnable()
    {
        Messenger<string, string>.AddListener(GameEvent.LOG_IN, Login);
        Messenger<string, string,string>.AddListener(GameEvent.REGISTER, Register);
        Messenger.AddListener(GameEvent.LOG_OUT,LogOut);
    }

    private void OnDisable()
    {
        Messenger<string, string>.RemoveListener(GameEvent.LOG_IN, Login);
        Messenger<string, string,string>.RemoveListener(GameEvent.REGISTER, Register);
        Messenger.RemoveListener(GameEvent.LOG_OUT,LogOut);
    }
    
    private void GoGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}