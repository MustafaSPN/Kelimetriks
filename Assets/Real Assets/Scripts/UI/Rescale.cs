using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescale : MonoBehaviour
{
    
    
    void Start()
    {
        float width = Screen.width;
        float height = Screen.height;
        float ratio = width / height;
        if (ratio < 0.5)
        {
            
            GetComponent<Transform>().localScale = new Vector3(1-(0.5f-ratio)*2,1-(0.5f-ratio)*2,1-(0.5f-ratio)*2);
        }
        else
        {
            GetComponent<Transform>().localScale = Vector3.one;
        }

    }
}
