using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LetterManager))]
public class LetterManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Letter"))
        {
           Messenger.Broadcast(GameEvent.GENERATE_LETTER);
        }
    }
}
