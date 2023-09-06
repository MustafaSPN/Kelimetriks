using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] public Grid grid;
    [SerializeField] public PastelColors randomColor;
    
    
    int width = 6;
    int height = 10;

    private void Start()
    {
        grid.InitializeGrid(width,height);
        randomColor.InitializeColors();
        
    }
}
