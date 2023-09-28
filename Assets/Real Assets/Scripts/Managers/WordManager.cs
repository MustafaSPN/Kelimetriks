using TMPro;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] public TMP_Text wordText;

    private void Start()
    {
          wordText.text = "";
    }

    public void ReturnWord()
    {
        Messenger<string>.Broadcast(GameEvent.RETURN_WORD,wordText.text);
    }
    
    public void CancelWord()
    {
        wordText.text = "";
    }

    public void AddLetterToWorld(char ch)
    {
        wordText.text += ch.ToString();
    }
    
    private void OnEnable()
    {
        Messenger<char>.AddListener(GameEvent.ADD_LETTER_TO_WORD,AddLetterToWorld);
        Messenger.AddListener(GameEvent.EMPTY_WORD,CancelWord);
        Messenger.AddListener(GameEvent.REQUEST_WORD,ReturnWord);
        Messenger.AddListener(GameEvent.GAME_OVER,CancelWord);
        Messenger.AddListener(GameEvent.REWARDED_ADS,CancelWord);
    }

    private void OnDisable()
    {
        Messenger<char>.RemoveListener(GameEvent.ADD_LETTER_TO_WORD,AddLetterToWorld);
        Messenger.RemoveListener(GameEvent.EMPTY_WORD,CancelWord);
        Messenger.RemoveListener(GameEvent.REQUEST_WORD,ReturnWord);
        Messenger.RemoveListener(GameEvent.GAME_OVER,CancelWord);
        Messenger.RemoveListener(GameEvent.REWARDED_ADS,CancelWord);
    }
}
