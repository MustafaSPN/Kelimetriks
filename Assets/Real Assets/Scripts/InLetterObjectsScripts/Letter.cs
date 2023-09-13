using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public int[] indexCell = new int[2];
    public char letter;
    public bool isClickable = false;
    public int score;
    public bool isCrossLetter = false;


    public void SetLetterValues(int x, int y,int scre ,char lettr)
    {
        indexCell[0] = x;
        indexCell[1] = y;
        letter = lettr;
        score = scre;
    }

    public int[] GetCellIndex()
    {
        return indexCell;
    }

    public void ResetLetter()
    {
        indexCell = new []{-1,-1};
        letter = 'n';
        score = 0;
        isCrossLetter = false;
        isClickable = false;
    }

    public void setPosition(int a, int b)
    {
        indexCell[0] = a;
        indexCell[1] = b;
        if (isCrossLetter && indexCell[1]==0)
        {
            Messenger<GameObject>.Broadcast(GameEvent.DESTROY_CROSS_LETTERS,this.gameObject);
        }
    }

    public void SetisClickable(bool b)
    {
        if (!isCrossLetter)
        {
            isClickable = b;    
        }
        
    }

    public bool GetIsClickable()
    {
        return isClickable;
    }

    public void SetisCrossLetter()
    {
        isCrossLetter = true;
    }
}
