using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickPauseButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Messenger.Broadcast(GameEvent.PAUSE_GAME);
    }
}
