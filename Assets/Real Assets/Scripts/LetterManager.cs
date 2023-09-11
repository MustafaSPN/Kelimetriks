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
    [SerializeField] public List<GameObject> selectedLetter;
    public GameObject[][]GridObjects;
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
        GridObjects = new GameObject[6][];
        queue = new Queue<GameObject>();
        selectedLetter = new List<GameObject>();
        for (int i = 0; i < 48; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }

        for (int i = 0; i < 6; i++)
        {
            GridObjects[i] = new GameObject[8];

            for (int j = 0; j < 8; j++)
            {
                GridObjects[i][j] = null;
            }
        }

    }

  

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.GENERATE_LETTER,GenerateLetter);
        Messenger.AddListener(GameEvent.DESTROY_CORRECT_LETTER,DestroyCorrectLetter);
        Messenger<GameObject>.AddListener(GameEvent.MOVE_CLICKED_LETTER_HIDE,HighlightLetter);
        Messenger.AddListener(GameEvent.MOVE_CLICKED_LETTER_BACK,HighlightBackLetter);
        Messenger<GameObject>.AddListener(GameEvent.SHAKE_LETTERS,ShakeLetters);
        
        
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.GENERATE_LETTER,GenerateLetter);
        Messenger.RemoveListener(GameEvent.DESTROY_CORRECT_LETTER,DestroyCorrectLetter);
        Messenger<GameObject>.AddListener(GameEvent.MOVE_CLICKED_LETTER_HIDE,HighlightLetter);
        Messenger.AddListener(GameEvent.MOVE_CLICKED_LETTER_BACK,HighlightBackLetter);
        Messenger<GameObject>.RemoveListener(GameEvent.SHAKE_LETTERS,ShakeLetters);
    }


    private void DestroyCorrectLetter()
    {
       
            foreach (var obj in selectedLetter)
            {
                Vector2 startPos = obj.GetComponent<Transform>().position;
                Vector2 targetPos = new Vector2(0, 3f);
                obj.GetComponent<LetterMovement>().Move(startPos, targetPos, 0.3f);
            }


            
        
            DestroyLetter();            
        
    }

  
    private void HighlightBackLetter()
    {
        foreach (var obj in selectedLetter)
        {
            Color color = obj.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            obj.GetComponent<SpriteRenderer>().color = color;
            obj.GetComponent<Letter>().SetisClickable(true);
            ShakeLetters(obj);
        }

        selectedLetter.Clear();
    }
    private void HighlightLetter(GameObject obj)
    {
        selectedLetter.Add(obj);
        Color color = obj.GetComponent<SpriteRenderer>().color;
        color.a = 0.4f;
        obj.GetComponent<SpriteRenderer>().color = color;
        obj.GetComponent<Letter>().SetisClickable(false);
    }

    public void DestroyLetter()
    {
       
            int score = 0;
            foreach (var obj in selectedLetter)
            {
                int[] index = obj.GetComponent<Letter>().GetCellIndex();
                score += obj.GetComponent<Letter>().score;
                obj.SetActive(false);
                grid.cellFullness[index[0]][index[1]] = false;
                GridObjects[index[0]][index[1]] = null;
                queue.Enqueue(obj);
               Reposition(index[0],index[1]);  
            }
            Messenger<int>.Broadcast(GameEvent.ADD_SCORE,score);

            selectedLetter.Clear();
            
       
            
    }
    
    public void Reposition(int a,int b)
    {
        for (int i = 1; i < 8; i++)
        {
            if (grid.cellFullness[a][i] && !grid.cellFullness[a][i-1])
            {
                GridObjects[a][i - 1] = GridObjects[a][i];
                GridObjects[a][i] = null;
                grid.cellFullness[a][i - 1] = true;
                grid.cellFullness[a][i] = false;
                Vector2 startPos = GridObjects[a][i - 1].GetComponent<Transform>().position;
                Vector2 targetPos = grid.cellPosition[a][i - 1];
                GridObjects[a][i-1].GetComponent<LetterMovement>().Move(startPos,targetPos,CalculateMoveDuration(startPos,targetPos) );
                GridObjects[a][i-1].GetComponent<Letter>().setPosition(a,i-1);
            }
            
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
        int score = randomL.GetScore(letter);
        obj.GetComponent<Letter>().SetLetterValues(position[0],position[1],score,letter);
        
            GridObjects[position[0]][position[1]] = obj;
        
       
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
            letter.GetComponent<LetterMovement>().Move(start,target,CalculateMoveDuration(start,target));
            int[] pos = new int[2];
            pos[0] = targetCell[0];
            pos[1] = targetCell[1];
            totalLetterCount++;
        return pos;
    }

    private float CalculateMoveDuration(Vector2 startPos, Vector2 targetPos)
    {
        return Mathf.Abs(startPos.y - targetPos.y) / 6;

    }
    
}
