using UnityEngine;

public class OnClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!GetComponent<Letter>().GetIsClickable()) return;
        char letter = GetComponent<Letter>().letter;
        Messenger<char,GameObject>.Broadcast(GameEvent.CLICKED_LETTER,letter,this.gameObject);
    }
}
