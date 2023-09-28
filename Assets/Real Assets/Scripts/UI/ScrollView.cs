using System;
using DG.Tweening;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
   public enum ScreenIndex
    {
        left = -1,
        mid = 0,
        right=1
    }
    
    [SerializeField] public RectTransform content;
    [SerializeField] public RectTransform holder1;
    [SerializeField] public RectTransform holder2;
    [SerializeField] public RectTransform holder3;
    [SerializeField] public RectTransform canvas;
    private Vector2 startPos;
    private Vector2 endPos;
    public ScreenIndex currentIndex;
    public bool isMoov = false;
    
    void Start()
    {
        content.sizeDelta = new Vector2(3*canvas.sizeDelta.x, 0);
        content.anchoredPosition = new Vector2(0,0);
        currentIndex = ScreenIndex.mid;
        holder1.sizeDelta = new Vector2(canvas.sizeDelta.x, 0);
        holder2.sizeDelta = new Vector2(canvas.sizeDelta.x, 0);
        holder3.sizeDelta = new Vector2(canvas.sizeDelta.x, 0);
    }

    private void Update()
    {
        if (content.anchoredPosition.x < canvas.sizeDelta.x/2 && content.anchoredPosition.x > -canvas.sizeDelta.x/2)
        {   
            currentIndex = ScreenIndex.mid;
        }else if (content.anchoredPosition.x > canvas.sizeDelta.x / 2)
        {
            currentIndex = ScreenIndex.left;
        }else if (content.anchoredPosition.x < -canvas.sizeDelta.x / 2)
        {
            currentIndex = ScreenIndex.right;
        }

        if (!Input.GetMouseButtonUp(0) || isMoov) return;
        switch (currentIndex)
        {
            case ScreenIndex.mid: content.DOAnchorPosX(0, 0.2f).OnComplete(() =>
                {
                    isMoov = false;
                });
                break;
            case ScreenIndex.left: content.DOAnchorPosX(canvas.sizeDelta.x, 0.2f).OnComplete(() =>
                {
                    isMoov = false;
                });
                break;
            case ScreenIndex.right: content.DOAnchorPosX(-canvas.sizeDelta.x, 0.2f).OnComplete(() =>
                {
                    isMoov = false;
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OpenLeaderboard()
    {
        isMoov = true;
        content.DOAnchorPosX(-canvas.sizeDelta.x, 0.2f).OnComplete(() =>
        {
            isMoov = false;
        });
    }

    public void OpenSettings()
    {
        isMoov = true;
        content.DOAnchorPosX(canvas.sizeDelta.x, 0.2f).OnComplete(() =>
        {
            isMoov = false;
        });
    }
}
