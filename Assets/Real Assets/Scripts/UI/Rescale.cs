using UnityEngine;

public class Rescale : MonoBehaviour
{
    void Start()
    {
        float width = Screen.width;
        float height = Screen.height;
        float ratio = width / height;
        GetComponent<Transform>().localScale = ratio < 0.5 ? new Vector3(1-(0.5f-ratio)*2,1-(0.5f-ratio)*2,1-(0.5f-ratio)*2) : Vector3.one;
    }
}
