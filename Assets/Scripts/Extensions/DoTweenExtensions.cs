using UnityEngine;

namespace DG.Tweening
{
    public static class DoTweenExtensions
    {
        public static Tween DoWait(this Transform target, float duration, System.Action OnCompleteFunc = null)
        {
            return DOTween.To(() => 0, (curProgress) => { }, 1f, duration)
                     .OnComplete(() => { OnCompleteFunc?.Invoke(); });
        }
    }
}
