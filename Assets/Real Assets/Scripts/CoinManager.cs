using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    public Queue<GameObject> queue = new Queue<GameObject>();
    [SerializeField] private TMP_Text coinText;
    public int coinCount;
    private void Start()
    {
        coinText.text = "0";
        for (int i = 0; i < 7; i++)
        {
            GameObject obj = Instantiate(prefab, parent.transform);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }

    }
    

    public void CreateAndMove(Transform startPos)
    {
        GameObject obj = queue.Dequeue();
        obj.GetComponent<Transform>().position = startPos.position;
        obj.SetActive(true);
        obj.GetComponent<RectTransform>().DOAnchorPos(parent.GetComponent<RectTransform>().anchoredPosition, 1f).OnComplete((
            () =>
            {
                obj.SetActive(false);
                queue.Enqueue(obj);
                coinCount++;
                coinText.text = coinCount.ToString();
            }));

       
    }
    
    public void CoinButton(){
        if (coinCount>=5)
        {
            Messenger.Broadcast(GameEvent.JOKER_LETTER_GENERATE);
            coinCount -= 5;
            coinText.text = coinCount.ToString();
        }
        else
        {
            coinText.transform.DOScale(1.5f, 0.2f);
            coinText.transform.DOScale(1f, 0.3f);
        }
    }

    private void ResetCoins()
    {
        coinCount = 0;
        coinText.text = "0";
    }

    private void OnEnable()
    {
        Messenger<Transform>.AddListener(GameEvent.COIN,CreateAndMove);
        Messenger.AddListener(GameEvent.START_GAME,ResetCoins);
    }

    private void OnDisable()
    {
        Messenger<Transform>.RemoveListener(GameEvent.COIN,CreateAndMove);
        Messenger.RemoveListener(GameEvent.START_GAME, ResetCoins);
    }
}
