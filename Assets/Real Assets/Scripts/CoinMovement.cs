using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] public RectTransform target;
    
    
    private void Start()
    {
        GetComponent<RectTransform>().DOAnchorPos(target.anchoredPosition, 3f);
    }

    public void SetTarget(RectTransform targt)
    {
        target = targt;
    }
}
