using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Messenger.Broadcast(GameEvent.CHECK_BUTTON_PRESSED);
    }
}
