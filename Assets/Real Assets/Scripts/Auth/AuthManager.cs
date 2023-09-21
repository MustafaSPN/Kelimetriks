using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;
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
#if UNITY_IOS
    QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#else
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 240;
#endif 

        Debug.Log($"(Auth)Awake functions called.");
        DontDestroyOnLoad(this);
        
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        StartCoroutine(CheckAndFixDependancies());
        Messenger.Broadcast(GameEvent.DONT_SHOW_WELCOME_SCENE);

    }

    private IEnumerator CheckAndFixDependancies()
    {
        var checkAndFixDepandanciesTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(predicate: () => checkAndFixDepandanciesTask.IsCompleted);
        var deepndancyResult = checkAndFixDepandanciesTask.Result;
        if (deepndancyResult==DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.Log($"(Auth)Could not resolve all Firebase dependencies: {dependencyStatus}");
        }
    }
    private void InitializeFirebase()
    {
        Debug.Log($"(Auth)InitializeFirebase functions called.");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            Debug.Log($"(Auth)FirebaseApp checkAndFixDependencies functions called.");
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this,null);
            
            StartCoroutine(CheckAutoLogin());
        });
        
    }

    private IEnumerator CheckAutoLogin()
    {
        Debug.Log($"checkautologin called");
        yield return new WaitForEndOfFrame();
        if (user!=null)
        {
            var reloadTask = user.ReloadAsync();
            yield return new WaitUntil(predicate: () => reloadTask.IsCompleted);
            Debug.Log($"checkautologin 2");
            AutoLogin();
        }
        else
        {
        Messenger.Broadcast(GameEvent.SHOW_WELCOME_SCENE);

        }
        
    }

    private void AutoLogin()
    {Debug.Log($"Autologin called");
        if (user!=null)
        {
            Debug.Log($"Autologin called2");
            scriptableUser.InitializeUser(auth);
            GoGameScene();
        }
    }
    
    
    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser!=null)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user!=null)
            {
                Debug.Log($"Signed Out.");
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log($"Signed In: {user.UserId}");
            }
        }
    }

    private void Login(string _email, string _password)
    {
        Debug.Log($"(Auth)Login function called email: {_email}, password:{_password}.");
        auth.SignInWithEmailAndPasswordAsync(_email, _password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("(Auth)SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("(Auth)SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            AuthResult result = task.Result;
            Debug.LogFormat("(Auth)User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
            
            scriptableUser.InitializeUser(auth);
            GoGameScene();

        });



    }

    private void Register(string _email, string _password,string _username)
    {
        Debug.Log($"(Auth)Login function called email: {_email}, password:{_password}, username:{_username}.");

        auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("(Auth)CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("(Auth)CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }
                
                // Firebase user has been created.
                AuthResult result = task.Result;
                Debug.LogFormat("(Auth)Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
                reference.Child("users").Child(auth.CurrentUser.UserId).SetValueAsync(_username);
                auth.SignOut();
                Messenger.Broadcast(GameEvent.REGISTER_COMPLATED);
            });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log($"(Auth)Logged out");
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