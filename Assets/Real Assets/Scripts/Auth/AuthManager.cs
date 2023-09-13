using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    

    private const string databaseUrl = "https://kelimetriks-default-rtdb.firebaseio.com/";

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
        Debug.Log($"Setting up Firebase Auth.");
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginButton(string email, string password)
    {
        StartCoroutine(Login(email, password));
    }

    public void RegisterButton(string email, string password)
    {
        StartCoroutine(Register(email, password));
    }


    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }

            Debug.Log(message);
        }
        else
        {
            user = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            Debug.Log($"Logged in");
            GoGameScene();
        }
    }

    private IEnumerator Register(string _email, string _password)
    {
        {
            Debug.Log($"{_email}, {_password}");
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }

                Debug.Log(message);
            }
            else
            {
                //User has now been created
                //Now get the result
                user = RegisterTask.Result.User;

                if (user != null)
                {
                    Debug.Log($"Email: {user.Email} Registered successfully. ");
                    Messenger<FirebaseUser>.Broadcast(GameEvent.SENDING_USER,user);
                    auth.SignOut();
                    Messenger.Broadcast(GameEvent.REGISTER_COMPLATED);
                }
            }
        }
    }


    private void OnEnable()
    {
        Messenger<string, string>.AddListener(GameEvent.LOG_IN, LoginButton);
        Messenger<string, string>.AddListener(GameEvent.REGISTER, RegisterButton);
        Messenger.AddListener(GameEvent.REQUEST_USER, SendingUser);
    }

    private void OnDisable()
    {
        Messenger<string, string>.RemoveListener(GameEvent.LOG_IN, LoginButton);
        Messenger<string, string>.RemoveListener(GameEvent.REGISTER, RegisterButton);
        Messenger.RemoveListener(GameEvent.REQUEST_USER, SendingUser);
    }

    private void SendingUser()
    {
        Messenger<FirebaseUser>.Broadcast(GameEvent.SENDING_USER, user);
    }

    private void GoGameScene()
    {
        SceneManager.LoadScene(1);
    }
}