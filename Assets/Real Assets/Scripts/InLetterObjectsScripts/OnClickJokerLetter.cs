using UnityEngine;

public class OnClickJokerLetter : MonoBehaviour
{
    public bool isClicked = false;
    
    private void OnMouseDown()
    {
        if (!GetComponent<Letter>().GetIsClickable()) return;
        isClicked = true;
        Messenger.Broadcast(GameEvent.CLICKED_JOKERLETTER);
    }
}
