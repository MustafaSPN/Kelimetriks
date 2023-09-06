using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Messenger.Broadcast(GameEvent.CROSS_TOUCHED);
    }
}
