using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OnClick : MonoBehaviour
{
    [SerializeField] private TMP_Text letterTMP;
    private Vector2 firstPos = Vector2.zero;

    

    private void OnMouseDown()
    {
        firstPos = gameObject.transform.position;
        gameObject.transform.DOMove(new Vector2(0, 1.5f), 0.5f);
        Messenger<String>.Broadcast(GameEvent.TOUCHED_LETTER,letterTMP.text);
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.CANCEL_WORD,ComeBackFirstPos);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.CANCEL_WORD,ComeBackFirstPos);
    }

    private void ComeBackFirstPos()
    {
        if (firstPos!=Vector2.zero)
        {
            gameObject.transform.DOMove(firstPos, 0.5f);    
        }
        
    }
}
