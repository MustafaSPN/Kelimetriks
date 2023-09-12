using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LetterManager))]
public class LetterManagerEditor : Editor
{
    [SerializeField] private Grid grid;
    private int number1;
    private int number2;
    private int result;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Letter"))
        {
           Messenger.Broadcast(GameEvent.GENERATE_LETTER);
        }
        //
        // number1 = EditorGUILayout.IntField("X:", number1);
        //
        // number2 = EditorGUILayout.IntField("Y:", number2);
        //
        // bool isFull = LetterManager.cell
        // // Toplam sonucunu metin olarak göster
        // EditorGUILayout.LabelField("isFull:", isFull.ToString());
        //
        // if (GUILayout.Button("Calculate"))
        // {
        //     Debug.Log("isFull: " + isFull);
        // }
    }
    
}
