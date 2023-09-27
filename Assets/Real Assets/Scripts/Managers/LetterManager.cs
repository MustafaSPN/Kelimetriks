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
        [SerializeField] public List<GameObject> jokerLetters;
        [SerializeField] public GameObject parent;
        public GameObject[][] GridObjects;
        private Queue<GameObject> queue;
        public int totalLetterCount = 0;
        public int crossLetter = 0;

        [Header("Scriptable Objects")] [SerializeField]
        public PastelColors randomColor;

        [SerializeField] public Shape shapes;
        [SerializeField] public RandomLetters randomL;
        [SerializeField] public Grid grid;

        [Header("Prefabs")] [SerializeField] public GameObject prefab;

        [SerializeField] public GameObject jokerPrefab;



        private void Awake()
        {
            GridObjects = new GameObject[6][];
            queue = new Queue<GameObject>();
            selectedLetter = new List<GameObject>();
            jokerLetters = new List<GameObject>();
            for (int i = 0; i < 49; i++)
            {
                GameObject obj = Instantiate(prefab,parent.transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            for (int i = 0; i < 6; i++)
            {
                GridObjects[i] = new GameObject[9];

                for (int j = 0; j < 9; j++)
                {
                    GridObjects[i][j] = null;
                }
            }

        }

        private void OnEnable()
        {
            Messenger<GameObject>.AddListener(GameEvent.DESTROY_CROSS_LETTERS, StartWaitForDestroyCrossLetter);
            Messenger.AddListener(GameEvent.CROSS_LETTER_GENERATE, GenerateCrossLetters);
            Messenger.AddListener(GameEvent.GENERATE_LETTER, GenerateLetter);
            Messenger.AddListener(GameEvent.DESTROY_CORRECT_LETTER, DestroyCorrectLetter);
            Messenger<GameObject>.AddListener(GameEvent.MOVE_CLICKED_LETTER_HIDE, HighlightLetter);
            Messenger.AddListener(GameEvent.MOVE_CLICKED_LETTER_BACK, HighlightBackLetter);
            Messenger<GameObject>.AddListener(GameEvent.SHAKE_LETTERS, ShakeLetters);
            Messenger.AddListener(GameEvent.START_GAME, ResetGame);
            Messenger.AddListener(GameEvent.JOKER_LETTER_GENERATE, GenerateJokerLetter);
            Messenger.AddListener(GameEvent.CLICKED_JOKERLETTER, ClickedJoker);
            Messenger.AddListener(GameEvent.REWARDED_ADS,RewardAds);


        }

        private void OnDisable()
        {
            Messenger<GameObject>.RemoveListener(GameEvent.DESTROY_CROSS_LETTERS, StartWaitForDestroyCrossLetter);
            Messenger.RemoveListener(GameEvent.CROSS_LETTER_GENERATE, GenerateCrossLetters);
            Messenger.RemoveListener(GameEvent.GENERATE_LETTER, GenerateLetter);
            Messenger.RemoveListener(GameEvent.DESTROY_CORRECT_LETTER, DestroyCorrectLetter);
            Messenger<GameObject>.RemoveListener(GameEvent.MOVE_CLICKED_LETTER_HIDE, HighlightLetter);
            Messenger.RemoveListener(GameEvent.MOVE_CLICKED_LETTER_BACK, HighlightBackLetter);
            Messenger<GameObject>.RemoveListener(GameEvent.SHAKE_LETTERS, ShakeLetters);
            Messenger.RemoveListener(GameEvent.START_GAME, ResetGame);
            Messenger.RemoveListener(GameEvent.JOKER_LETTER_GENERATE, GenerateJokerLetter);
            Messenger.RemoveListener(GameEvent.CLICKED_JOKERLETTER, ClickedJoker);
            Messenger.RemoveListener(GameEvent.REWARDED_ADS,RewardAds);
        }

        private void DestroyCorrectLetter()
        {

            foreach (var obj in selectedLetter)
            {
                Vector2 startPos = obj.GetComponent<Transform>().position;
                Vector2 targetPos = new Vector2(0, 3f);
                int score1 = obj.GetComponent<Letter>().score;
                if (score1 >=150){
                    Messenger<Transform>.Broadcast(GameEvent.COIN,obj.transform);
                }
                obj.GetComponent<LetterMovement>().Move(startPos, targetPos, 0.3f);
            }


            StartCoroutine(WaitForDestroySelectedLetter());


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

        public void StartWaitForDestroyCrossLetter(GameObject obj)
        {
            StartCoroutine(WaitForDestroyCrossLetter(obj));
        }

        private IEnumerator WaitForDestroySelectedLetter()
        {
            yield return new WaitForSeconds(0.2f);
            DestroySelectedLetters();

        }

        private IEnumerator WaitForDestroyCrossLetter(GameObject obj)
        {
            yield return new WaitForSeconds(0.2f);
            DestroyCrossLetters(obj);

        }

        private void HighlightLetter(GameObject obj)
        {
            // Messenger<Transform>.Broadcast(GameEvent.COIN,obj.transform);
            selectedLetter.Add(obj);
            Color color = obj.GetComponent<SpriteRenderer>().color;
            color.a = 0.4f;
            obj.GetComponent<SpriteRenderer>().color = color;
            obj.GetComponent<Letter>().SetisClickable(false);
        }

        public void DestroyCrossLetters(GameObject obj)
        {

            int[] index = obj.GetComponent<Letter>().GetCellIndex();
            int row = index[0];
            int i = index[1];
            grid.cellFullness[index[0]][index[1]] = false;
            GridObjects[index[0]][index[1]] = null;
            obj.GetComponent<Letter>().ResetLetter();
            obj.GetComponent<Letter>().isClickable = false;
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            obj.SetActive(false);
            queue.Enqueue(obj);
            Reposition(row);



        }

        public void DestroySelectedLetters()
        {

            int score = 0;
            foreach (GameObject obj in selectedLetter)
            {
                int[] index = obj.GetComponent<Letter>().GetCellIndex();
                int row = index[0];
                int score1 = obj.GetComponent<Letter>().score; 
                score += score1;
                grid.cellFullness[index[0]][index[1]] = false;
                GridObjects[index[0]][index[1]] = null;
                obj.GetComponent<Letter>().ResetLetter();
                obj.GetComponent<Letter>().isClickable = false;
                Reposition(row);
                obj.SetActive(false);
                queue.Enqueue(obj);

            }

            Messenger<int>.Broadcast(GameEvent.ADD_SCORE, score);

            selectedLetter.Clear();




        }

        public void Reposition(int a)
        {
            for (int i = 1; i < 8; i++)
            {

                if (grid.cellFullness[a][i] && !grid.cellFullness[a][i - 1])
                {
                    GridObjects[a][i - 1] = GridObjects[a][i];
                    GridObjects[a][i] = null;
                    grid.cellFullness[a][i - 1] = true;
                    grid.cellFullness[a][i] = false;
                    Vector2 startPos = GridObjects[a][i - 1].GetComponent<Transform>().localPosition;
                    Vector2 targetPos = grid.cellPosition[a][i - 1];
                    GridObjects[a][i - 1].GetComponent<LetterMovement>().Move(startPos, targetPos,
                        CalculateMoveDuration(startPos, targetPos));
                    GridObjects[a][i - 1].GetComponent<Letter>().setPosition(a, i - 1);
                }

            }



        }

        public void GenerateLetter()
        {
            
            
            GameObject obj = queue.Dequeue();
            obj.SetActive(true);
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(1).gameObject.SetActive(false);
            CreateLetter(obj);
            Messenger.Broadcast(GameEvent.PLAY_GENERATE_LETTERS); 
        }

        public void CreateLetter(GameObject obj)
        {
            obj.GetComponent<SpriteRenderer>().sprite = shapes.randomShape();
            obj.GetComponent<SpriteRenderer>().color = randomColor.GenerateRandomColor();
            TMP_Text child = obj.transform.GetChild(0).GetComponent<TMP_Text>();
            char letter = randomL.GenerateLetter();
            child.text = letter.ToString();
            int[] position = new int[2];
            position = SetLetterPosition(obj);
            int score = randomL.GetScore(letter);
            obj.GetComponent<Letter>().SetLetterValues(position[0], position[1], score, letter);
            obj.GetComponent<Letter>().isClickable = false;
            GridObjects[position[0]][position[1]] = obj;
            if (position[1] == 8)
            {
                GameOver();

            }

        }

        public void ShakeLetters(GameObject obje)
        {
            obje.GetComponent<Transform>().DOShakeScale(0.3f);
            Messenger.Broadcast(GameEvent.PLAY_HARFLER_BINGILDARKEN);

        }

        public int[] SetLetterPosition(GameObject letter)
        {
            int column;
            if (totalLetterCount < 12)
            {
                column = totalLetterCount % 6;

            }
            else if (crossLetter <= 6 && crossLetter > 0)
            {
                column = crossLetter % 6;
                crossLetter--;
            }
            else
            {
                column = Random.Range(0, 6);
            }

            int[] targetCell = new int[2];
            targetCell = grid.TargetCell(column);
            Vector2 target = grid.cellPosition[targetCell[0]][targetCell[1]];
            Vector2 start = grid.cellPosition[targetCell[0]][9];
            letter.GetComponent<LetterMovement>().Move(start, target, CalculateMoveDuration(start, target));
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

        public void GameOver()
        {
            Messenger.Broadcast(GameEvent.WAIT_FOR_ADS);
        }

        private void ResetGame()
        {
            DestroyAllWords();
            selectedLetter.Clear();
            foreach (var obj in jokerLetters)
            {
                Destroy(obj);
            }
            jokerLetters.Clear();
            grid.setAllEmpty();
            randomL.Reset();
            crossLetter = 0;
            totalLetterCount = 0;

        }

        private void GenerateCrossLetters()
        {
            Messenger.Broadcast(GameEvent.PLAY_CROSS_LETTERS);
            crossLetter = 6;
            for (int i = 0; i < 6; i++)
            {
                GameObject obj = queue.Dequeue();
                obj.SetActive(true);
                obj.GetComponent<SpriteRenderer>().sprite = shapes.randomShape();
                obj.GetComponent<SpriteRenderer>().color = randomColor.GenerateRandomColor();
                obj.transform.GetChild(0).gameObject.SetActive(false);
                obj.transform.GetChild(1).gameObject.SetActive(true);
                int[] position = new int[2];
                position = SetLetterPosition(obj);
                obj.GetComponent<Letter>().SetisCrossLetter();
                obj.GetComponent<Letter>().setPosition(position[0], position[1]);
                if (position[1]==0)
                {
                    StartCoroutine(WaitForFirstDestroyCrossLetter(obj));
                }
                GridObjects[position[0]][position[1]] = obj;
                if (position[1] == 8)
                {
                    GameOver();

                }
            }

        }
        private IEnumerator WaitForFirstDestroyCrossLetter(GameObject obj)
        {
            yield return new WaitForSeconds(2f);
            DestroyCrossLetters(obj);

        }
        
        private void GenerateJokerLetter()
        {
            Messenger.Broadcast(GameEvent.PLAY_JOKER_DUSERKEN);
            GameObject obj = Instantiate(jokerPrefab,parent.transform);
            int[] position = new int[2];
            position = SetLetterPosition(obj);
            obj.GetComponent<Letter>().setPosition(position[0], position[1]);
            obj.GetComponent<Letter>().SetisJokerLetter();
            GridObjects[position[0]][position[1]] = obj;
            jokerLetters.Add(obj);
            if (position[1] == 8)
            {
                GameOver();

            }

        }

        private IEnumerator JokerEffect(GameObject obj)
        {
            Messenger.Broadcast(GameEvent.PLAY_JOKER_CALISIRKEN);
            obj.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            
              int[] pos = obj.GetComponent<Letter>().GetCellIndex();
                    for (int i = 0; i < 6; i++)
                    {
                        if (GridObjects[i][pos[1]]!=null)
                        {
                            if (i==pos[0])
                            {
                                continue;
                            }

                                GameObject obje = GridObjects[i][pos[1]];
                                int[] index = obje.GetComponent<Letter>().GetCellIndex();
                                int row = index[0];
                                grid.cellFullness[index[0]][index[1]] = false;
                                GridObjects[index[0]][index[1]] = null;
                                obje.GetComponent<Letter>().ResetLetter();
                                obje.GetComponent<Letter>().isClickable = false;
                                Reposition(row);
                                obje.SetActive(false);
                                queue.Enqueue(obje);
                            
                        }
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        if (GridObjects[pos[0]][i]!=null)
                        {
                            if (i==pos[1])
                            {
                                continue;
                            }

                            GameObject obje = GridObjects[pos[0]][i];
                            int[] index = obje.GetComponent<Letter>().GetCellIndex();
                            grid.cellFullness[index[0]][index[1]] = false;
                            GridObjects[index[0]][index[1]] = null;
                            obje.GetComponent<Letter>().ResetLetter();
                            obje.GetComponent<Letter>().isClickable = false;
                            obje.SetActive(false);
                            queue.Enqueue(obje);
                            
                        }
                    }
            
            
            
            
            obj.transform.GetChild(1).gameObject.SetActive(false);
            int[] indx = obj.GetComponent<Letter>().GetCellIndex();
            int row1 = indx[0];
            grid.cellFullness[indx[0]][indx[1]] = false;
            GridObjects[indx[0]][indx[1]] = null;
            obj.SetActive(false);
            Reposition(row1);
            Destroy(obj);
        }
        private void ClickedJoker()
        {
            foreach (var obj in jokerLetters)
            {
                if (obj.GetComponent<OnClickJokerLetter>().isClicked)
                {
                    StartCoroutine(JokerEffect(obj));
                  
                  
                }
                else
                {
                    continue;
                }

                jokerLetters.Remove(obj);
            }
        }

        private void RewardAds()
        {
        ResetGame();

        }
        
        private void DestroyAllWords()
    {
        foreach (var objects in GridObjects)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    if (obj.GetComponent<Letter>().GetisJokerLetter())
                    {
                      Destroy(obj);    
                    
                    }
                    
                    else{
                        int[] pos = obj.GetComponent<Letter>().GetCellIndex();
                    obj.transform.GetChild(1).gameObject.SetActive(false);
                    obj.SetActive(false);
                    obj.GetComponent<Letter>().ResetLetter();
                    queue.Enqueue(obj);
                    objects[pos[1]] = null;
                    }
                }
            }
            
        }
    }
}
