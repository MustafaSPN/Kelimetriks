using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class WordCountData : MonoBehaviour
{
    [SerializeField] private Words wordObject;
    private DatabaseReference reference;
    private Dictionary<String, int> wordData = new Dictionary<string, int>();
    DataSnapshot snapshot;
    
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.GetReference("WordCountData");
    }

    public void AddWordToWordDataDictionary(string word)
    {
        if (wordData.ContainsKey(word))
        {
            wordData[word]++;
        }
        else
        {
            wordData[word] = 1;
        }
        SaveWordCountDataToDatabase(word);
    }
    
    public void SaveWordCountDataToDatabase(string word)
    {
            int count = wordData[word];
            if (snapshot.Child(word).Value !=null)
            {
                count += int.Parse(snapshot.Child(word).Value.ToString());
            }
            reference.Child(word).SetValueAsync(count);
    }

    public void PullWordCountDataFromDatabase()
    {
        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted) return;
            snapshot = task.Result;
            foreach (var obj in snapshot.Children)
            {
                int count = int.Parse(obj.Value.ToString());
                switch (count)
                {
                    case < 10:
                        wordObject.AddWordCountDataSegment1(obj.Key);
                        break;
                    case < 20:
                        wordObject.AddWordCountDataSegment2(obj.Key);
                        break;
                    case < 50:
                        wordObject.AddWordCountDataSegment3(obj.Key);
                        break;
                    default:
                        wordObject.AddWordCountDataSegment4(obj.Key);
                        break;
                }
            }
        });
    }
    
    private void OnEnable()
    {
        Messenger<string>.AddListener(GameEvent.CORRECT_WORD,AddWordToWordDataDictionary);
        Messenger.AddListener(GameEvent.START_GAME,PullWordCountDataFromDatabase);
    }

    private void OnDisable()
    {
        Messenger<string>.RemoveListener(GameEvent.CORRECT_WORD,AddWordToWordDataDictionary);
        Messenger.RemoveListener(GameEvent.START_GAME,PullWordCountDataFromDatabase);
    }
}
