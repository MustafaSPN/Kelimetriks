using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickJokerLetter : MonoBehaviour
{
    public bool isClicked = false;
    private void OnMouseDown()
    {
        if (GetComponent<Letter>().GetIsClickable())
        {
            isClicked = true;
            Messenger.Broadcast(GameEvent.CLICKED_JOKERLETTER);
        }

    }
}
