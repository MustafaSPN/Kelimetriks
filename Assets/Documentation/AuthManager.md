## AuthManager
> 
### Fields
```cs
AuthUser scriptableUser
```
```cs
DependencyStatus dependencyStatus
```
```cs
FirebaseAuth auth
```
```cs
FirebaseUser user
```
```cs
DatabaseReference reference
```
```cs
FirebaseApp app
```

### Methods
```cs
IEnumerator CheckAndFixDependancies
```
```cs
Void InitializeFirebase
```
> Initialize all Firebase Features
```cs
IEnumerator CheckAutoLogin
```
> Checking for enable auto login and call AutoLogin method
```cs
Void AutoLogin
```
> Auto log in and call GogGameScene method
```cs
Void AuthStateChanged
```
> Handle auth state change cases
```cs
Void Login
```
> Need two parameter: string email, string password. Then login and call GoGameSceneMethod
```cs
Void Register
```
> Need three parameter: string email, string password and string username. Then navigate to Login Screen
```cs
Void LogOut
```
```cs
Void GoGameScene
```
> Load Game Scene

