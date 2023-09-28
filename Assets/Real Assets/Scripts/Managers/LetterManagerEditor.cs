#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LetterManager))]
public class LetterManagerEditor : Editor
{
    [SerializeField] private Grid grid;
    private int number1;
    private int number2;
    private int result;

    //Create editor button and features
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Letter"))
        {
           Messenger.Broadcast(GameEvent.GENERATE_LETTER);
        }
    }
}
#endif