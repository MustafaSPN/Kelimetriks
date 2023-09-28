using UnityEngine;
using DG.Tweening;

public class LetterMovement : MonoBehaviour
{
    private Tween currentTween;

    public void Move(Vector2 startPosition, Vector2 targetPosition,float duration)
    {
        SetClickable(false);
        transform.localPosition = startPosition;
        if (currentTween != null && currentTween.IsActive())
        {
            // Eğer bir tween hala çalışıyorsa, ikinci tweeni başlatma
            currentTween.OnKill(() =>
            {
                currentTween = transform.DOLocalMove(targetPosition, duration).OnComplete(() => { SetClickable(true); })
                    .OnKill(() => { transform.localPosition = targetPosition; });
            });
        }
        else
        {
            // Hiçbir tween çalışmıyorsa, doğrudan tween'i başlat
            currentTween = transform.DOLocalMove(targetPosition, duration).OnComplete(() => { SetClickable(true); })
                .OnKill(() => { transform.localPosition = targetPosition; });

        }
    }

    public void SetClickable(bool b)
    {
        GetComponent<Letter>().SetisClickable(b);
    }
}
