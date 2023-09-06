using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    [SerializeField] private TMP_Text letterTMP;
   

    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        Messenger<String>.Broadcast(GameEvent.TOUCHED_LETTER,letterTMP.text);
    }
}
