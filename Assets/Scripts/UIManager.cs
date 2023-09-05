using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject welcomePanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    public TMP_InputField registerEmail;
    public TMP_InputField registerPassword1;
    public TMP_InputField registerPassword2;


    private void Start()
    {
        welcomePanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void LoginButtonWelcomePressed()
    {
        welcomePanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void RegisterButtonWelcomePressed()
    {
        welcomePanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void BackButtonPressed()
    {
        welcomePanel.SetActive(true);
        registerPanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    public void LoginButtonPressedForLogin()
    {
        Messenger<string,string>.Broadcast(GameEvent.LOG_IN,loginEmail.text,loginPassword.text);
    }

    public void RegisterButtonPressedForRegister()
    {
        if (registerPassword1.text == registerPassword2.text)
        {
            Messenger<string,string>.Broadcast(GameEvent.REGISTER,registerEmail.text,registerPassword1.text);           
        }
        else
        {
            Debug.Log("Passwords are not match.");  
        }
    
    }

}
