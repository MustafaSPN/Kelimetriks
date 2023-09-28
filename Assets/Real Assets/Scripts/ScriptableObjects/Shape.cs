using UnityEngine;
[CreateAssetMenu(fileName = "ShapeObject",menuName = "ScriptableObjects/Shapes")]

public class Shape : ScriptableObject
{
    [SerializeField] public Sprite[] shape;

    public Sprite randomShape()
    {
        return shape[Random.Range(0, 3)];
    }
}
