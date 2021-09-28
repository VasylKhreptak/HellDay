using DG.Tweening;
using UnityEngine;

public class UI_MovementAnimation : MonoBehaviour
{
    [System.Serializable]
    protected enum AnimationType
    {
        appear,
        disappear
    }

    [Header("References")] 
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Transform _startTransform;
    protected Vector3 _targetAnchoredPosition;

    [Header("Preferences")]
    [SerializeField] protected float _duration = 1f;
    [SerializeField] protected AnimationCurve _animationCurve;

    protected virtual void StartAnimation(AnimationType animationType, float duration)
    {
        _rectTransform.DOAnchorPos(
            animationType == AnimationType.appear ? _targetAnchoredPosition : _startTransform.localPosition,
            _duration).SetEase(_animationCurve);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        if (_startTransform == null)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_startTransform.position, 0.5f);
        Gizmos.DrawWireSphere(_rectTransform.position, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_startTransform.position, _rectTransform.position);
    }
#endif
}