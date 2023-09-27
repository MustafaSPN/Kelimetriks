using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Unity.Collections;
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
            if (task.IsCompleted)
            {
                snapshot = task.Result;
                foreach (var obj in snapshot.Children)
                {
                    int count = int.Parse(obj.Value.ToString());
                    if (count < 10)
                    {
                        wordObject.AddWordCountDataSegment1(obj.Key);
                        
                    }else if (count < 20)
                    {
                        wordObject.AddWordCountDataSegment2(obj.Key);
                    }else if (count<50)
                    {
                        wordObject.AddWordCountDataSegment3(obj.Key);
                    }
                    else
                    {
                        wordObject.AddWordCountDataSegment4(obj.Key);
                    }
                    
                    
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
