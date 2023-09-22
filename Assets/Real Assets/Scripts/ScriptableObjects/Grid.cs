using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "GridObject",menuName = "ScriptableObjects/Grid")]
public class Grid : ScriptableObject
{
    [Serialize] public Vector2[][] cellPosition;
    [SerializeField] public bool[][] cellFullness;
    
    public int width;
    public int height;

  
    public float startX;
    public float startY;
    public float offset = 0.65f;
    
    public void InitializeGrid(int _width, int _height)
    {
        startX = -1.62f;
        startY = -2.5f;
        width = _width;
        height = _height;
        cellPosition = new Vector2[width][];
        cellFullness = new bool[width][];
        for (int i = 0; i < width; i++)
        {
            cellPosition[i] = new Vector2[height];
            cellFullness[i] = new bool[height];
        }
        setAllPositions();
        setAllEmpty();
    }

    public void setAllPositions()
    {
        for (int i = 0; i < cellPosition.Length; i++)
        {
            for (int j = 0; j < cellPosition[i].Length; j++)
            {
                float posX = startX + (offset * i);
                float posY = startY + (offset * j);
                cellPosition[i][j] = new Vector2(posX, posY);
            }
        }
    }

    public void setAllEmpty()
    {
        for (int i  = 0; i  < cellFullness.Length; i ++)
        {
            for (int j = 0; j < cellFullness[i].Length; j++)
            {
                cellFullness[i][j] = false;
            }
        }   
    }


    public int[] TargetCell(int column)
    {
        int[] targetcell = new int[2];
        for (int i = 0; i < cellPosition[column].Length; i++)
        {
            if (!cellFullness[column][i])
            {
                cellFullness[column][i] = true;
                targetcell[0] = column;
                targetcell[1] = i;
                return targetcell;

            }
        }

        return targetcell;
    }
}
