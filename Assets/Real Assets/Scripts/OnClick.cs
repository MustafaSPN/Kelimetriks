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
    private void OnMouseDown()
    {
        char letter = GetComponent<Letter>().letter;
        Messenger<char,GameObject>.Broadcast(GameEvent.CLICKED_LETTER,letter,this.gameObject);
    }
}
