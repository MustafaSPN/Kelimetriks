using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    private void Start()
    {
        GetComponent<RectTransform>().DOAnchorPos(target.anchoredPosition, 3f);
    }
}
