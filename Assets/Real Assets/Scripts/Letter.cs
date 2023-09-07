using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public int[] indexCell = new int[2];
    public char letter;


    public void SetLetterValues(int x, int y, char lettr)
    {
        indexCell[0] = x;
        indexCell[1] = y;
        letter = lettr;
    }

    public int[] GetCellIndex()
    {
        return indexCell;
    }

    public void ResetLetter()
    {
        indexCell = null;
        letter = '1';
    }

    public void setPosition(int a, int b)
    {
        indexCell[0] = a;
        indexCell[1] = b;
    }
}
