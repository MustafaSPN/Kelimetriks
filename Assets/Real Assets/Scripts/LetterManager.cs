    using System;
using System.Collections;
using System.Collections.Generic;
    using DG.Tweening;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;
using UnityEngine.Pool;

public class LetterManager : MonoBehaviour
{
    [SerializeField] public GameObject[][] objects;
    [SerializeField] public List<GameObject> selectedLetter;
    private Queue<GameObject> queue;
    public int totalLetterCount = 0;
    [Header("Scriptable Objects")]
    [SerializeField] public PastelColors randomColor;
    [SerializeField] public Shape shapes;
    [SerializeField] public RandomLetters randomL;
    [SerializeField] public Grid grid;
    
    [Header("Prefabs")]
    [SerializeField] public GameObject prefab;
    
    
    private void Awake()
    {
        queue = new Queue<GameObject>();
        selectedLetter = new List<GameObject>();
        for (int i = 0; i < 48; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
        
        objects = new GameObject[6][];
        for (int i = 0; i < 6; i++)
        {
            objects[i] = new GameObject[8];
        }
   
    }

  

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.GENERATE_LETTER,GenerateLetter);
        Messenger.AddListener(GameEvent.DESTROY_LETTER,DestroyLetter);
        Messenger<GameObject>.AddListener(GameEvent.MOVE_CLICKED_LETTER_HIDE,MoveClickedLetter);
        Messenger.AddListener(GameEvent.MOVE_CLICKED_LETTER_BACK,MoveBackClickedLetter);
        Messenger<GameObject>.AddListener(GameEvent.SHAKE_LETTERS,ShakeLetters);
        
        
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GENERATE_LETTER,GenerateLetter);
        Messenger.RemoveListener(GameEvent.DESTROY_LETTER,DestroyLetter);
        Messenger<GameObject>.AddListener(GameEvent.MOVE_CLICKED_LETTER_HIDE,MoveClickedLetter);
        Messenger.AddListener(GameEvent.MOVE_CLICKED_LETTER_BACK,MoveBackClickedLetter);
        Messenger<GameObject>.RemoveListener(GameEvent.SHAKE_LETTERS,ShakeLetters);
    }

    private void MoveBackClickedLetter()
    {
        foreach (var obj in selectedLetter)
        {
            int a = obj.GetComponent<Letter>().indexCell[0];
            int b = obj.GetComponent<Letter>().indexCell[1];
            obj.GetComponent<LetterMovement>().Move(new Vector2(0, 1.54f),grid.cellPosition[a][b]);
           // selectedLetter.Remove(obj);
        }

        selectedLetter.Clear();
    }
    private void MoveClickedLetter(GameObject obj)
    {
        Vector2 pos = obj.GetComponent<Transform>().position;
        Vector2 final = new Vector2(0, 1.54f);
        obj.GetComponent<LetterMovement>().Move(pos,final);
        selectedLetter.Add(obj);
    }

    public void DestroyLetter()
    {
        if (selectedLetter.Count!=0)
        {
            foreach (var obj in selectedLetter)
            {
                int[] index = obj.GetComponent<Letter>().GetCellIndex(); 
                obj.SetActive(false);
                grid.cellFullness[index[0]][index[1]] = false;
                objects[index[0]][index[1]] = null;
                queue.Enqueue(obj);
                Reposition(index[0],index[1]);  
            }
            selectedLetter.Clear();
        }
       
            
    }

    public void Reposition(int a,int b)
    {
        for (int i = b+1; i < 8; i++)
        {
            if (objects[a][i] == null)
            {
                break;
            }
            objects[a][i - 1] = objects[a][i];
            objects[a][i] = null;
            objects[a][i-1].GetComponent<LetterMovement>().Move(grid.cellPosition[a][i],grid.cellPosition[a][i-1]);
            objects[a][i-1].GetComponent<Letter>().setPosition(a,i-1);
            grid.cellFullness[a][i] = false;
            grid.cellFullness[a][i-1] = true;
        }
    }
    public void GenerateLetter()
    {

        GameObject obj = queue.Dequeue();
        obj.SetActive(true);
        CreateLetter(obj);

    }
    public void CreateLetter(GameObject obj)
    {
        
        obj.GetComponent<SpriteRenderer>().sprite = shapes.randomShape();
        obj.GetComponent<SpriteRenderer>().color = randomColor.GenerateRandomColor();
        TMP_Text child = obj.transform.GetChild(0).GetComponent<TMP_Text>();
        char letter = randomL.GenerateRandomLetter(); 
        child.text = letter.ToString();
        int[] position = new int[2];
        position = SetLetterPosition(obj);
        objects[position[0]][position[1]] = obj;
        obj.GetComponent<Letter>().SetLetterValues(position[0],position[1],letter);
    }

    public void ShakeLetters(GameObject obje)
    {
            obje.GetComponent<Transform>().DOShakeScale(0.3f);        
      
    }
    public int[] SetLetterPosition(GameObject letter)
    {
        int column;
        if (totalLetterCount<12)
        {
             column = totalLetterCount % 6;
            
        }

        else
        {
             column = Random.Range(0, 6);
        }
            int[] targetCell = new int[2];
            targetCell = grid.TargetCell(column);
            Vector2 target = grid.cellPosition[targetCell[0]][targetCell[1]];
            Vector2 start = grid.cellPosition[targetCell[0]][9];
            letter.GetComponent<LetterMovement>().Move(start,target);
            int[] pos = new int[2];
            pos[0] = targetCell[0];
            pos[1] = targetCell[1];
            totalLetterCount++;
        return pos;
    }

    
}
