using System;
using System.Collections;
using System.Collections.Generic;
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
        Messenger<string>.AddListener(GameEvent.SENDING_USER,SetText);
    }

    private void OnDisable()
    {
        Messenger<string>.RemoveListener(GameEvent.SENDING_USER,SetText);
    }


    private void SetText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
