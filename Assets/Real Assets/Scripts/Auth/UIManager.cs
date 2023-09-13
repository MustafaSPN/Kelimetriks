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
    public GameObject GamePanel;

    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;

    public TMP_InputField registerEmail;
    public TMP_InputField registerName;
    public TMP_InputField registerPassword1;
    public TMP_InputField registerPassword2;

    public Toggle staySign;
    private DateTime today;
    private string email;
    private string password;
    private int todayInt;
    private TMP_InputField[] inputs;
    private void Start() { 
        today = DateTime.Now;
        todayInt = today.Year * 10000 + today.Month * 100 + today.Day;
        if (Load())
        {
            welcomePanel.SetActive(false);
            loginPanel.SetActive(false);
            registerPanel.SetActive(false);
            GamePanel.SetActive(true);
        }else
        {
            welcomePanel.SetActive(true);
            loginPanel.SetActive(false);
            registerPanel.SetActive(false);
            GamePanel.SetActive(false);
        }

        inputs = new[]
        {
            loginEmail, loginPassword, registerEmail, registerName, registerPassword1, registerPassword2
        };


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
        foreach (var input in inputs)
        {
            input.text = string.Empty;
        }
        
    }

    public void LoginButtonPressedForLogin()
    {
        if (staySign.isOn)
        {
            Save();
        }
        Messenger<string, string>.Broadcast(GameEvent.LOG_IN, loginEmail.text, loginPassword.text);
        
    }

    public void Save()
    {
        DateTime exDate = DateTime.Now.AddDays(30);
        int exdateInt = exDate.Year * 10000 + exDate.Month * 100 + exDate.Day;
        PlayerPrefs.SetString("email",loginEmail.text);
        PlayerPrefs.SetString("password",loginPassword.text);
        PlayerPrefs.SetInt("ExDate",exdateInt);
    }

    public bool Load()
    {
        email = PlayerPrefs.GetString("email");
        password = PlayerPrefs.GetString("password");
        int exdate = PlayerPrefs.GetInt("ExDate");
        Debug.Log($"today {todayInt}, ex {exdate} ");
        if (todayInt > exdate)
        {
            Debug.Log("false");
            return false;
        }
        else
        {
            Debug.Log("true");
            return true;
        }
    }
    public void RegisterButtonPressedForRegister()
    {
        if (registerPassword1.text == registerPassword2.text)
        {
            Messenger<string, string>.Broadcast(GameEvent.REGISTER, registerEmail.text, registerPassword1.text);
            Messenger<string>.Broadcast(GameEvent.SENDING_USERNAME,registerName.text);
        }
        else
        {
            Debug.Log("Passwords are not match.");
        }

    }

    public void KisaGiris()
    {
        Messenger<string,string>.Broadcast(GameEvent.LOG_IN,"mustafa@sepen.com","123sepen");
    }

    public void GoGameButton()
    {
        Messenger<string,string>.Broadcast(GameEvent.LOG_IN,email,password);
        
    }

    public void GamePassRegisterButton()
    {
        welcomePanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.REGISTER_COMPLATED,BackButtonPressed);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.REGISTER_COMPLATED,BackButtonPressed);
    }
}
