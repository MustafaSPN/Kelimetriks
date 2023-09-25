using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanelUI : MonoBehaviour
{
   public void CrossButton()
   {
      Messenger.Broadcast(GameEvent.CROSS_BUTTON_PRESSED);
   }

   public void CheckButton()
   {
      Messenger.Broadcast(GameEvent.CHECK_BUTTON_PRESSED);

   }

   public void PauseButton()
   {
      Messenger.Broadcast(GameEvent.PAUSE_GAME);

   }

   
}
