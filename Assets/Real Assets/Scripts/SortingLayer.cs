using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
   
    void Start()
    {
        if (tag=="Letter")
        {
            GetComponent<MeshRenderer>().sortingLayerName = "Letter";

        }else if (tag == "Word")
        {
            GetComponent<MeshRenderer>().sortingLayerName = "Word";

        }
       
    }

   
}
