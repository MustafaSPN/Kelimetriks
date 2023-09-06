using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "GridObject",menuName = "ScriptableObjcets/Grid")]
public class Grid : ScriptableObject
{
    [Serialize] public Vector2[][] cellPosition;
    [Serialize] public bool[][] cellFullness;
    public int width;
    public int height;
    public float startX = -1.62f;
    public float startY = -4.25f;
    public float offset = 0.65f;
    
    public void InitializeGrid(int _width, int _height)
    {
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

    public Vector2 GenerateTargetPosition(int target)
    {
        
        Vector2 targetPos = Vector2.zero;
        for (int i = 0; i < cellPosition[target].Length; i++)
        {
            if (!cellFullness[target][i])
            {targetPos = cellPosition[target][i];
                cellFullness[target][i] = true;
                Debug.Log($"target: {target},i: {i}");
                break;
                
            }
            
                
            
           
            
        }

        return targetPos;
    }
}
