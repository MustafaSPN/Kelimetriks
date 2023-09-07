using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Tilemaps;
using Random = UnityEngine.Random;

public class LetterMovement : MonoBehaviour
{
    public void Move(Vector2 startPosition,Vector2 targetPosition)
    {
        transform.position = startPosition;
        float duration = Mathf.Abs(startPosition.y - targetPosition.y) / 6;
        if (targetPosition.x == 0 || startPosition.x == 0)
        {
            duration = 0.2f;
        }
        transform.DOMove(targetPosition, duration);

    }

}
