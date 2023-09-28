using System;
using System.Collections;
using TMPro;
using UnityEngine;

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
    private DateTime today;
    private string email;
    private string password;
    private int todayInt;
    private TMP_InputField[] inputs;
    
    private void Start()
    { 
            welcomePanel.SetActive(true);
            loginPanel.SetActive(false);
            registerPanel.SetActive(false);
            inputs = new[]
        {
            loginEmail, loginPassword, registerEmail, registerName, registerPassword1, registerPassword2
        };
    }

    private void Disappear()
    {
        welcomePanel.SetActive(false);
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
        foreach (var input in inputs)
        {
            input.text = string.Empty;
        }
    }

    public void LoginButtonPressedForLogin()
    {
        Messenger<string, string>.Broadcast(GameEvent.LOG_IN, loginEmail.text, loginPassword.text);
    }

    public void RegisterButtonPressedForRegister()
    {
        if (registerPassword1.text == registerPassword2.text)
        {
            Messenger<string, string,string>.Broadcast(GameEvent.REGISTER, registerEmail.text, registerPassword1.text,registerName.text);
        }
        else
        {
            Debug.Log("Passwords are not match.");
        }
    }

    public IEnumerator AutoLogIn()
    {
        yield return new WaitForSeconds(1f);
        Messenger<string,string>.Broadcast(GameEvent.LOG_IN,email,password);
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
        Messenger.AddListener(GameEvent.SHOW_WELCOME_SCENE,Start);
        Messenger.AddListener(GameEvent.DONT_SHOW_WELCOME_SCENE,Disappear);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.REGISTER_COMPLATED,BackButtonPressed);
        Messenger.RemoveListener(GameEvent.SHOW_WELCOME_SCENE,Start);
        Messenger.RemoveListener(GameEvent.DONT_SHOW_WELCOME_SCENE,Disappear);
    }
}
