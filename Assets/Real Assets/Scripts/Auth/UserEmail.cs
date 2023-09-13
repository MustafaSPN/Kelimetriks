using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class UserEmail : MonoBehaviour
{
    
    void Start()
    {
        Messenger.Broadcast(GameEvent.REQUEST_USER);

    }

    private void OnEnable()
    {
        Messenger<FirebaseUser>.AddListener(GameEvent.SENDING_USER,SetText);
    }

    private void OnDisable()
    {
        Messenger<FirebaseUser>.RemoveListener(GameEvent.SENDING_USER,SetText);
    }


    private void SetText(FirebaseUser user)
    {
        GetComponent<TextMeshProUGUI>().text = user.Email;
    }
}
