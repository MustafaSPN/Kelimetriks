using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class LetterMovement : MonoBehaviour
{
    private Tween currentTween;

    public void Move(Vector2 startPosition, Vector2 targetPosition,float duration)
    {
        SetClickable(false);
        transform.position = startPosition;
       

        if (currentTween != null && currentTween.IsActive())
        {
            // Eğer bir tween hala çalışıyorsa, ikinci tweeni başlatma
            currentTween.OnKill(() =>
            {
                currentTween = transform.DOMove(targetPosition, duration).OnComplete(() => { SetClickable(true); })
                    .OnKill(() => { transform.position = targetPosition; });
            });
        }
        else
        {
            // Hiçbir tween çalışmıyorsa, doğrudan tween'i başlat
            currentTween = transform.DOMove(targetPosition, duration).OnComplete(() => { SetClickable(true); })
                .OnKill(() => { transform.position = targetPosition; });

        }
    }

    public void SetClickable(bool b)
    {
        GetComponent<Letter>().SetisClickable(b);
    }

}
